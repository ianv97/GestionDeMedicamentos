import React from "react";
import Grid from "@material-ui/core/Grid";
import MaterialTable from "../components/MaterialTable.js";

class Partidas extends React.Component {
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
      const response = await fetch(window.ApiUrl + "partidas?order=date");
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      const data = await response.json();
      const displayData = [];
      data.forEach(function(partida) {
        displayData.push([partida.id, partida.date]);
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
            <h1>Partidas</h1>
          </Grid>
        </Grid>

        <MaterialTable
          titles={["ID", "Fecha"]}
          data={this.state.data}
          currentUrl={"Partidas"}
          edit={false}
          loading={this.state.loading}
          error={this.state.error}
        />
      </React.Fragment>
    );
  }
}

export default Partidas;
