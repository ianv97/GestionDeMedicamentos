import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import Breadcrumbs from "@material-ui/core/Breadcrumbs";
import { Link } from "react-router-dom";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import Typography from "@material-ui/core/Typography";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";
import OutlinedInput from "@material-ui/core/OutlinedInput";
import Box from "@material-ui/core/Box";
import post from "../methods/post.js";
import put from "../methods/put.js";
import del from "../methods/delete.js";

class MedicamentosDetalles extends React.Component {
  state = {
    add: false,
    edit: false,
    delete: false,
    form: {
      id: 0,
      name: "",
      drugId: 0,
      proportion: 0,
      presentation: "",
      laboratory: "",
      stock: 0
    },
    drugs: []
  };

  async getData() {
    const response = await fetch(
      "http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos/" +
        this.props.match.params.id
    );
    const data = await response.json();
    this.setState({
      form: {
        id: data.id,
        name: data.name,
        drugId: data.drugId,
        proportion: data.proportion,
        presentation: data.presentation,
        laboratory: data.laboratory,
        stock: data.stock
      }
    });
    this.getDrugs();
  }

  async getDrugs() {
    const response = await fetch(
      "http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas?order=name"
    );
    const data = await response.json();
    data.forEach(drug => {
      this.setState({
        drugs: {
          ...this.state.drugs,
          [drug.id]: drug.name
        }
      });
    });
  }

  componentDidMount() {
    if (this.props.match.params.id !== "Añadir") {
      this.getData();
    } else {
      this.getDrugs();
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
        "/Medicamentos/" + this.props.match.params.id + "?edit=true"
      );
    }
  };

  delete = e => {
    const params = new URLSearchParams(this.props.location.search);
    if (!params.get("delete")) {
      this.props.history.push(
        "/Medicamentos/" + this.props.match.params.id + "?delete=true"
      );
    }
  };

  handleSubmit = e => {
    e.preventDefault();
    console.log(this.state.form);
    if (this.state.add) {
      post(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos",
        this.state.form
      );
      this.props.history.push("/Medicamentos");
    } else if (this.state.edit) {
      put(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos/" +
          this.props.match.params.id,
        this.state.form
      );
      this.setState({ edit: false });
      this.props.history.push("/Medicamentos/" + this.props.match.params.id);
    } else if (this.state.delete) {
      del(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos/" +
          this.props.match.params.id
      );
      this.props.history.push("/Medicamentos");
    }
  };

  render() {
    return (
      <div>
        <Breadcrumbs separator={<NavigateNextIcon fontSize="small" />}>
          <Link color="inherit" to="/">
            Gestión de medicamentos
          </Link>
          <Link color="inherit" to="/Medicamentos">
            Medicamentos
          </Link>
          <Typography color="textPrimary">
            {this.props.match.params.id}
          </Typography>
        </Breadcrumbs>

        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Medicamentos</h1>
            </Grid>
          </Grid>
          <form onSubmit={this.handleSubmit}>
            {this.state.add ? null : (
              <Grid container direction="row" justify="center" spacing={5}>
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
            <Grid container direction="row" justify="center" spacing={5}>
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
              <Grid item>
                <TextField
                  label="Laboratorio"
                  margin="normal"
                  variant="outlined"
                  name="laboratory"
                  onChange={this.handleChange}
                  value={this.state.form.laboratory}
                  InputProps={{
                    readOnly: !this.state.edit && !this.state.add
                  }}
                />
              </Grid>
            </Grid>
            <Grid container direction="row" justify="center" spacing={5}>
              <Grid item className="mt-3">
                <FormControl
                  required
                  variant="outlined"
                  style={{ minWidth: 210 }}
                >
                  <InputLabel htmlFor="drugId">Droga</InputLabel>
                  <Select
                    id="drugId"
                    name="drugId"
                    onChange={this.handleChange}
                    value={this.state.form.drugId}
                    input={<OutlinedInput labelWidth={55} />}
                    inputProps={{
                      readOnly: !this.state.edit && !this.state.add
                    }}
                  >
                    {Object.keys(this.state.drugs).map(key => {
                      return (
                        <MenuItem key={key} value={key}>
                          {this.state.drugs[key]}
                        </MenuItem>
                      );
                    })}
                  </Select>
                </FormControl>
              </Grid>
              <Grid item>
                <TextField
                  label="Proporción (mg)"
                  margin="normal"
                  variant="outlined"
                  name="proportion"
                  onChange={this.handleChange}
                  value={this.state.form.proportion}
                  InputProps={{
                    readOnly: !this.state.edit && !this.state.add
                  }}
                />
              </Grid>
            </Grid>
            <Grid container direction="row" justify="center" spacing={5}>
              <Grid item className="mt-3">
                <FormControl variant="outlined" style={{ minWidth: 210 }}>
                  <InputLabel htmlFor="presentation">Presentación</InputLabel>
                  <Select
                    id="presentation"
                    name="presentation"
                    onChange={this.handleChange}
                    value={this.state.form.presentation}
                    input={<OutlinedInput labelWidth={95} />}
                    inputProps={{
                      readOnly: !this.state.edit && !this.state.add
                    }}
                  >
                    <MenuItem value="Inyectable">Inyectable</MenuItem>
                    <MenuItem value="Jarabe">Jarabe</MenuItem>
                    <MenuItem value="Píldora">Píldora</MenuItem>
                    <MenuItem value="Comprimido">Comprimido</MenuItem>
                  </Select>
                </FormControl>
              </Grid>
              <Grid item>
                <TextField
                  label="En stock"
                  margin="normal"
                  variant="outlined"
                  name="stock"
                  onChange={this.handleChange}
                  value={this.state.form.stock}
                  InputProps={{
                    readOnly: true
                  }}
                />
              </Grid>
            </Grid>
            <Grid container direction="row" justify="center" spacing={10}>
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

export default MedicamentosDetalles;
