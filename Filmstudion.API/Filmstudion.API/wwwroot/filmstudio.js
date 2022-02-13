const content = document.querySelector("#content")

const user = JSON.parse(sessionStorage.getItem("user"));
let headers = {};
if (user) {
    headers = {
        "Content-type": "application/json;charset=UTF-8",
        "Authorization": `Bearer ${user.token}`
    }
}

const userUrl = `https://localhost:5001/api/filmstudio/${user.filmstudioId}`;


const getData = async (url, headers) => {
    const response = await fetch(url, {
        method: "GET",
        headers: headers
    })
    const data = await response.json();
    return data;
}

const returnFilm = async (returnUrl, headers) => {
    const response = await fetch(returnUrl, {
        method: 'Post',
        headers: headers
    })
    const data = await response.json();
    return data;
}

const getStudios = async () => {
    if (user) {
        const studios = await getData(userUrl, headers);
        console.log(studios);
        return studios;
    }

}
const renderPage = async () => {
    const studio = await getStudios();
    const filmCopies = studio.rentedFilmCopies;
    const h = document.createElement("h2");
    h.textContent = "Lånade filmer";
    content.appendChild(h)
    if (filmCopies.length == 0) {
        const p = document.createElement("p");
        p.textContent = "Inga lånade filmer för närvarande.";
        content.appendChild(p);
    }
    else {
        for (filmCopy of filmCopies) {
            console.log(filmCopy)
            let url = `https://localhost:5001/api/films/${filmCopy.filmId}`
            film = await getData(url, headers);
            const div = document.createElement("div");
            const returnBtn = document.createElement("button");
            const ul = document.createElement("ul");
            const liName = document.createElement("li");
            const liCountry = document.createElement("li");
            const liDirector = document.createElement("li");
            div.classList.add("rentedCopyDiv");
            ul.classList.add("rentedCopyUl");
            liName.classList.add("rentedCopyLi");
            liCountry.classList.add("rentedCopyLi");
            liDirector.classList.add("rentedCopyLi");
            liName.textContent = `Namn = ${film.name}`;
            liCountry.textContent = `Land = ${film.country}`;
            liDirector.textContent = `Regissör = ${film.director}`;
            returnBtn.textContent = "Lämna tillbaka film";

            const returnUrl = `https://localhost:5001/api/films/return?id=${film.filmId}&studioid=${user.filmstudioId}`;
            returnBtn.addEventListener("click", async () => {
                returnFilm(returnUrl, headers);
                location.href = "filmstudios.html";
            })
            content.appendChild(div);
            div.append(ul, returnBtn);
            ul.append(liName, liDirector, liCountry);
        };
    }
}

renderPage();

