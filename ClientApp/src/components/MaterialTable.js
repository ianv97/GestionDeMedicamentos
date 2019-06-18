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
import CircularProgress from "@material-ui/core/CircularProgress";

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
              <StyledTableCell key={title} align="center">
                {title}
              </StyledTableCell>
            ))}
            <StyledTableCell align="center">Detalles</StyledTableCell>
            {props.edit === undefined && <StyledTableCell align="center">Editar</StyledTableCell>}
            <StyledTableCell align="center">Eliminar</StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {props.loading ? (
            <StyledTableRow>
              <StyledTableCell align="center">
                <CircularProgress />
              </StyledTableCell>
            </StyledTableRow>
          ) : props.error ? (
            <StyledTableRow>
              <StyledTableCell align="center">Error {props.error.message}</StyledTableCell>
            </StyledTableRow>
          ) : (
            props.data.map(row => (
              <StyledTableRow key={row[0]}>
                {row.map(cell => (
                  <StyledTableCell key={cell} align="center">
                    {cell}
                  </StyledTableCell>
                ))}
                <StyledTableCell align="center">
                  <Link to={props.currentUrl + "/" + row[0] + "?mode=read"}>
                    <Fab size="small">
                      <Icon className="fas fa-search" />
                    </Fab>
                  </Link>
                </StyledTableCell>
                {props.edit === undefined && (
                  <StyledTableCell align="center">
                    <Link to={props.currentUrl + "/" + row[0] + "?mode=update"}>
                      <Fab className="bg-warning" size="small">
                        <Icon>edit_icon</Icon>
                      </Fab>
                    </Link>
                  </StyledTableCell>
                )}
                <StyledTableCell align="center">
                  <Link to={props.currentUrl + "/" + row[0] + "?mode=delete"}>
                    <Fab color="secondary" size="small">
                      <Icon>delete_icon</Icon>
                    </Fab>
                  </Link>
                </StyledTableCell>
              </StyledTableRow>
            ))
          )}
        </TableBody>
      </Table>
    </Paper>
  );
}

export default CustomizedTables;
