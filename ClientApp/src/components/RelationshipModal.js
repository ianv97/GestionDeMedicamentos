import React from "react";
import { Modal } from "react-bootstrap";
import MaterialTable from "../components/MaterialTable";

function RelationshipModal(props) {
  const [error, setError] = React.useState();
  const [loading, setLoading] = React.useState();

  //   async function getData(search) {
  //     setError(null);
  //     try {
  //       let apiUrl =
  //         window.ApiUrl +
  //         props.currentUrl +
  //         "?order=" +
  //         props.order +
  //         "&pageSize=" +
  //         pageSize +
  //         "&pageNumber=" +
  //         pageNumber;
  //       let response;
  //       this.state.search.id
  //         ? (response = await fetch(window.ApiUrl + this.state.currentUrl + "/" + this.state.search.id))
  //         : search
  //         ? (response = await fetch(apiUrl + search))
  //         : (response = await fetch(apiUrl));
  //       if (!response.ok) {
  //         throw Error(response.status + " " + response.statusText);
  //       }

  //       let data = await response.json();
  //       if (!Array.isArray(data)) {
  //         data = [data];
  //       }
  //       const displayData = [];
  //       data.forEach(function(drug) {
  //         displayData.push([drug.id, drug.name]);
  //       });
  //       this.setState({
  //         data: displayData,
  //         page: response.headers.get("page"),
  //         totalRecords: parseInt(response.headers.get("totalRecords"))
  //       });
  //     } catch (error) {
  //       setEror(error);
  //     } finally {
  //       setLoading(false);
  //     }
  //   }

  return (
    <Modal {...props} size="lg" centered>
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">Modal heading</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        {/* <MaterialTable
          titles={[["ID", "id"], ["Nombre", "name"]]}
          data={props.data}
          currentUrl={props.currentUrl}
        /> */}
      </Modal.Body>
    </Modal>
  );
}

export default RelationshipModal;
