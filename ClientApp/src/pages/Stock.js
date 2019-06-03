import React from "react";
import Grid from "@material-ui/core/Grid";
import TablaStock from "../components/TablaStock";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";

class Stock extends React.Component {
  render() {
    return (
      <React.Fragment>
        <Grid container spacing={2}>
          <Grid item>
            <h1>Stock</h1>
          </Grid>
          <Grid item>
            <Link to="/Stock/Añadir">
                <Fab color="primary" size="medium">
                 <AddIcon />
                </Fab>
            </Link>
          </Grid>
        </Grid>
        <TablaStock />
      </React.Fragment>
    );
  }
}

export default Stock;
