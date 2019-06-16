import React from "react";
import Grid from "@material-ui/core/Grid";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import { Link } from "react-router-dom";
import MaterialTable from "../components/MaterialTable.js";

class Medicamentos extends React.Component {
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
      const response = await fetch(window.ApiUrl + "medicamentos?order=name");
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      const data = await response.json();
      const displayData = [];
      data.forEach(function(med) {
        displayData.push([
          med.id,
          med.name,
          med.drug.name,
          med.proportion,
          med.presentation,
          med.laboratory,
          med.stock
        ]);
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
            <h1>Medicamentos</h1>
          </Grid>
          <Grid item>
            <Link to="/Medicamentos/Añadir">
              <Fab color="primary" size="medium">
                <AddIcon />
              </Fab>
            </Link>
          </Grid>
        </Grid>

        <MaterialTable
          titles={["ID", "Nombre", "Droga", "Proporción (mg)", "Presentación", "Laboratorio", "En Stock"]}
          data={this.state.data}
          currentUrl={"Medicamentos"}
          loading={this.state.loading}
          error={this.state.error}
        />
      </React.Fragment>
    );
  }
}

export default Medicamentos;
