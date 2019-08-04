import React from "react";
import PropTypes from "prop-types";
// import { MdVisibility } from "react-icons/lib/md";

function showHidePassword() {
  console.log("asdas");
  let input = document.getElementById("password");
  if (input.type === "password") {
    input.type = "text";
  } else {
    input.type = "password";
  }
}

const Input = props => {
  let iconVisibility = null;

  if (props.type === "password") {
    iconVisibility = (
      <span onClick={showHidePassword}>
        <i className="login iconVisibility fas fa-eye" />
      </span>
    );
  }

  return (
    <div className="login Input">
      <input
        id={props.name}
        autoComplete="false"
        required
        type={props.type}
        placeholder={props.placeholder}
        name={props.name}
        onChange={props.onChange}
      />
      {iconVisibility}
    </div>
  );
};

Input.propTypes = {
  id: PropTypes.string,
  type: PropTypes.string,
  placeholer: PropTypes.string
};

export default Input;
