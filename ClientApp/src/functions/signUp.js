export default async function signUp() {
  try {
    await fetch(window.ApiUrl + "User", {
      method: "POST",
      mode: "cors",
      body: JSON.stringify(this.state.form),
      headers: {
        "Content-Type": "application/json"
      }
    }).then(response => {
      if (response.ok) {
        window.container.success(
          "Ya puede ingresar con su usuario y contraseña",
          "Usuario registrado con éxito",
          {
            showAnimation: "animated rubberBand",
            hideAnimation: "animated flipOutX",
            timeOut: 7000,
            extendedTimeOut: 2000
          }
        );
        this.setState({ mounted: false });
        // this.props.type = "signIn";
      } else {
        throw Error;
      }
    });
  } catch (error) {
    window.container.error("El usuario ingresado ya existe", "Error", {
      showAnimation: "animated rubberBand",
      hideAnimation: "animated flipOutX",
      timeOut: 7000,
      extendedTimeOut: 2000
    });
  }
}
