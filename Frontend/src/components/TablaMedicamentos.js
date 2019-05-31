import React from "react"
import MaterialTable from "./MaterialTable.js"

class TablaMedicamentos extends React.Component {
    state = {data: []}

    componentDidMount() {
        this.getData()
    }

    async getData() {
        const response = await fetch("http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos");
        const data = await response.json();
        const displayData = [];
        data.forEach(function (med){
            displayData.push([med.id, med.name, med.drug.name, med.proportion, med.presentation, med.laboratory, med.stock])
        })
        this.setState({
            data: displayData
        });
    }

    render() {
      return (
          <MaterialTable titles={["ID", "Nombre", "Droga", "Proporción", "Presentación", "Laboratorio", "Stock"]} data={this.state.data}/>)
    }
  }
  
  export default TablaMedicamentos;