import React from "react";
import Grid from "@material-ui/core/Grid";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";
import MaterialTable from "../components/MaterialTable.js";

class Reposiciones extends React.Component {
  state = {
    loading: true,
    error: null,
    data: []
  };

  componentDidMount() {
    this.getData();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.getData());
  }

  async getData() {
    try {
      const response = await fetch(window.ApiUrl + "reposiciones");
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      const data = await response.json();
      const displayData = [];
      data.forEach(function(reposicion) {
        displayData.push([reposicion.id, reposicion.date]);
      });
      this.setState({ data: displayData });
    } catch (error) {
      this.setState({ error: error });
    } finally {
      this.setState({ loading: false });
    }
  }

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

        <MaterialTable
          titles={["ID", "Fecha"]}
          data={this.state.data}
          currentUrl={"Reposiciones"}
          loading={this.state.loading}
          error={this.state.error}
        />
      </React.Fragment>
    );
  }
}

export default Reposiciones;
