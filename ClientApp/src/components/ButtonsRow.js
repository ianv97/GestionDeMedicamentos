import React from "react";
import Grid from "@material-ui/core/Grid";
import Button from "@material-ui/core/Button";
import Box from "@material-ui/core/Box";

class ButtonsRow extends React.Component {
  render() {
    return (
      <Grid container direction="row" justify="center" className="mt-3">
        {this.props.mode === "read" ? (
          <Grid item>
            <Box component="div" display="none">
              <Button type="submit">.</Button>
            </Box>
            <Button
              variant="contained"
              color="secondary"
              onClick={() =>
                this.props.history.push({
                  pathname: this.props.id,
                  search: "?mode=delete"
                })
              }
            >
              Eliminar
            </Button>
            <Button
              variant="contained"
              className="bg-warning ml-4"
              onClick={() =>
                this.props.history.push({
                  pathname: this.props.id,
                  search: "?mode=update"
                })
              }
            >
              Editar
            </Button>
          </Grid>
        ) : this.props.mode === "create" || this.props.mode === "update" ? (
          <Grid item>
            <Button type="submit" variant="contained" className="bg-success">
              Guardar
            </Button>
          </Grid>
        ) : (
          <Grid item>
            <Button type="submit" variant="contained" color="secondary">
              Confirmar eliminación
            </Button>
          </Grid>
        )}
      </Grid>
    );
  }
}

export default ButtonsRow;
