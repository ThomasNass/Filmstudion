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
            FilmStudioName: studioName.value,
            FilmStudioCity: city,
            password: password.value
        })
    });
    const data = await response.json();
    return data;
}

registerBtn.querySelector("click", () => {

    let result = await getData(url);

    console.log(result)
})