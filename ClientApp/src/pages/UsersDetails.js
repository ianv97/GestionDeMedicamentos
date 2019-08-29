import React from "react";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import handleSubmit from "../functions/handleSubmit";
import handleChange from "../functions/handleChange";
import getCookie from "../functions/getCookie";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";
import OutlinedInput from "@material-ui/core/OutlinedInput";

class UsersDetails extends React.Component {
  state = {
    currentUrl: "users",
    mode: "read",
    loading: false,
    roles: [],
    form: {
      id: 0,
      name: "",
      username: "",
      password: "",
      roleId: ""
    }
  };
  changeMode = changeMode.bind(this);
  handleSubmit = handleSubmit.bind(this);
  handleChange = handleChange.bind(this);

  async getData() {
    const response = await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.props.match.params.id, {
      headers: {
        Authorization: "BEARER " + getCookie("token")
      }
    });
    const data = await response.json();
    this.setState({
      form: {
        id: data.id,
        name: data.name,
        username: data.username,
        roleId: data.roleId,
        roleName: data.role.name
      }
    });
  }

  async getRoles() {
    const response = await fetch(window.ApiUrl + "roles?order=name", {
      headers: {
        Authorization: "BEARER " + getCookie("token")
      }
    });
    const data = await response.json();
    data.forEach(role => {
      this.setState({
        roles: {
          ...this.state.roles,
          [role.id]: role.name
        }
      });
    });
  }

  componentDidMount() {
    if (this.props.match.params.id !== "añadir") {
      this.getData();
    }
    this.getRoles();
    this.changeMode();
  }

  componentDidUpdate() {
    this.props.history.listen(location => this.changeMode());
  }

  render() {
    return (
      <div>
        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Usuarios</h1>
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
            <Grid container direction="row" justify="center">
              <Grid item>
                <TextField
                  required
                  label="Usuario"
                  margin="normal"
                  variant="outlined"
                  name="username"
                  onChange={this.handleChange}
                  value={this.state.form.username}
                  InputProps={{
                    readOnly: this.state.mode === "read" || this.state.mode === "delete"
                  }}
                />
              </Grid>
            </Grid>
            {this.state.mode === "create" && (
              <Grid container direction="row" justify="center">
                <Grid item>
                  <TextField
                    required
                    label="Contraseña"
                    margin="normal"
                    variant="outlined"
                    name="password"
                    onChange={this.handleChange}
                    value={this.state.form.password}
                  />
                </Grid>
              </Grid>
            )}
            {this.state.mode === "update" && (
              <Grid container direction="row" justify="center">
                <Grid item>
                  <TextField
                    label="Nueva contraseña"
                    margin="normal"
                    variant="outlined"
                    name="password"
                    onChange={this.handleChange}
                    value={this.state.form.password}
                  />
                </Grid>
              </Grid>
            )}
            <Grid container direction="row" justify="center" spacing={5}>
              <Grid item className="mt-3">
                <FormControl required variant="outlined" style={{ minWidth: 210 }}>
                  <InputLabel htmlFor="roleId">Rol</InputLabel>
                  <Select
                    id="roleId"
                    name="roleId"
                    onChange={this.handleChange}
                    value={this.state.form.roleId}
                    input={<OutlinedInput labelWidth={40} />}
                    inputProps={{
                      readOnly: this.state.mode === "read" || this.state.mode === "delete"
                    }}
                  >
                    {Object.keys(this.state.roles).map(key => {
                      return (
                        <MenuItem key={key} value={key}>
                          {this.state.roles[key]}
                        </MenuItem>
                      );
                    })}
                  </Select>
                </FormControl>
              </Grid>
            </Grid>
            <ButtonsRow
              id={this.props.match.params.id}
              mode={this.state.mode}
              history={this.props.history}
              loading={this.state.loading}
            />
          </form>
        </Grid>
      </div>
    );
  }
}

export default UsersDetails;
