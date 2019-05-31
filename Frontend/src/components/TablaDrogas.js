import React from "react"
import MaterialTable from "./MaterialTable.js"

class TablaDrogas extends React.Component {
    state = {data: []}

    componentDidMount() {
        this.getData()
    }

    async getData() {
        const response = await fetch("http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas");
        const data = await response.json();
        const displayData = [];
        data.forEach(function (drug){
            displayData.push([drug.id, drug.name])
        })
        this.setState({
            data: displayData
        });
    }

    render() {
      return (
          <MaterialTable titles={["ID", "Nombre"]} data={this.state.data}/>)
    }
  }
  
  export default TablaDrogas;