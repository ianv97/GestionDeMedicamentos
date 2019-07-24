import React from "react";
import Navbar from "./components/Navbar";
import { BrowserRouter, Switch, Route } from "react-router-dom";
import Inicio from "./pages/Inicio";
import Login from "./pages/Login";
import Drogas from "./pages/Drogas";
import DrogasDetalles from "./pages/DrogasDetalles";
import Medicamentos from "./pages/Medicamentos";
import MedicamentosDetalles from "./pages/MedicamentosDetalles";
import Reposiciones from "./pages/Reposiciones";
import ReposicionesDetalles from "./pages/ReposicionesDetalles";
import Partidas from "./pages/Partidas";
import PartidasDetalles from "./pages/PartidasDetalles";
import Stock from "./pages/Stock";
import StockDetalles from "./pages/StockDetalles";
import { ToastContainer } from "react-toastr";
import { ToastMessageAnimated } from "react-toastr";

import "toastr/build/toastr.css";
import "animate.css/animate.css";

class App extends React.Component {
  render() {
    return (
      <React.Fragment>
        <ToastContainer
          toastMessageFactory={React.createFactory(ToastMessageAnimated)}
          className="toast-top-right"
          ref={ref => {
            window.container = ref;
          }}
        />
        <BrowserRouter>
          <Switch>
            <Route exact path="/" component={Login} />
            <Navbar>
              <Route exact path="/drogas" component={Drogas} />
              <Route exact path="/drogas/:id" component={DrogasDetalles} />
              <Route exact path="/medicamentos" component={Medicamentos} />
              <Route exact path="/medicamentos/:id" component={MedicamentosDetalles} />
              <Route exact path="/reposiciones" component={Reposiciones} />
              <Route exact path="/reposiciones/:id" component={ReposicionesDetalles} />
              <Route exact path="/partidas" component={Partidas} />
              <Route exact path="/partidas/:id" component={PartidasDetalles} />
              <Route exact path="/stock" component={Stock} />
              <Route exact path="/stock/:id" component={StockDetalles} />
            </Navbar>
          </Switch>
        </BrowserRouter>
      </React.Fragment>
    );
  }
}

export default App;
