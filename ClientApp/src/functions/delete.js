export default async function del(url) {
  return await fetch(url, {
    method: "DELETE",
    mode: "cors",
    headers: {
      "Content-Type": "application/json"
    }
  }).then(res => {
    return res;
  });
}
