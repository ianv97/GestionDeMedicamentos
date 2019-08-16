import React from "react";
import "../components/Login/login.css";
import ReactCSSTransitionGroup from "react-addons-css-transition-group";
import NavigationPanel from "../components/Login/NavigationPanel";
import Modal from "../components/Login/Modal";
import styled from "styled-components";
import img from "../components/Login/login_background.jpg";

const Div = styled.div`
  background-image: url(${img});
  background-size: 100%;
  width: 100vw;
  height: 100vh;
`;

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      mounted: false
    };
  }

  componentDidMount() {
    this.setState({ mounted: true });
  }

  render() {
    const { mounted } = this.state;
    let child;

    if (mounted) {
      child = (
        <div className="login App_test">
          <NavigationPanel />
          <Modal />
        </div>
      );
    }

    return (
      <Div>
        <div className="login App">
          <ReactCSSTransitionGroup
            transitionName="example"
            transitionEnterTimeout={500}
            transitionLeaveTimeout={300}
          >
            {child}
          </ReactCSSTransitionGroup>
        </div>
      </Div>
    );
  }
}

export default Login;
