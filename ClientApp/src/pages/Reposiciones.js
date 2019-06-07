import React from "react";
import Grid from "@material-ui/core/Grid";
import TablaReposiciones from "../components/TablaReposiciones";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";

class Reposiciones extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Grid container spacing={2}>
          <Grid item>
            <h1>Reposiciones</h1>
          </Grid>
          <Grid item>
            <Link to="/Reposiciones/Añadir">
              <Fab color="primary" size="medium">
                <AddIcon />
              </Fab>
            </Link>
          </Grid>
        </Grid>
        <TablaReposiciones />
      </React.Fragment>
    );
  }
}

export default Reposiciones;
