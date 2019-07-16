import React from "react";
import Grid from "@material-ui/core/Grid";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";
import MaterialTable from "../components/MaterialTable.js";
import handleSearch from "../functions/handleSearch";
import handleChangePage from "../functions/handleChangePage";
import handleChangeRowsPerPage from "../functions/handleChangeRowsPerPage";
import getData from "../functions/getData";

class Stock extends React.Component {
  state = {
    currentUrl: "stock",
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
        : this.state.searchString
        ? (response = await fetch(apiUrl + this.state.searchString))
        : (response = await fetch(apiUrl));
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      let data = await response.json();
      if (!Array.isArray(data)) {
        data = [data];
      }
      const displayData = [];
      data.forEach(function(reposicion) {
        displayData.push([reposicion.id, reposicion.date]);
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
            <h1>Stock</h1>
          </Grid>
          <Grid item>
            <Link to={"/" + this.state.currentUrl + "/añadir"}>
              <Fab color="primary" size="medium">
                <AddIcon />
              </Fab>
            </Link>
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

export default Stock;
