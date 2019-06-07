import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";
import ButtonsRow from "../components/ButtonsRow";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";
import OutlinedInput from "@material-ui/core/OutlinedInput";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import changeMode from "../functions/changeMode";
import post from "../functions/post";
import put from "../functions/put";
import del from "../functions/delete";

class ReposicionesDetalles extends React.Component {
  state = {
    add: false,
    edit: false,
    delete: false,
    medicines: [],
    medicinePurchases: [{ medicineId: 0, quantity: 0 }],
    form: { id: 0, date: "" }
  };
  changeMode = changeMode.bind(this);

  async getData() {
    const response = await fetch(
      "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones/" +
        this.props.match.params.id
    );
    const data = await response.json();
    this.setState({
      form: {
        id: data.id,
        nombre: data.date
      }
    });
    this.getMedicines();
  }

  async getMedicines() {
    const response = await fetch(
      "http://medicamentos.us-east-1.elasticbeanstalk.com/api/medicamentos?order=name"
    );
    const data = await response.json();
    data.forEach(medicine => {
      this.setState({
        medicines: {
          ...this.state.medicines,
          [medicine.id]: medicine.name
        }
      });
    });
  }

  componentDidMount() {
    if (this.props.match.params.id !== "Añadir") {
      this.getData();
    } else {
      this.getMedicines();
    }
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

  handleRowChange = (e, index) => {
    console.log(index);
    let { name, value } = e.target;
    let { medicinePurchases } = this.state;
    console.log(medicinePurchases);
    if (name === "medicineId") {
      medicinePurchases[index].medicineId = value;
    } else {
      medicinePurchases[index].quantity = value;
    }
    this.setState({
      medicinePurchases
    });
  };

  addNewRow = () => {
    let { medicinePurchases } = this.state;
    medicinePurchases.push({ medicineId: 0, quantity: 0 });
    this.setState({ medicinePurchases });
  };

  handleSubmit = e => {
    e.preventDefault();
    if (this.state.add) {
      post(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones",
        this.state.form
      );
      this.props.history.push("/Reposiciones");
    } else if (this.state.edit) {
      put(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones/" +
          this.props.match.params.id,
        this.state.form
      );
      this.setState({ edit: false });
      this.props.history.push("/Reposiciones/" + this.props.match.params.id);
    } else if (this.state.delete) {
      del(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones/" +
          this.props.match.params.id
      );
      this.props.history.push("/Reposiciones");
    }
  };

  render() {
    let ReposicionesFila = this.state.medicinePurchases.map(
      (elemento, index) => {
        return (
          <Grid
            key={"FI" + index}
            container
            direction="row"
            justify="center"
            spacing={5}
          >
            <Grid item className="mt-3">
              <FormControl
                required
                variant="outlined"
                style={{ minWidth: 210 }}
              >
                <InputLabel htmlFor="medicineId">Medicamento</InputLabel>
                <Select
                  id="medicineId"
                  name="medicineId"
                  onChange={e => this.handleRowChange(e, index)}
                  value={elemento.medicineId}
                  input={<OutlinedInput labelWidth={110} />}
                  inputProps={{
                    readOnly:
                      this.state.mode === "read" || this.state.mode === "delete"
                  }}
                >
                  {Object.keys(this.state.medicines).map(key => {
                    return (
                      <MenuItem key={key} value={key}>
                        {this.state.medicines[key]}
                      </MenuItem>
                    );
                  })}
                </Select>
              </FormControl>
            </Grid>
            <Grid item>
              <TextField
                required
                type="number"
                label="Cantidad"
                margin="normal"
                variant="outlined"
                name="quantity"
                onChange={e => this.handleRowChange(e, index)}
                value={elemento.quantity}
                InputProps={{
                  readOnly:
                    this.state.mode === "read" || this.state.mode === "delete"
                }}
              />
            </Grid>
          </Grid>
        );
      }
    );
    return (
      <div>
        <Breadcrumbs
          currentUrl={"Reposiciones"}
          id={this.props.match.params.id}
        />

        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Reposiciones</h1>
            </Grid>
          </Grid>
          <form onSubmit={this.handleSubmit}>
            {this.state.mode !== "create" && (
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
                  type="date"
                  label="Fecha"
                  margin="normal"
                  variant="outlined"
                  name="date"
                  onChange={this.handleChange}
                  value={this.state.form.date}
                  InputProps={{
                    readOnly:
                      this.state.mode === "read" || this.state.mode === "delete"
                  }}
                  InputLabelProps={{ shrink: true }}
                />
              </Grid>
            </Grid>
            {ReposicionesFila}
            <Grid container direction="row" justify="center" spacing={5}>
              <Grid item>
                <Fab onClick={this.addNewRow} color="primary" size="medium">
                  <AddIcon />
                </Fab>
              </Grid>
            </Grid>
            <ButtonsRow
              id={this.props.match.params.id}
              mode={this.state.mode}
              history={this.props.history}
            />
          </form>
        </Grid>
      </div>
    );
  }
}

export default ReposicionesDetalles;
