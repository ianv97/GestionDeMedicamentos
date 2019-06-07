import React from "react";
import MaterialTable from "./MaterialTable.js";

class TablaDrogas extends React.Component {
  state = {
    loading: true,
    error: null,
    data: []
  };

  componentDidMount() {
    this.getData();
  }

  componentDidUpdate() {
    if (!this.state.error) {
      this.getData();
    }
  }

  async getData() {
    try {
      const response = await fetch(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas?order=name"
      );
      if (!response.ok) {
        throw Error(response.status + " " + response.statusText);
      }
      const data = await response.json();
      const displayData = [];
      data.forEach(function(drug) {
        displayData.push([drug.id, drug.name]);
      });
      this.setState({
        data: displayData
      });
    } catch (error) {
      this.setState({
        error: error
      });
    } finally {
      this.setState({
        loading: false
      });
    }
  }

  render() {
    return (
      <MaterialTable
        titles={["ID", "Nombre"]}
        data={this.state.data}
        currentUrl={"Drogas"}
        loading={this.state.loading}
        error={this.state.error}
      />
    );
  }
}

export default TablaDrogas;
