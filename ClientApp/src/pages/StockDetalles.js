import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import post from "../functions/post";
import put from "../functions/put";
import del from "../functions/delete";

class StockDetalles extends React.Component {
  state = { id: null, fecha: null };
  changeMode = changeMode.bind(this);

  async getData() {
    const response = await fetch(
      "http://medicamentos.us-east-1.elasticbeanstalk.com/api/stock/" +
        this.props.match.params.id
    );
    const data = await response.json();
    this.setState({
      id: data.id,
      nombre: data.date
    });
  }

  componentDidMount() {
    if (this.props.match.params.id !== "Añadir") {
      this.getData();
    }
    this.changeMode();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.changeMode());
  }

  render() {
    return (
      <div>
        <Breadcrumbs currentUrl={"Drogas"} id={this.props.match.params.id} />

        <Grid container direction="row" justify="center" className="mt-5">
          <Grid item>
            <Grid container direction="column">
              <Grid item>
                <h1>Stock</h1>
              </Grid>
              <Grid item>
                <TextField
                  label="ID"
                  margin="normal"
                  variant="outlined"
                  name="id"
                  value={this.state.id}
                  InputProps={{
                    readOnly: true
                  }}
                />
              </Grid>
              <Grid item>
                <TextField
                  label="Nombre"
                  margin="normal"
                  variant="outlined"
                  name="fecha"
                  onChange={this.handleChange}
                  value={this.state.fecha}
                  InputProps={{
                    readOnly: true
                  }}
                />
              </Grid>
              <ButtonsRow
                id={this.props.match.params.id}
                mode={this.state.mode}
                history={this.props.history}
              />
            </Grid>
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default StockDetalles;
