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
        this.setState({ mounted: false });
        document.cookie = "token=" + data.token + "; expireAt=" + data.expireAt + "; path=/;";
        this.props.history.push("/inicio");
      });
  } catch (error) {
    window.container.error("Usuario y/o contraseña incorrectos", "Error", {
      showAnimation: "animated rubberBand",
      hideAnimation: "animated flipOutX",
      timeOut: 7000,
      extendedTimeOut: 2000
    });
  }
}
