import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";
import OutlinedInput from "@material-ui/core/OutlinedInput";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import post from "../functions/post";
import put from "../functions/put";
import del from "../functions/delete";

class MedicamentosDetalles extends React.Component {
  state = {
    mode: "read",
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
  changeMode = changeMode.bind(this);

  async getData() {
    const response = await fetch(
      "http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos/" + this.props.match.params.id
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
  }

  async getDrugs() {
    const response = await fetch("http://medicamentos.us-east-1.elasticbeanstalk.com/api/drogas?order=name");
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
    }
    this.getDrugs();
    this.changeMode();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.changeMode());
  }

  handleChange = e => {
    this.setState({
      form: {
        ...this.state.form,
        [e.target.name]: e.target.value
      }
    });
  };

  handleSubmit = e => {
    e.preventDefault();
    if (this.state.mode === "create") {
      post(window.ApiUrl + "medicamentos", this.state.form);
      this.props.history.push("/Medicamentos");
    } else if (this.state.mode === "update") {
      put(window.ApiUrl + "medicamentos/" + this.props.match.params.id, this.state.form);
      this.setState({ mode: "read" });
      this.props.history.push("/Medicamentos/" + this.props.match.params.id);
    } else if (this.state.mode === "delete") {
      del(window.ApiUrl + "medicamentos/" + this.props.match.params.id);
      this.props.history.push("/Medicamentos");
    }
  };

  render() {
    return (
      <div>
        <Breadcrumbs currentUrl={"Medicamentos"} id={this.props.match.params.id} />

        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Medicamentos</h1>
            </Grid>
          </Grid>
          <form onSubmit={this.handleSubmit}>
            {this.state.mode !== "create" && (
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
                    readOnly: this.state.mode === "read" || this.state.mode === "delete"
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
                    readOnly: this.state.mode === "read" || this.state.mode === "delete"
                  }}
                />
              </Grid>
            </Grid>
            <Grid container direction="row" justify="center" spacing={5}>
              <Grid item className="mt-3">
                <FormControl required variant="outlined" style={{ minWidth: 210 }}>
                  <InputLabel htmlFor="drugId">Droga</InputLabel>
                  <Select
                    id="drugId"
                    name="drugId"
                    onChange={this.handleChange}
                    value={this.state.form.drugId}
                    input={<OutlinedInput labelWidth={55} />}
                    inputProps={{
                      readOnly: this.state.mode === "read" || this.state.mode === "delete"
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
                    readOnly: this.state.mode === "read" || this.state.mode === "delete"
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
                      readOnly: this.state.mode === "read" || this.state.mode === "delete"
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
            <ButtonsRow
              id={this.props.match.params.id}
              mode={this.state.mode}
              currentUrl="Medicamentos"
              location={this.props.location}
              history={this.props.history}
            />
          </form>
        </Grid>
      </div>
    );
  }
}

export default MedicamentosDetalles;
