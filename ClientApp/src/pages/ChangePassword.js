import React from "react";
import Grid from "@material-ui/core/Grid";
import handleSubmit from "../functions/handleSubmit";
import { Div, Form, Input, Span, Button } from "../styles/ChangePasswordStyles.js";
import getCookie from "../functions/getCookie.js";
import CircularProgress from "@material-ui/core/CircularProgress";

class ChangePassword extends React.Component {
  state = {
    mode: "update",
    currentUrl: "change-password",
    warning: "",
    loading: false,
    form: {
      username: getCookie("username"),
      oldPassword: "",
      newPassword: "",
      newPasswordV: ""
    }
  };
  handleSubmit = handleSubmit.bind(this);

  handleChange = e => {
    this.setState(
      {
        form: {
          ...this.state.form,
          [e.target.name]: e.target.value
        }
      },
      () => {
        if (
          this.state.form.newPasswordV !== "" &&
          this.state.form.newPassword !== this.state.form.newPasswordV
        ) {
          this.setState({ warning: "Las contraseñas no coinciden" });
        } else {
          this.setState({ warning: "" });
        }
      }
    );
  };

  render() {
    return (
      <Grid container direction="column" justify="center" style={{ height: 80 + "vh" }}>
        <Div className="container-login100">
          <Div className="wrap-login100">
            <Form
              className="login100-form validate-form"
              onSubmit={e => this.handleSubmit(e, this.state.currentUrl + "/" + this.props.match.params.id)}
            >
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
              <label style={{ fontSize: "15px", color: "red" }}>{this.state.warning}</label>
              <Div className="container-login100-form-btn">
                <Button type="submit" className="login100-form-btn" disabled={this.state.warning !== ""}>
                  {this.state.loading ? <CircularProgress /> : "Cambiar contraseña"}
                </Button>
              </Div>
            </Form>
          </Div>
        </Div>
      </Grid>
    );
  }
}

export default ChangePassword;
