import React from "react";
import Breadc from "@material-ui/core/Breadcrumbs";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import { Link } from "react-router-dom";
import Typography from "@material-ui/core/Typography";

function Breadcrumbs(props) {
  return (
    <Breadc separator={<NavigateNextIcon fontSize="small" />}>
      <Link color="inherit" to="/">
        Gestión de medicamentos
      </Link>
      <Link color="inherit" to={"/" + props.currentUrl}>
        {props.currentUrl.charAt(0).toUpperCase() + props.currentUrl.slice(1)}
      </Link>
      <Typography color="textPrimary">{props.id}</Typography>
    </Breadc>
  );
}

export default Breadcrumbs;
