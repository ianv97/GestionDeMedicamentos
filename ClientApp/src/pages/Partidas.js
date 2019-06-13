import React from "react";
import Grid from "@material-ui/core/Grid";
import TablaPartidas from "../components/TablaPartidas";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";

class Partidas extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Grid container spacing={2}>
          <Grid item>
            <h1>Partidas</h1>
          </Grid>
        </Grid>
        <TablaPartidas />
      </React.Fragment>
    );
  }
}

export default Partidas;
