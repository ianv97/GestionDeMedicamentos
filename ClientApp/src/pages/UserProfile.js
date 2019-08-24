import React from "react";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import handleSubmit from "../functions/handleSubmit";
import handleChange from "../functions/handleChange";
import getCookie from "../functions/getCookie";
import Avatar from "../components/Avatar";

class UserProfile extends React.Component {
  state = {
    currentUrl: "users",
    mode: "read",
    loading: false,
    form: {
      id: "",
      username: "",
      name: "",
      role: ""
    }
  };
  changeMode = changeMode.bind(this);
  handleSubmit = handleSubmit.bind(this);
  handleChange = handleChange.bind(this);

  async getData() {
    try {
      await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.props.match.params.id, {
        headers: {
          Authorization: "BEARER " + getCookie("token")
        }
      })
        .then(response => {
          if (response.ok) {
            return response.json();
          } else {
            throw Error(response.status + " " + response.statusText);
          }
        })
        .then(data => {
          this.setState({
            form: {
              id: data.id,
              username: data.username,
              name: data.name,
              role: data.role.name
            }
          });
        });
    } catch (error) {
      window.container.error(error.message, "Error", {
        showAnimation: "animated rubberBand",
        hideAnimation: "animated flipOutX",
        timeOut: 7000,
        extendedTimeOut: 2000
      });
    }
  }

  componentDidMount() {
    if (this.props.match.params.id !== "añadir") {
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
        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Perfil</h1>
            </Grid>
          </Grid>
          <Avatar />
          <form
            onSubmit={e => this.handleSubmit(e, this.state.currentUrl + "/" + this.props.match.params.id)}
          >
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
                  label="Rol"
                  margin="normal"
                  variant="outlined"
                  name="role"
                  value={this.state.form.role}
                  InputProps={{ readOnly: true }}
                />
              </Grid>
            </Grid>
            <ButtonsRow
              id={this.props.match.params.id}
              mode={this.state.mode}
              history={this.props.history}
              delete={false}
              loading={this.state.loading}
            />
          </form>
        </Grid>
      </div>
    );
  }
}

export default UserProfile;
