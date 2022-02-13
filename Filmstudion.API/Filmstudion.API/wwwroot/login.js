const loginBtn = document.querySelector("#login");
const studioName = document.querySelector("#filmStudio");
const password = document.querySelector("#password");
const url = "https://localhost:5001/api/User/authenticate";


const postData = async (url, _data) => {
    const response = await fetch(url, {
        method: "POST",
        body: JSON.stringify(_data),
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        }
    });
    const data = await response.json();
    return data;
}

loginBtn.addEventListener("click", async () => {

    let _data = {
        userName: studioName.value.toString(),
        password: password.value.toString()
    }
    const data = await postData(url, _data)
    sessionStorage.setItem("user", JSON.stringify(data))
    console.log(data.token);
    location.href = "filmstudios.html";

})

