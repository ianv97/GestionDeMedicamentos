import React from "react";
import TablaPartidas from "../components/TablaPartidas";

class Partidas extends React.Component {
  render() {
    return (
      <React.Fragment>
        <h1>Partidas</h1>
        <TablaPartidas/>
      </React.Fragment>
    )
  }
}

export default Partidas;
