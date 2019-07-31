import React, { Component } from "react";
import PropTypes from "prop-types";
import { Motion, spring } from "react-motion";
import Input from "./Input";
import SubmitButton from "./SubmitButton";

class SignExpanded extends Component {
  constructor(props) {
    super(props);
    this.state = {
      flexState: false,
      animIsFinished: false
    };
  }

  componentDidMount() {
    this.setState({ flexState: !this.state.flexState });
  }

  isFinished = () => {
    this.setState({ animIsFinished: true });
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
                <form
                  onSubmit={this.props.onSubmit}
                  className="login logForm"
                  style={{
                    WebkitTransform: `translate3d(0, ${y}px, 0)`,
                    transform: `translate3d(0, ${y}px, 0)`,
                    opacity: `${opacity}`
                  }}
                >
                  <h2>{this.props.type === "signIn" ? "INGRESAR" : "REGISTRARSE"}</h2>
                  <Input id="login" type="text" placeholder="Usuario" />
                  <Input id="password" type="password" placeholder="Contraseña" />
                  <SubmitButton type={this.props.type} />
                  <a href="url" className="login forgotPass">
                    {this.props.type === "signIn" ? "¿Olvidó su contraseña?" : ""}
                  </a>
                </form>
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
