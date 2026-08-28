export type ActivityListItem = {
  id: string;
  type: string;
  startUtc: string;
  endUtc: string;
  distanceMeters: number | null;
  analysisStatus: "eligible" | "limited" | "blocked";
  originalFileName: string | null;
  sourceProvider: string;
};

export type ActivityMetric = {
  code: string;
  value: number;
  unit: { code: string; symbol: string };
  provenance: {
    origin: "measured" | "importedsummary" | "userentered" | "derived";
    sourceField: string | null;
    sourceUnit: string | null;
    derivation: {
      method: string;
      version: string;
      inputMetricCodes: string[];
    } | null;
  };
};

export type ActivityDetail = {
  id: string;
  type: string;
  source: {
    kind: string;
    provider: string;
    externalIdentifier: string | null;
    originalFileName: string | null;
    contentHashSha256: string | null;
    importedAtUtc: string;
  };
  timeRange: {
    start: ActivityTimestamp;
    end: ActivityTimestamp;
  };
  summary: ActivityMetric[];
  samples: Array<{ timestampUtc: string; metrics: ActivityMetric[] }>;
  segments: Array<{
    index: number;
    type: string;
    timeRange: { start: ActivityTimestamp; end: ActivityTimestamp };
    summary: ActivityMetric[];
  }>;
  quality: {
    analysisStatus: "eligible" | "limited" | "blocked";
    isAnalysisRestricted: boolean;
    findings: Array<{
      code: string;
      severity: "information" | "warning" | "error";
      message: string;
      affectedMetricCodes: string[];
    }>;
  };
};

type ActivityTimestamp = {
  instantUtc: string;
  timeZoneId: string;
  observedUtcOffsetMinutes: number;
};

async function authorizedRequest(
  path: string,
  token: string,
  init?: RequestInit,
) {
  const response = await fetch(path, {
    ...init,
    headers: {
      ...init?.headers,
      Authorization: `Bearer ${token}`,
    },
  });
  if (!response.ok) {
    throw new Error(
      `Kairos API request failed with status ${response.status}.`,
    );
  }
  return response;
}

export async function listActivities(token: string) {
  const response = await authorizedRequest("/api/activities", token);
  return (await response.json()) as ActivityListItem[];
}

export async function getActivity(id: string, token: string) {
  const response = await authorizedRequest(`/api/activities/${id}`, token);
  return (await response.json()) as ActivityDetail;
}

export async function exportActivity(id: string, token: string) {
  const response = await authorizedRequest(
    `/api/activities/${id}/export`,
    token,
  );
  const disposition = response.headers.get("Content-Disposition") ?? "";
  const encodedName = disposition.match(/filename\*=UTF-8''([^;]+)/i)?.[1];
  const plainName = disposition.match(/filename="?([^";]+)"?/i)?.[1];
  return {
    blob: await response.blob(),
    fileName:
      (encodedName ? decodeURIComponent(encodedName) : plainName) ??
      `kairos-activity-${id}.json`,
  };
}

export async function deleteActivity(id: string, token: string) {
  await authorizedRequest(`/api/activities/${id}`, token, {
    method: "DELETE",
  });
}

export async function uploadAndImportActivity(file: File, token: string) {
  const form = new FormData();
  form.append("file", file);
  const uploadResponse = await authorizedRequest(
    "/api/activity-imports/fit",
    token,
    { method: "POST", body: form },
  );
  const upload = (await uploadResponse.json()) as { id: string };
  const importResponse = await authorizedRequest(
    `/api/activity-imports/fit/${upload.id}/import`,
    token,
    { method: "POST" },
  );
  return (await importResponse.json()) as {
    id: string;
    status: "imported" | "duplicate";
  };
}
