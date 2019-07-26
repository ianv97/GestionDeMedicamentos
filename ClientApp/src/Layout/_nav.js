export default {
  items: [
    {
      name: "Inicio",
      url: "/inicio",
      icon: "fas fa-home"
    },
    {
      title: true,
      name: "Hospital",
      wrapper: {
        // optional wrapper object
        element: "", // required valid HTML5 element tag
        attributes: {} // optional valid JS object with JS API naming ex: { className: "my-class", style: { fontFamily: "Verdana" }, id: "my-id"}
      },
      class: "text-center" // optional class names space delimited list for title item ex: "text-center"
    },
    {
      name: "Drogas",
      url: "/drogas",
      icon: "fas fa-prescription-bottle-alt"
    },
    {
      name: "Medicamentos",
      url: "/medicamentos",
      icon: "fas fa-pills"
    },
    {
      name: "Reposiciones",
      url: "/reposiciones",
      icon: "fas fa-sign-in-alt"
      // children: [
      //   {
      //     name: "Breadcrumbs",
      //     url: "/reposiciones/breadcrumbs",
      //     icon: "icon-puzzle"
      //   }
      // ]
    },
    {
      name: "Partidas",
      url: "/partidas",
      icon: "fas fa-sign-out-alt"
    },
    {
      name: "Stock",
      url: "/stock",
      icon: "fas fa-clipboard-list"
    }
  ]
};
