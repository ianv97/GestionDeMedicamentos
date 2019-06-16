import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";

class PartidasDetalles extends React.Component {
  state = { id: null, fecha: null };

  async getData() {
    const response = await fetch(window.ApiUrl + "partidas/" + this.props.match.params.id);
    const data = await response.json();
    this.setState({
      id: data.id,
      fecha: data.date
    });
  }

  componentDidMount() {
    this.getData();
  }

  render() {
    return (
      <div>
        <Breadcrumbs currentUrl={"Partidas"} id={this.props.match.params.id} />

        <Grid container direction="row" justify="center" className="mt-5">
          <Grid item>
            <Grid container direction="column">
              <Grid item>
                <h1>Partidas</h1>
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
                  label="Fecha"
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
            </Grid>
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default PartidasDetalles;
