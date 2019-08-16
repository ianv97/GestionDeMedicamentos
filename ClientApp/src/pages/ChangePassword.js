import React from "react";
import handleChange from "../functions/handleChange";
import handleSubmit from "../functions/handleSubmit";
import { Div, Form, Input, Span, Button } from "../styles/ChangePasswordStyles.js";
import getCookie from "../functions/getCookie.js";

class ChangePassword extends React.Component {
  state = {
    mode: "update",
    currentUrl: "user",
    form: {
      username: getCookie("username"),
      oldPassword: "",
      newPassword: "",
      newPasswordV: ""
    }
  };
  handleChange = handleChange.bind(this);
  handleSubmit = handleSubmit.bind(this);

  render() {
    return (
        <Div className="container-login100">
          <Div className="wrap-login100">
            <Form className="login100-form validate-form" onSubmit={this.handleSubmit} >
              <Span className="login100-form-title">Cambio de Contraseña</Span>
              <Div className="wrap-input100 validate-input mb-0">
                <Input
                  className="input100"
                  type="password"
                  name="oldPassword"
                  placeholder="Contraseña actual"
                  value={this.state.form.oldPassword}
                  onChange={this.handleChange}
                />
                <Span className="focus-input100" />
                <Span className="symbol-input100">
                  <i className="fa fa-lock" aria-hidden="true" />
                </Span>
              </Div>

              <Div className="wrap-input100 validate-input mt-4 mb-0">
                <Span
                  className="badge badge-pill badge-danger"
                  style={{ position: "absolute", margin: "15px 15px" }}
                >
                  New
                </Span>
                <Input
                  className="input100"
                  type="password"
                  name="newPassword"
                  placeholder="Nueva contraseña"
                  value={this.state.form.newPassword}
                  onChange={this.handleChange}
                />
                <Span className="focus-input100" />
              </Div>

              <Div className="wrap-input100 validate-input mt-4 mb-0">
                <Span
                  className="badge badge-pill badge-danger"
                  style={{ position: "absolute", margin: "15px 15px" }}
                >
                  New
                </Span>
                <Input
                  className="input100"
                  type="password"
                  name="newPasswordV"
                  placeholder="Repita la nueva contraseña"
                  value={this.state.form.newPasswordV}
                  onChange={this.handleChange}
                />
                <Span className="focus-input100" />
              </Div>

              <Div className="container-login100-form-btn">
                <Button type="submit" className="login100-form-btn">
                  Cambiar contraseña
                </Button>
              </Div>
            </Form>
          </Div>
        </Div>
    );
  }
}

export default ChangePassword;
