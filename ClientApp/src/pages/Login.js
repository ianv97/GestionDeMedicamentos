import React from "react";
import "../login.css";
import ReactCSSTransitionGroup from "react-addons-css-transition-group";
import NavigationPanel from "../components/Login/NavigationPanel";
import Modal from "../components/Login/Modal";

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

  handleSubmit = e => {
    this.setState({ mounted: false });
    e.preventDefault();
  };

  render() {
    const { mounted } = this.state;
    let child;
    if (mounted) {
      child = (
        <div className="login App_test">
          <NavigationPanel />
          <Modal onSubmit={this.handleSubmit} />
        </div>
      );
    }

    return (
      <div className="login App">
        <ReactCSSTransitionGroup
          transitionName="example"
          transitionEnterTimeout={500}
          transitionLeaveTimeout={300}
        >
          {child}
        </ReactCSSTransitionGroup>
      </div>
    );
  }
}

export default Login;
