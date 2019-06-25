import React from "react";
import Grid from "@material-ui/core/Grid";
import MaterialTable from "../components/MaterialTable.js";
import handleSearch from "../functions/handleSearch";

class Partidas extends React.Component {
  state = {
    loading: true,
    error: null,
    data: [],
    search: {
      id: "",
      date: ""
    }
  };
  handleSearch = handleSearch.bind(this);

  componentDidMount() {
    this.getData();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.getData());
  }

  async getData(search) {
    this.setState({ error: null });
    try {
      let response;
      this.state.search.id
        ? (response = await fetch(window.ApiUrl + "partidas/" + this.state.search.id))
        : search
        ? (response = await fetch(window.ApiUrl + "partidas?order=date" + search))
        : (response = await fetch(window.ApiUrl + "partidas?order=date"));
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      let data = await response.json();
      if (!Array.isArray(data)) {
        data = [data];
      }
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
          titles={[["ID", "id"], ["Fecha", "date"]]}
          data={this.state.data}
          currentUrl={"Partidas"}
          edit={false}
          loading={this.state.loading}
          error={this.state.error}
          handleSearch={this.handleSearch}
        />
      </React.Fragment>
    );
  }
}

export default Partidas;
