export default async function put(url, data) {
  return await fetch(url, {
    method: "PUT",
    mode: "cors",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json"
    }
  }).then(res => {
    return res;
  });
}
