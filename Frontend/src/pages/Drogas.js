import React from "react";
import Grid from "@material-ui/core/Grid";
import TablaDrogas from "../components/TablaDrogas";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";

class Drogas extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Grid container spacing={2}>
          <Grid item>
            <h1>Drogas</h1>
          </Grid>
          <Grid item>
            <Fab color="primary" size="medium">
              <AddIcon />
            </Fab>
          </Grid>
        </Grid>
        <TablaDrogas />
      </React.Fragment>
    );
  }
}

export default Drogas;
