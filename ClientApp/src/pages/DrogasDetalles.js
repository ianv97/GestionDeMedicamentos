import React from "react";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import Breadcrumbs from "@material-ui/core/Breadcrumbs";
import { Link } from "react-router-dom";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import Typography from "@material-ui/core/Typography";
import Box from "@material-ui/core/Box";
import post from "../methods/post.js";
import put from "../methods/put.js";
import del from "../methods/delete.js";

class DrogasDetalles extends React.Component {
  state = { add: false, edit: false, delete: false, form: { id: 0, name: "" } };

  async getData() {
    const response = await fetch(
      "http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas/" +
        this.props.match.params.id
    );
    const data = await response.json();
    this.setState({
      form: {
        id: data.id,
        name: data.name
      }
    });
  }

  componentDidMount() {
    if (this.props.match.params.id !== "Añadir") {
      this.getData();
    }
    this.changeMode();
  }

  componentDidUpdate() {
    this.changeMode();
  }

  changeMode() {
    if (this.props.match.params.id === "Añadir") {
      if (!this.state.add) {
        this.setState({ add: true });
      }
    } else {
      const params = new URLSearchParams(this.props.location.search);
      if (params.get("edit") && !this.state.edit) {
        this.setState({ edit: true });
      } else if (params.get("delete") && !this.state.delete) {
        this.setState({ delete: true });
      }
    }
  }

  handleChange = e => {
    this.setState({
      form: {
        ...this.state.form,
        [e.target.name]: e.target.value
      }
    });
  };

  edit = e => {
    const params = new URLSearchParams(this.props.location.search);
    if (!params.get("edit")) {
      this.props.history.push(
        "/Drogas/" + this.props.match.params.id + "?edit=true"
      );
    }
  };

  delete = e => {
    const params = new URLSearchParams(this.props.location.search);
    if (!params.get("delete")) {
      this.props.history.push(
        "/Drogas/" + this.props.match.params.id + "?delete=true"
      );
    }
  };

  handleSubmit = e => {
    e.preventDefault();
    if (this.state.add) {
      post(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas",
        this.state.form
      );
      this.props.history.push("/Drogas");
    } else if (this.state.edit) {
      put(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas/" +
          this.props.match.params.id,
        this.state.form
      );
      this.setState({ edit: false });
      this.props.history.push("/Drogas/" + this.props.match.params.id);
    } else if (this.state.delete) {
      del(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas/" +
          this.props.match.params.id
      );
      this.props.history.push("/Drogas");
    }
  };

  render() {
    return (
      <div>
        <Breadcrumbs separator={<NavigateNextIcon fontSize="small" />}>
          <Link color="inherit" to="/">
            Gestión de medicamentos
          </Link>
          <Link color="inherit" to="/Drogas">
            Drogas
          </Link>
          <Typography color="textPrimary">
            {this.props.match.params.id}
          </Typography>
        </Breadcrumbs>

        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Drogas</h1>
            </Grid>
          </Grid>
          <form onSubmit={this.handleSubmit}>
            {this.state.add ? null : (
              <Grid container direction="row" justify="center" className="mt-3">
                <Grid item>
                  <TextField
                    label="ID"
                    margin="normal"
                    variant="outlined"
                    name="id"
                    value={this.state.form.id}
                    InputProps={{ readOnly: true }}
                  />
                </Grid>
              </Grid>
            )}
            <Grid container direction="row" justify="center">
              <Grid item>
                <TextField
                  required
                  label="Nombre"
                  margin="normal"
                  variant="outlined"
                  name="name"
                  onChange={this.handleChange}
                  value={this.state.form.name}
                  InputProps={{
                    readOnly: !this.state.edit && !this.state.add
                  }}
                />
              </Grid>
            </Grid>
            <Grid container direction="row" justify="center" className="mt-3">
              {this.state.add || this.state.edit || this.state.delete ? (
                this.state.add || this.state.edit ? (
                  <Grid item>
                    <Button
                      type="submit"
                      variant="contained"
                      className="bg-success"
                    >
                      Guardar
                    </Button>
                  </Grid>
                ) : (
                  <Grid item>
                    <Button type="submit" variant="contained" color="secondary">
                      Confirmar eliminación
                    </Button>
                  </Grid>
                )
              ) : (
                <Grid item>
                  <Box component="div" display="none">
                    <Button type="submit" />
                  </Box>
                  <Button
                    variant="contained"
                    color="secondary"
                    onClick={this.delete}
                  >
                    Eliminar
                  </Button>
                  <Button
                    variant="contained"
                    className="bg-warning ml-4"
                    onClick={this.edit}
                  >
                    Editar
                  </Button>
                </Grid>
              )}
            </Grid>
          </form>
        </Grid>
      </div>
    );
  }
}

export default DrogasDetalles;
