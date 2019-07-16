import React from "react";
import Grid from "@material-ui/core/Grid";
import MaterialTable from "../components/MaterialTable.js";
import handleSearch from "../functions/handleSearch";
import handleChangePage from "../functions/handleChangePage";
import handleChangeRowsPerPage from "../functions/handleChangeRowsPerPage";
import getData from "../functions/getData";

class Partidas extends React.Component {
  state = {
    currentUrl: "partidas",
    titles: [["ID", "id"], ["Fecha", "date"]],
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
    },
    searchString: ""
  };

  getData = getData.bind(this);
  handleSearch = handleSearch.bind(this);
  handleChangePage = handleChangePage.bind(this);
  handleChangeRowsPerPage = handleChangeRowsPerPage.bind(this);

  componentDidMount() {
    this.getData();
  }

  componentDidUpdate() {
    this.props.history.listen(location => {
      if (location.pathname === "/" + this.state.currentUrl) {
        this.getData();
      }
    });
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
          titles={this.state.titles}
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
