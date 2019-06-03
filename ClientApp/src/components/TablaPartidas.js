import React from "react";
import MaterialTable from "./MaterialTable.js";

class TablaPartidas extends React.Component {
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
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/partidas"
      );
      this.setState({
        loading: false
      });
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      const data = await response.json();
      const displayData = [];
      data.forEach(function(partida) {
        displayData.push([partida.id, partida.date]);
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
        titles={["ID", "Fecha"]}
        data={this.state.data}
        currentUrl={"Drogas"}
        loading={this.state.loading}
        error={this.state.error}
      />
    );
  }
}

export default TablaPartidas;
