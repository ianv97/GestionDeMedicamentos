import React from "react";
import clsx from "clsx";
import { makeStyles, useTheme } from "@material-ui/core/styles";
import Drawer from "@material-ui/core/Drawer";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import List from "@material-ui/core/List";
import CssBaseline from "@material-ui/core/CssBaseline";
import Typography from "@material-ui/core/Typography";
import Divider from "@material-ui/core/Divider";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import ChevronLeftIcon from "@material-ui/icons/ChevronLeft";
import ChevronRightIcon from "@material-ui/icons/ChevronRight";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import ListItemText from "@material-ui/core/ListItemText";
import { Link } from "react-router-dom";
import Button from "@material-ui/core/Button";

const drawerWidth = 220;

const useStyles = makeStyles(theme => ({
  root: {
    display: "flex"
  },
  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen
    })
  },
  appBarShift: {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(["width", "margin"], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen
    })
  },
  menuButton: {
    marginRight: 25
  },
  hide: {
    display: "none"
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
    whiteSpace: "nowrap"
  },
  drawerOpen: {
    width: drawerWidth,
    transition: theme.transitions.create("width", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen
    })
  },
  drawerClose: {
    transition: theme.transitions.create("width", {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen
    }),
    overflowX: "hidden",
    width: theme.spacing(7) + 1,
    [theme.breakpoints.up("sm")]: {
      width: theme.spacing(9) + 1
    }
  },
  toolbar: {
    display: "flex",
    alignItems: "center",
    justifyContent: "flex-end",
    padding: "0 8px",
    ...theme.mixins.toolbar
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing(3)
  },
  active: {
    backgroundColor: "#3f51b5",
    color: "#fff"
  },
  title: {
    flexGrow: 1
  }
}));

function Navbar(props) {
  const classes = useStyles();
  const theme = useTheme();
  const [open, setOpen] = React.useState(false);
  const [active, setActive] = React.useState();

  function handleDrawerOpen() {
    setOpen(true);
  }

  function handleDrawerClose() {
    setOpen(false);
  }

  const onSelectHandler = key => {
    setActive(key);
  };

  return (
    <div className={classes.root}>
      <CssBaseline />
      <AppBar
        position="fixed"
        className={clsx(classes.appBar, {
          [classes.appBarShift]: open
        })}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="Open drawer"
            onClick={handleDrawerOpen}
            edge="start"
            className={clsx(classes.menuButton, {
              [classes.hide]: open
            })}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" noWrap className={classes.title}>
            Gestión de medicamentos
          </Typography>
          <Button variant="contained" color="secondary">
            Cerrar Sesión
          </Button>
        </Toolbar>
      </AppBar>
      <Drawer
        variant="permanent"
        className={clsx(classes.drawer, {
          [classes.drawerOpen]: open,
          [classes.drawerClose]: !open
        })}
        classes={{
          paper: clsx({
            [classes.drawerOpen]: open,
            [classes.drawerClose]: !open
          })
        }}
        open={open}
      >
        <div className={classes.toolbar}>
          <IconButton onClick={handleDrawerClose}>
            {theme.direction === "rtl" ? <ChevronRightIcon /> : <ChevronLeftIcon />}
          </IconButton>
        </div>
        <Divider />
        <List>
          <ListItem
            button
            component={Link}
            to="/drogas"
            onClick={e => onSelectHandler(1)}
            className={active === 1 ? clsx(classes.active) : null}
          >
            <ListItemIcon>
              <i className="fas fa-2x fa-prescription-bottle-alt" />
            </ListItemIcon>
            <ListItemText primary="Drogas" />
          </ListItem>
          <ListItem
            button
            component={Link}
            to="/medicamentos"
            onClick={e => onSelectHandler(2)}
            className={active === 2 ? clsx(classes.active) : null}
          >
            <ListItemIcon>
              <i className="fas fa-2x fa-pills" />
            </ListItemIcon>
            <ListItemText primary="Medicamentos" />
          </ListItem>
          <ListItem
            button
            component={Link}
            to="/reposiciones"
            onClick={e => onSelectHandler(3)}
            className={active === 3 ? clsx(classes.active) : null}
          >
            <ListItemIcon>
              <i className="fas fa-2x fa-sign-in-alt" />
            </ListItemIcon>
            <ListItemText primary="Reposiciones" />
          </ListItem>
          <ListItem
            button
            component={Link}
            to="/partidas"
            onClick={e => onSelectHandler(4)}
            className={active === 4 ? clsx(classes.active) : null}
          >
            <ListItemIcon>
              <i className="fas fa-2x fa-sign-out-alt" />
            </ListItemIcon>
            <ListItemText primary="Partidas" />
          </ListItem>
          <ListItem
            button
            component={Link}
            to="/stock"
            onClick={e => onSelectHandler(5)}
            className={active === 5 ? clsx(classes.active) : null}
          >
            <ListItemIcon>
              <i className="fas fa-2x fa-clipboard-list" />
            </ListItemIcon>
            <ListItemText primary="Stock" />
          </ListItem>
        </List>
      </Drawer>
      <main className={classes.content}>
        <div className={classes.toolbar} />
        {props.children}
      </main>
    </div>
  );
}

export default Navbar;
