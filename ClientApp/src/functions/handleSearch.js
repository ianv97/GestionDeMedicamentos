export default function handleSearch(e) {
  this.setState(
    {
      search: {
        ...this.state.search,
        [e.target.name]: e.target.value
      }
    },
    () => {
      let search = "";
      for (var prop in this.state.search) {
        if (this.state.search[prop]) {
          search += "&" + prop + "=" + this.state.search[prop];
        }
      }
      this.getData(search);
    }
  );
}
