export default async function getData() {
  this.setState({ error: null });
  try {
    let apiUrl =
      window.ApiUrl +
      this.state.currentUrl +
      "?order=" +
      this.state.order +
      "&pageSize=" +
      this.state.pageSize +
      "&pageNumber=" +
      this.state.pageNumber;
    let response;
    this.state.search.id
      ? (response = await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.state.search.id))
      : this.state.searchString
      ? (response = await fetch(apiUrl + this.state.searchString))
      : (response = await fetch(apiUrl));
    if (!response.ok) {
      throw Error(response.status + " " + response.statusText);
    }

    let data = await response.json();
    if (!Array.isArray(data)) {
      data = [data];
    }
    let displayData = [];
    let tempDisplayData = [];
    data.forEach(entity => {
      tempDisplayData = [];
      this.state.titles.forEach(value => tempDisplayData.push([eval("entity." + value[1])]));
      displayData.push(tempDisplayData);
    });

    this.setState({
      data: displayData,
      page: response.headers.get("page"),
      totalRecords: parseInt(response.headers.get("totalRecords"))
    });
  } catch (error) {
    this.setState({ error: error });
  } finally {
    this.setState({ loading: false });
  }
}
