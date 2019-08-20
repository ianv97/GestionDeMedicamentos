import React, { createContext, useState } from "react";
// import getCookie from "./functions/getCookie";
const Context = createContext();

const Provider = ({ children }) => {
  const [img, setImg] = useState();
  // const isAuth = () => {
  //   if (getCookie("token") !== "") {
  //     return true;
  //   } else {
  //     return false;
  //   }
  // };

  const value = {
    img,
    setImg
  };
  return <Context.Provider value={value}>{children}</Context.Provider>;
};

export default { Context, Provider, Consumer: Context.Consumer };
