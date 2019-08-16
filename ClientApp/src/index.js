import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import "bootstrap/dist/css/bootstrap.css";
import Context from "./Context";

window.token = "";
window.ApiUrl = "http://localhost:50176/api/";
//"http://medicamentos.us-east-1.elasticbeanstalk.com/api/";
ReactDOM.render(
  <Context.Provider>
    <App />
  </Context.Provider>,
  document.getElementById("app")
);
