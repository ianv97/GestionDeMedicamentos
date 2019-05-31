import React from "react";
import Navbar from "./Navbar";
import { Switch, Route } from "react-router-dom";
import Inicio from "../pages/Inicio";
import Drogas from "../pages/Drogas";
import DrogasDetalles from "../pages/DrogasDetalles";
import Medicamentos from "../pages/Medicamentos";
import Reposiciones from "../pages/Reposiciones";
import Partidas from "../pages/Partidas";
import Stock from "../pages/Stock";

function App() {
  return (
    <Navbar>
      <Switch>
        <Route exact path="/" component={Inicio} />
        <Route exact path="/Drogas" component={Drogas} />
        <Route exact path="/Drogas/:id" component={DrogasDetalles} />
        <Route exact path="/Medicamentos" component={Medicamentos} />
        <Route exact path="/Reposiciones" component={Reposiciones} />
        <Route exact path="/Partidas" component={Partidas} />
        <Route exact path="/Stock" component={Stock} />
      </Switch>
    </Navbar>
  );
}

export default App;
