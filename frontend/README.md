# Kairos frontend

Responsive React and TypeScript web application. The first route is `/today`
and provides the stable shell for the athlete's daily coaching view.

Authenticated athletes can import and browse FIT activities at `/activities`.
The overview and detail behavior is documented in
[`docs/ACTIVITY_UI.md`](../docs/ACTIVITY_UI.md).

## Commands

```powershell
npm.cmd install
npm.cmd run dev
npm.cmd run lint
npm.cmd run typecheck
npm.cmd test
npm.cmd run build
```

The Vite development server defaults to `http://localhost:5173`. No runtime
secret belongs in the frontend bundle; only explicitly public `VITE_*` values
may be supplied at build time.
