import React from "react";
import MaterialTable from "./MaterialTable.js";

class TablaMedicamentos extends React.Component {
  state = {
    loading: true,
    error: null,
    data: []
  };

  componentDidMount() {
    this.getData();
  }

  async getData() {
    try {
      const response = await fetch(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos?order=name"
      );
      this.setState({
        loading: false
      });
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
      this.setState({
        data: displayData
      });
    } catch (error) {
      this.setState({
        error: error
      });
    }
  }

  render() {
    return (
      <MaterialTable
        titles={[
          "ID",
          "Nombre",
          "Droga",
          "Proporción",
          "Presentación",
          "Laboratorio",
          "En Stock"
        ]}
        data={this.state.data}
        currentUrl={"Medicamentos"}
        loading={this.state.loading}
        error={this.state.error}
      />
    );
  }
}

export default TablaMedicamentos;
