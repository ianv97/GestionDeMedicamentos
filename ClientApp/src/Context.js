import React, { createContext, useState } from "react";
const Context = createContext();

const Provider = ({ children }) => {
  const [isAuth, setAuth] = useState(false);
  const [token, setToken] = useState("");

  const value = {
    isAuth,
    setAuth,
    token,
    setToken
  };
  return <Context.Provider value={value}>{children}</Context.Provider>;
};

export default { Context, Provider, Consumer: Context.Consumer };
