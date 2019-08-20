import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";
import "./layout/scss/style.scss";
import GlobalStyle from "./styles/GlobalStyles";
import { ToastContainer } from "react-toastr";
import { ToastMessageAnimated } from "react-toastr";
import "toastr/build/toastr.css";
import "animate.css/animate.css";
import CircularProgress from "@material-ui/core/CircularProgress";
import getCookie from "./functions/getCookie";

const Login = React.lazy(() => import("./pages/Login"));
const DefaultLayout = React.lazy(() => import("./layout/containers/DefaultLayout.js"));
const loading = () => (
  <div className="animated fadeIn pt-3 text-center">
    <CircularProgress />
  </div>
);

class App extends React.Component {
  state = { isAuth: getCookie("token") ? true : false };

  isAuth() {
    if (getCookie("token")) {
      this.setState({ isAuth: true });
    } else {
      this.setState({ isAuth: false });
    }
  }

  intervalo = setInterval(this.isAuth.bind(this), 2000);

  render() {
    return (
      <React.Fragment>
        <GlobalStyle />
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
              {this.state.isAuth ? (
                <Route path="/" name="Inicio" render={props => <DefaultLayout {...props} />} />
              ) : (
                <Route path="/" name="Login" render={props => <Login {...props} />} />
              )}
            </Switch>
          </React.Suspense>
        </BrowserRouter>
      </React.Fragment>
    );
  }
}

export default App;
