export default function signOut(event) {
  event.preventDefault();
  document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
  this.props.history.push("/login");
}
