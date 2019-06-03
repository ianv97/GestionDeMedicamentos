import React from "react";
import Grid from "@material-ui/core/Grid";
import TablaMedicamentos from "../components/TablaMedicamentos";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";

class Medicamentos extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Grid container spacing={2}>
          <Grid item>
            <h1>Medicamentos</h1>
          </Grid>
          <Grid item>
            <Link to="/Medicamentos/Añadir">
              <Fab color="primary" size="medium">
                <AddIcon />
              </Fab>
            </Link>
          </Grid>
        </Grid>
        <TablaMedicamentos />
      </React.Fragment>
    );
  }
}

export default Medicamentos;
