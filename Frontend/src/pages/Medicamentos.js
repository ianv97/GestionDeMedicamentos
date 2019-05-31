import React from "react";
import TablaMedicamentos from "../components/TablaMedicamentos"

class Medicamentos extends React.Component {
  render() {
    return (
      <React.Fragment>
        <h1>Medicamentos</h1>
        <TablaMedicamentos/>
      </React.Fragment>
    )
  }
}

export default Medicamentos;
