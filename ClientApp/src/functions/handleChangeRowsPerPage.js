export default function handleChangeRowsPerPage(event) {
  this.setState({ pageSize: +event.target.value }, () => {
    this.getData();
  });
}
