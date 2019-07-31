import React from "react";
import "../components/Login/login.css";
import ReactCSSTransitionGroup from "react-addons-css-transition-group";
import NavigationPanel from "../components/Login/NavigationPanel";
import Modal from "../components/Login/Modal";
import Context from "../Context";

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

  handleSubmit = (e, setAuth) => {
    e.preventDefault();
    this.setState({ mounted: false });
    setAuth(true);
  };

  render() {
    const { mounted } = this.state;
    let child;
    if (mounted) {
      child = (
        <div className="login App_test">
          <NavigationPanel />
          <Context.Consumer>
            {({ setAuth }) => {
              return <Modal onSubmit={e => this.handleSubmit(e, setAuth)} />;
            }}
          </Context.Consumer>
          ;
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
