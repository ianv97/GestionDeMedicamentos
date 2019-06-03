import React from "react";
import MaterialTable from "./MaterialTable.js";

class TablaReposiciones extends React.Component {
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
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones"
      );
      this.setState({
        loading: false
      });
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      const data = await response.json();
      const displayData = [];
      data.forEach(function(reposicion) {
        displayData.push([reposicion.id, reposicion.date]);
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
        currentUrl={"Reposiciones"}
        loading={this.state.loading}
        error={this.state.error}
      />
    );
  }
}

export default TablaReposiciones;
