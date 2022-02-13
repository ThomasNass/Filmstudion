const registerBtn = document.querySelector("#register");
const studioName = document.querySelector("#filmStudio");
const city = document.querySelector("#city");
const password = document.querySelector("#password");
const url = "https://localhost:5001/api/FilmStudio/register"


const getData = async (url) => {
    const response = await fetch(url, {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            FilmStudioName: studioName.value.toString(),
            FilmStudioCity: city.value.toString(),
            password: password.value.toString()
        })
    });
    const data = await response.json();
    return data;
}

registerBtn.addEventListener("click", async () => {

    console.log(studioName.value, city.value, password.value)
    let result = await registerUser();

    location.href = "login.html"
    console.log(result)
})

const registerUser = async () => {
    let result = await getData(url);
    return result;

}