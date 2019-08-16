import React from "react";

const Inicio = React.lazy(() => import("../pages/Inicio.js"));
const Drogas = React.lazy(() => import("../pages/Drogas.js"));
const DrogasDetalles = React.lazy(() => import("../pages/DrogasDetalles.js"));
const Medicamentos = React.lazy(() => import("../pages/Medicamentos.js"));
const MedicamentosDetalles = React.lazy(() => import("../pages/MedicamentosDetalles.js"));
const Reposiciones = React.lazy(() => import("../pages/Reposiciones.js"));
const ReposicionesDetalles = React.lazy(() => import("../pages/ReposicionesDetalles.js"));
const Partidas = React.lazy(() => import("../pages/Partidas.js"));
const PartidasDetalles = React.lazy(() => import("../pages/PartidasDetalles.js"));
const Stock = React.lazy(() => import("../pages/Stock.js"));
const StockDetalles = React.lazy(() => import("../pages/StockDetalles.js"));
const ChangePassword = React.lazy(() => import("../pages/ChangePassword.js"));

const routes = [
  { path: "/inicio", exact: true, name: "Inicio", component: Inicio },
  { path: "/drogas", exact: true, name: "Drogas", component: Drogas },
  { path: "/drogas/:id", name: "Detalles", component: DrogasDetalles },
  { path: "/medicamentos", exact: true, name: "Medicamentos", component: Medicamentos },
  { path: "/medicamentos/:id", name: "Detalles", component: MedicamentosDetalles },
  { path: "/reposiciones", exact: true, name: "Reposiciones", component: Reposiciones },
  { path: "/reposiciones/:id", name: "Detalles", component: ReposicionesDetalles },
  { path: "/partidas", exact: true, name: "Partidas", component: Partidas },
  { path: "/partidas/:id", name: "Detalles", component: PartidasDetalles },
  { path: "/stock", exact: true, name: "Stock", component: Stock },
  { path: "/stock/:id", name: "Detalles", component: StockDetalles },
  { path: "/cambiar-contraseña/:id", name: "Cambiar contraseña", component: ChangePassword }
];

export default routes;
