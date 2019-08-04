import React, { Component } from "react";
import PropTypes from "prop-types";
import { Motion, spring } from "react-motion";
import Input from "./Input";
import SubmitButton from "./SubmitButton";
import Context from "../../Context";

class SignExpanded extends Component {
  constructor(props) {
    super(props);
    this.state = {
      flexState: false,
      animIsFinished: false,
      currentUrl: "Auth",
      form: {
        username: "",
        password: "",
        name: ""
      }
    };
  }

  componentDidMount() {
    this.setState({ flexState: !this.state.flexState });
  }

  isFinished = () => {
    this.setState({ animIsFinished: true });
  };

  handleChange = e => {
    this.setState({
      form: {
        ...this.state.form,
        [e.target.name]: e.target.value
      }
    });
  };

  handleSubmit = async (e, setAuth) => {
    e.preventDefault();

    const response = await fetch(window.ApiUrl + this.state.currentUrl, {
      method: "POST",
      mode: "cors",
      body: JSON.stringify(this.state.form),
      headers: {
        "Content-Type": "application/json"
      }
    }).then(response => {
      if (response.ok) {
        const data = response.json();
        window.container.success("Bienvenido", "Sesión iniciada", {
          showAnimation: "animated rubberBand",
          hideAnimation: "animated flipOutX",
          timeOut: 7000,
          extendedTimeOut: 2000
        });
        this.setState({ mounted: false });
        setAuth(true);
      } else {
        window.container.error("Usuario y/o contraseña incorrectos", "Error", {
          showAnimation: "animated rubberBand",
          hideAnimation: "animated flipOutX",
          timeOut: 7000,
          extendedTimeOut: 2000
        });
      }
    });
  };

  render() {
    return (
      <Motion
        style={{
          flexVal: spring(this.state.flexState ? 8 : 1)
        }}
        onRest={this.isFinished}
      >
        {({ flexVal }) => (
          <div
            className={this.props.type === "signIn" ? "signInExpanded" : "signUpExpanded"}
            style={{
              flexGrow: `${flexVal}`
            }}
          >
            <Motion
              style={{
                opacity: spring(this.state.flexState ? 1 : 0, { stiffness: 300, damping: 17 }),
                y: spring(this.state.flexState ? 0 : 50, { stiffness: 100, damping: 17 })
              }}
            >
              {({ opacity, y }) => (
                <Context.Consumer>
                  {({ setAuth }) => (
                    <form
                      onSubmit={e => this.handleSubmit(e, setAuth)}
                      className="login logForm"
                      style={{
                        WebkitTransform: `translate3d(0, ${y}px, 0)`,
                        transform: `translate3d(0, ${y}px, 0)`,
                        opacity: `${opacity}`
                      }}
                    >
                      <h2>{this.props.type === "signIn" ? "INGRESAR" : "REGISTRARSE"}</h2>
                      <Input
                        type="text"
                        placeholder="Usuario"
                        name="username"
                        value={this.state.form.username}
                        onChange={this.handleChange}
                      />
                      <Input
                        id="password"
                        type="password"
                        placeholder="Contraseña"
                        name="password"
                        value={this.state.form.password}
                        onChange={this.handleChange}
                      />
                      <a href="url" className="login forgotPass">
                        {this.props.type === "signIn" ? "¿Olvidó su contraseña?" : ""}
                      </a>
                      {this.props.type !== "signIn" && (
                        <Input
                          type="text"
                          placeholder="Nombre"
                          name="name"
                          value={this.state.form.name}
                          onChange={this.handleChange}
                        />
                      )}
                      <SubmitButton type={this.props.type} />
                    </form>
                  )}
                </Context.Consumer>
              )}
            </Motion>
          </div>
        )}
      </Motion>
    );
  }
}

SignExpanded.propTypes = {
  type: PropTypes.string
};

export default SignExpanded;
