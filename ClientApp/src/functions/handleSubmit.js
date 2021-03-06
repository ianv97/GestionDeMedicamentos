import getCookie from "./getCookie";
import signOut from "./signOut";

export default async function handleSubmit(event, nextUrl = this.state.currentUrl) {
  event.preventDefault();
  this.setState({ loading: true });
  var response;
  if (this.state.mode === "create") {
    response = await fetch(window.ApiUrl + this.state.currentUrl, {
      method: "POST",
      mode: "cors",
      body: JSON.stringify(this.state.form),
      headers: {
        Authorization: "BEARER " + getCookie("token"),
        "Content-Type": "application/json"
      }
    });
  } else if (this.state.mode === "update") {
    response = await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.props.match.params.id, {
      method: "PUT",
      mode: "cors",
      body: JSON.stringify(this.state.form),
      headers: {
        Authorization: "BEARER " + getCookie("token"),
        "Content-Type": "application/json"
      }
    });
  } else if (this.state.mode === "delete") {
    response = await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.props.match.params.id, {
      method: "DELETE",
      mode: "cors",
      headers: {
        Authorization: "BEARER " + getCookie("token"),
        "Content-Type": "application/json"
      }
    });
  }
  if (response.ok) {
    window.container.success("Acción realizada exitosamente", "Éxito", {
      showAnimation: "animated rubberBand",
      hideAnimation: "animated flipOutX",
      timeOut: 7000,
      extendedTimeOut: 2000
    });
  } else {
    if (response.status === 401) {
      signOut();
    }
    window.container.error(response.status + " " + response.statusText, "Error", {
      showAnimation: "animated rubberBand",
      hideAnimation: "animated flipOutX",
      timeOut: 7000,
      extendedTimeOut: 2000
    });
  }
  this.setState({ loading: false });
  this.props.history.push("/" + nextUrl);
}
