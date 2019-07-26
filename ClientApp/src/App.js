import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import "./Layout/App.scss";
import { ToastContainer } from "react-toastr";
import { ToastMessageAnimated } from "react-toastr";
import "toastr/build/toastr.css";
import "animate.css/animate.css";
import CircularProgress from "@material-ui/core/CircularProgress";

const Login = React.lazy(() => import("./pages/Login"));
const DefaultLayout = React.lazy(() => import("./Layout/containers/DefaultLayout"));
const loading = () => (
  <div className="animated fadeIn pt-3 text-center">
    <CircularProgress />
  </div>
);

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
          <React.Suspense fallback={loading()}>
            <Switch>
              <Route exact path="/login" name="Login" render={props => <Login {...props} />} />
              <Route path="/" name="Inicio" render={props => <DefaultLayout {...props} />} />
            </Switch>
          </React.Suspense>
        </BrowserRouter>
      </React.Fragment>
    );
  }
}

export default App;
