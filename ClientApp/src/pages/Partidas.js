import React from "react";
import Grid from "@material-ui/core/Grid";
import MaterialTable from "../components/MaterialTable.js";
import handleSearch from "../functions/handleSearch";
import handleChangePage from "../functions/handleChangePage";
import handleChangeRowsPerPage from "../functions/handleChangeRowsPerPage";

class Partidas extends React.Component {
  state = {
    currentUrl: "partidas",
    loading: true,
    error: null,
    data: [],
    pageSize: 5,
    pageNumber: 1,
    totalRecords: 0,
    order: "date",
    search: {
      id: "",
      date: ""
    }
  };
  handleSearch = handleSearch.bind(this);
  handleChangePage = handleChangePage.bind(this);
  handleChangeRowsPerPage = handleChangeRowsPerPage.bind(this);

  componentDidMount() {
    this.getData();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.getData());
  }

  async getData(search) {
    this.setState({ error: null });
    try {
      let apiUrl =
        window.ApiUrl +
        this.state.currentUrl +
        "?order=" +
        this.state.order +
        "&pageSize=" +
        this.state.pageSize +
        "&pageNumber=" +
        this.state.pageNumber;
      let response;
      this.state.search.id
        ? (response = await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.state.search.id))
        : search
        ? (response = await fetch(apiUrl + search))
        : (response = await fetch(apiUrl));
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
      this.setState({
        data: displayData,
        page: response.headers.get("page"),
        totalRecords: parseInt(response.headers.get("totalRecords"))
      });
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
          currentUrl={this.state.currentUrl}
          edit={false}
          loading={this.state.loading}
          error={this.state.error}
          handleSearch={this.handleSearch}
          pageSize={this.state.pageSize}
          pageNumber={this.state.pageNumber}
          totalRecords={this.state.totalRecords}
          handleChangePage={this.handleChangePage}
          handleChangeRowsPerPage={this.handleChangeRowsPerPage}
        />
      </React.Fragment>
    );
  }
}

export default Partidas;
