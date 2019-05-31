import React from "react";
import { withStyles, makeStyles } from "@material-ui/core/styles";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import Paper from "@material-ui/core/Paper";
import Fab from "@material-ui/core/Fab";
import Icon from "@material-ui/core/Icon";
import { Link } from "react-router-dom";

const StyledTableCell = withStyles(theme => ({
  head: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
    fontSize: 15
  },
  body: {
    fontSize: 14
  }
}))(TableCell);

const StyledTableRow = withStyles(theme => ({
  root: {
    "&:nth-of-type(odd)": {
      backgroundColor: theme.palette.background.default
    }
  }
}))(TableRow);

const useStyles = makeStyles(theme => ({
  root: {
    width: "100%",
    marginTop: theme.spacing(3),
    overflowX: "auto"
  },
  table: {
    minWidth: 700
  }
}));

function CustomizedTables(props) {
  const classes = useStyles();

  return (
    <Paper className={classes.root}>
      <Table className={classes.table}>
        <TableHead>
          <TableRow>
            {props.titles.map(title => (
              <StyledTableCell align="center">{title}</StyledTableCell>
            ))}
            <StyledTableCell align="center">Detalles</StyledTableCell>
            <StyledTableCell align="center">Editar</StyledTableCell>
            <StyledTableCell align="center">Eliminar</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {props.data.map(row => (
            <StyledTableRow key={row[0]}>
              {row.map(cell => (
                <StyledTableCell align="center">{cell}</StyledTableCell>
              ))}
              <StyledTableCell align="center">
                <Link to={props.currentUrl + "/" + row[0]}>
                  <Fab size="small">
                    <Icon className="fas fa-search" />
                  </Fab>
                </Link>
              </StyledTableCell>
              <StyledTableCell align="center">
                <Link to={props.currentUrl + "/" + row[0]}>
                  <Fab className="bg-warning" size="small">
                    <Icon>edit_icon</Icon>
                  </Fab>
                </Link>
              </StyledTableCell>
              <StyledTableCell align="center">
                <Link to={props.currentUrl + "/" + row[0]}>
                  <Fab color="secondary" size="small">
                    <Icon>delete_icon</Icon>
                  </Fab>
                </Link>
              </StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </Paper>
  );
}

export default CustomizedTables;
