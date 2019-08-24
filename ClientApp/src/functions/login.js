export default async function login(sessionRecord) {
  try {
    await fetch(window.ApiUrl + "auth", {
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
        if (sessionRecord) {
          const expires = new Date(data.expireAt).toUTCString();
          document.cookie = `token=${data.token}; expires=${expires};`;
          document.cookie = `id=${data.user.id}; expires=${expires};`;
          document.cookie = `username=${data.user.username}; expires=${expires};`;
        } else {
          document.cookie = `token=${data.token};`;
          document.cookie = `id=${data.user.id};`;
          document.cookie = `username=${data.user.username};`;
        }
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
