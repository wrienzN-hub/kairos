import { Component, type ErrorInfo, type ReactNode } from "react";

type Props = { children: ReactNode };
type State = { hasError: boolean };

export class AppErrorBoundary extends Component<Props, State> {
  public state: State = { hasError: false };

  public static getDerivedStateFromError(): State {
    return { hasError: true };
  }

  public componentDidCatch(error: Error, info: ErrorInfo) {
    console.error("Kairos UI failed to render.", error, info);
  }

  public render() {
    if (this.state.hasError) {
      return (
        <main className="error-state">
          <p className="eyebrow">Kairos</p>
          <h1>Die Ansicht konnte nicht geladen werden.</h1>
          <p>
            Bitte lade die Seite neu. Deine Trainingsdaten bleiben erhalten.
          </p>
          <button type="button" onClick={() => window.location.reload()}>
            Neu laden
          </button>
        </main>
      );
    }

    return this.props.children;
  }
}
