export default async function login() {
  try {
    await fetch(window.ApiUrl + "Auth", {
      method: "POST",
      mode: "cors",
      body: JSON.stringify(this.state.form),
      headers: {
        "Content-Type": "application/json"
      }
    })
      .then(response => {
        if (response.ok) {
          return response.json();
        } else {
          throw Error;
        }
      })
      .then(data => {
        window.container.success("Bienvenido " + data.user.name, "Sesión iniciada", {
          showAnimation: "animated rubberBand",
          hideAnimation: "animated flipOutX",
          timeOut: 7000,
          extendedTimeOut: 2000
        });
        this.setState({ loading: false, mounted: false });
        console.log(data.expireAt);
        document.cookie = "token=" + data.token + "; expires=" + new Date(data.expireAt).toUTCString() + ";";
        document.cookie = "id=" + data.user.id + "; expires=" + new Date(data.expireAt).toUTCString() + ";";
        document.cookie =
          "username=" + data.user.username + "; expires=" + new Date(data.expireAt).toUTCString() + ";";
        this.props.history.push("/inicio");
      });
  } catch (error) {
    window.container.error("Usuario y/o contraseña incorrectos", "Error", {
      showAnimation: "animated rubberBand",
      hideAnimation: "animated flipOutX",
      timeOut: 7000,
      extendedTimeOut: 2000
    });
    this.setState({ loading: false });
  }
}
