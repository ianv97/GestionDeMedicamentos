import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";
import InputRow from "../components/InputRow";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import post from "../functions/post";
import del from "../functions/delete";

class PartidasDetalles extends React.Component {
  state = {
    mode: "read",
    medicines: [],
    form: {
      id: 0,
      date: "",
      medicinePrescriptions: [{ medicineId: 0, quantity: 0 }]
    }
  };
  changeMode = changeMode.bind(this);

  async getData() {
    const response = await fetch(window.ApiUrl + "partidas/" + this.props.match.params.id);
    const data = await response.json();
    this.setState({
      form: {
        id: data.id,
        date: data.date,
        medicinePrescriptions: data.medicinePrescriptions
      }
    });
  }

  async getMedicines() {
    const response = await fetch(window.ApiUrl + "medicamentos?order=name");
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
    }
    this.getMedicines();
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
    let { name, value } = e.target;
    let { medicinePrescriptions } = this.state.form;
    medicinePrescriptions[index][name] = value;

    this.setState({
      form: {
        ...this.state.form,
        medicinePrescriptions
      }
    });
  };

  handleSubmit = e => {
    e.preventDefault();
    if (this.state.mode === "delete") {
      del(window.ApiUrl + "partidas/" + this.props.match.params.id);
      this.props.history.push("/partidas");
    }
  };

  render() {
    return (
      <div>
        <Breadcrumbs currentUrl={"Partidas"} id={this.props.match.params.id} />

        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Partidas</h1>
            </Grid>
          </Grid>
          <form onSubmit={this.handleSubmit}>
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
            <Grid container direction="row" justify="center">
              <Grid item>
                <TextField
                  required
                  type="datetime-local"
                  label="Fecha"
                  margin="normal"
                  variant="outlined"
                  name="date"
                  onChange={this.handleChange}
                  value={this.state.form.date}
                  InputProps={{
                    readOnly: this.state.mode === "read" || this.state.mode === "delete"
                  }}
                  InputLabelProps={{ shrink: true }}
                />
              </Grid>
            </Grid>
            {this.state.form.medicinePrescriptions.map((element, index) => (
              <InputRow
                key={index}
                mode={this.state.mode}
                element={element}
                medicines={this.state.medicines}
                handleChange={e => this.handleRowChange(e, index)}
              />
            ))}
            <ButtonsRow
              id={this.props.match.params.id}
              mode={this.state.mode}
              history={this.props.history}
              edit={false}
            />
          </form>
        </Grid>
      </div>
    );
  }
}

export default PartidasDetalles;
