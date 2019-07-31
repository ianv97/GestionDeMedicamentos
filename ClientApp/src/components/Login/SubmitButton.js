import React from "react";
import PropTypes from "prop-types";
// import { FaGooglePlus, FaTwitter, FaFacebook } from "react-icons/lib/fa";

const SubmitButton = props => {
  let socialNets;

  if (props.type === "signIn") {
    socialNets = (
      <div className="login socialNets">
        {/* <FaGooglePlus className="login socialNetsIcon" />
        <FaTwitter className="login socialNetsIcon" />
        <FaFacebook className="login socialNetsIcon" /> */}
      </div>
    );
  } else {
    socialNets = <div className="login socialNets" />;
  }
  return (
    <div className={"submitButton"}>
      {socialNets}
      <button className={props.type === "signIn" ? "submitSignIn" : "submitSignUp"}>
        <i className="login fas fa-arrow-right" />
      </button>
    </div>
  );
};

SubmitButton.propTypes = {
  type: PropTypes.string
};

export default SubmitButton;
