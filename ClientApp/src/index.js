import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import "bootstrap/dist/css/bootstrap.css";
import Context from "./Context";

window.ApiUrl = "http://medicamentos.us-east-1.elasticbeanstalk.com/api/";
// "http://localhost:55604/api/";

ReactDOM.render(
  <Context.Provider>
    <App />
  </Context.Provider>,
  document.getElementById("app")
);
