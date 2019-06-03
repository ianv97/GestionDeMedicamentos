export default async function post(url, data) {
  return await fetch(url, {
    method: "POST",
    mode: "cors",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json"
    }
  }).then(res => {
    return res;
  });
}
