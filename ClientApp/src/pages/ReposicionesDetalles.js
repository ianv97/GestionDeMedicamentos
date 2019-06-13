import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";
import Fila from "../components/Fila";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import post from "../functions/post";
import put from "../functions/put";
import del from "../functions/delete";

class ReposicionesDetalles extends React.Component {
  state = {
    mode: "read",
    medicines: [],
    form: {
      id: 0,
      date: "",
      medicinePurchases: [{ medicineId: 0, quantity: 0 }]
    }
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
    let { medicinePurchases } = this.state.form;
    medicinePurchases[index][name] = value;

    this.setState({
      form: {
        ...this.state.form,
        medicinePurchases
      }
    });
  };

  addNewRow = () => {
    let { medicinePurchases } = this.state.form;
    medicinePurchases.push({ medicineId: 0, quantity: 0 });
    this.setState({
      form: {
        ...this.state.form,
        medicinePurchases
      }
    });
  };

  handleSubmit = e => {
    e.preventDefault();
    if (this.state.mode === "create") {
      post(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones",
        this.state.form
      );
      this.props.history.push("/Reposiciones");
    } else if (this.state.mode === "update") {
      put(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones/" +
          this.props.match.params.id,
        this.state.form
      );
      this.setState({ edit: false });
      this.props.history.push("/Reposiciones/" + this.props.match.params.id);
    } else if (this.state.mode === "delete") {
      del(
        "http://medicamentos.us-east-1.elasticbeanstalk.com/api/reposiciones/" +
          this.props.match.params.id
      );
      this.props.history.push("/Reposiciones");
    }
  };

  render() {
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
            {this.state.form.medicinePurchases.map((element, index) => (
              <Fila
                key={index}
                mode={this.state.mode}
                medicinePurchase={element}
                medicines={this.state.medicines}
                handleChange={e => this.handleRowChange(e, index)}
              />
            ))}
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
