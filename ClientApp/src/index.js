import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import "bootstrap/dist/css/bootstrap.css";

window.ApiUrl = "http://medicamentos.us-east-1.elasticbeanstalk.com/api/";
ReactDOM.render(<App />, document.getElementById("app"));
