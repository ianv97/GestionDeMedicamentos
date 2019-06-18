import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import post from "../functions/post";
import put from "../functions/put";
import del from "../functions/delete";

class DrogasDetalles extends React.Component {
  state = { mode: "read", form: { id: 0, name: "" } };
  changeMode = changeMode.bind(this);

  async getData() {
    const response = await fetch(window.ApiUrl + "drogas/" + this.props.match.params.id);
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
      post(window.ApiUrl + "drogas", this.state.form);
      this.props.history.push("/Drogas");
    } else if (this.state.mode === "update") {
      put(window.ApiUrl + "drogas/" + this.props.match.params.id, this.state.form);
      this.setState({ mode: "read" });
      this.props.history.push("/Drogas/" + this.props.match.params.id + "?mode=read");
    } else if (this.state.mode === "delete") {
      del(window.ApiUrl + "drogas/" + this.props.match.params.id);
      this.props.history.push("/Drogas");
    }
  };

  render() {
    return (
      <div>
        <Breadcrumbs currentUrl={"Drogas"} id={this.props.match.params.id} />

        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Drogas</h1>
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
            </Grid>
            <ButtonsRow id={this.props.match.params.id} mode={this.state.mode} history={this.props.history} />
          </form>
        </Grid>
      </div>
    );
  }
}

export default DrogasDetalles;
