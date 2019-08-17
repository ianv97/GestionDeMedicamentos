import React from "react";
import PropTypes from "prop-types";
import CircularProgress from "@material-ui/core/CircularProgress";
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
        {props.loading ? (
          <CircularProgress color="secondary" />
        ) : (
          <div>
            <i className="login fas fa-arrow-right" />
          </div>
        )}
      </button>
    </div>
  );
};

SubmitButton.propTypes = {
  type: PropTypes.string
};

export default SubmitButton;
