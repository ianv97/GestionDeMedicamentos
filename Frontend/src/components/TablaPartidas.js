import React from "react"
import MaterialTable from "./MaterialTable.js"

class TablaPartidas extends React.Component {
    state = {data: []}

    componentDidMount() {
        this.getData()
    }

    async getData() {
        const response = await fetch("http://medicamentos.us-east-1.elasticbeanstalk.com/api/partidas");
        const data = await response.json();
        const displayData = [];
        data.forEach(function (presc){
            displayData.push([presc.id, presc.date])
        })
        this.setState({
            data: displayData
        });
    }

    render() {
      return (
          <MaterialTable titles={["ID", "Fecha"]} data={this.state.data}/>)
    }
  }
  
  export default TablaPartidas;