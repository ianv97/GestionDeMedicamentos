import React from "react";
import Grid from "@material-ui/core/Grid";
import Breadcrumbs from "../components/Breadcrumbs";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import RelationshipModal from "../components/RelationshipModal";
import Fab from "@material-ui/core/Fab";
import AddIcon from "@material-ui/icons/Add";
import ButtonsRow from "../components/ButtonsRow";
import changeMode from "../functions/changeMode";
import handleSubmit from "../functions/handleSubmit";

class StockDetalles extends React.Component {
  state = {
    currentUrl: "stock",
    mode: "read",
    form: {
      id: 0,
      date: "",
      medicineStockOrders: [{ medicineId: 0, medicineName: "", quantity: 0 }]
    },
    modalShow: [false]
  };
  changeMode = changeMode.bind(this);
  handleSubmit = handleSubmit.bind(this);

  async getData() {
    const response = await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.props.match.params.id);
    const data = await response.json();
    this.setState({
      form: {
        id: data.id,
        date: data.date,
        medicineStockOrders: data.medicineStockOrders
      }
    });
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

  handleChange = e => {
    this.setState({
      form: {
        ...this.state.form,
        [e.target.name]: e.target.value
      }
    });
  };

  addNewRow = () => {
    let { medicineStockOrders } = this.state.form;
    let { modalShow } = this.state;
    medicineStockOrders.push({ medicineId: 0, medicineName: "", quantity: 0 });
    modalShow.push(false);
    this.setState({
      form: {
        ...this.state.form,
        medicineStockOrders
      },
      modalShow
    });
  };

  selectRelation = (index, id, name) => {
    let { medicineStockOrders } = this.state.form;
    let { modalShow } = this.state;
    medicineStockOrders[index]["medicineId"] = id;
    medicineStockOrders[index]["medicineName"] = name;
    modalShow[index] = false;

    this.setState({
      form: {
        ...this.state.form,
        medicineStockOrders
      },
      modalShow
    });
  };

  handleRowChange = (e, index) => {
    let { name, value } = e.target;
    let { medicineStockOrders } = this.state.form;
    medicineStockOrders[index][name] = value;

    this.setState({
      form: {
        ...this.state.form,
        medicineStockOrders
      }
    });
  };

  render() {
    return (
      <div>
        <Breadcrumbs currentUrl={this.state.currentUrl} id={this.props.match.params.id} />

        <Grid container direction="column">
          <Grid container direction="row" justify="center" className="mt-5">
            <Grid item>
              <h1>Stock</h1>
            </Grid>
          </Grid>
          <form onSubmit={this.handleSubmit}>
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
                  type="datetime-local"
                  label="Fecha"
                  margin="normal"
                  variant="outlined"
                  name="date"
                  onChange={this.handleChange}
                  value={this.state.form.date}
                  InputProps={{
                    readOnly: this.state.mode === "read" || this.state.mode === "delete"
                  }}
                  InputLabelProps={{ shrink: true }}
                />
              </Grid>
            </Grid>
            {this.state.form.medicineStockOrders.map((element, index) => (
              <Grid container key={index} direction="row" justify="center" spacing={5}>
                <Grid item className="mt-3">
                  <TextField
                    required
                    label="Medicamento"
                    margin="none"
                    variant="outlined"
                    name="medicineName"
                    value={element.medicineName}
                    style={{ width: 145 }}
                    InputProps={{
                      readOnly: true
                    }}
                  />
                  <Button
                    disabled={this.props.mode === "read" || this.props.mode === "delete"}
                    className="mt-1 px-0"
                    size="large"
                    variant="contained"
                    color="primary"
                    onClick={() => {
                      let { modalShow } = this.state;
                      modalShow[index] = true;
                      this.setState({ modalShow });
                    }}
                  >
                    <i className="fas fa-2x fa-pills" />
                  </Button>
                  <RelationshipModal
                    show={this.state.modalShow[index]}
                    onHide={() => {
                      let { modalShow } = this.state;
                      modalShow[index] = false;
                      this.setState({ modalShow });
                    }}
                    entity={"Medicamentos"}
                    history={this.props.history}
                    selectRelation={(id, name) => this.selectRelation(index, id, name)}
                  />
                </Grid>
                <Grid item>
                  <TextField
                    required
                    type="number"
                    label="Cantidad"
                    margin="normal"
                    variant="outlined"
                    name="quantity"
                    onChange={e => this.handleRowChange(e, index)}
                    value={element.quantity}
                    InputProps={{
                      readOnly: this.state.mode === "read" || this.state.mode === "delete"
                    }}
                  />
                </Grid>
              </Grid>
            ))}
            {this.state.mode === "create" && (
              <Grid container direction="row" justify="center" spacing={5}>
                <Grid item>
                  <Fab onClick={this.addNewRow} color="primary" size="medium">
                    <AddIcon />
                  </Fab>
                </Grid>
              </Grid>
            )}
            <ButtonsRow
              id={this.props.match.params.id}
              mode={this.state.mode}
              history={this.props.history}
              edit={false}
            />
          </form>
        </Grid>
      </div>
    );
  }
}

export default StockDetalles;
