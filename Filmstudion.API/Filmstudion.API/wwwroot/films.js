const content = document.querySelector("#content");
const user = JSON.parse(sessionStorage.getItem("user"));
const url = "https://localhost:5001/api/films";

let headers = { "Content-type": "application/json;charset=UTF-8" };
if (user) {
    headers = {
        "Content-type": "application/json;charset=UTF-8",
        "Authorization": `Bearer ${user.token}`
    }
}

const getData = async (url, headers) => {
    const response = await fetch(url, {
        method: "GET",
        headers: headers
    })
    const data = await response.json();
    return data;
}

const rentFilm = async (returnUrl, headers) => {
    const response = await fetch(returnUrl, {
        method: 'Post',
        headers: headers
    })
    const data = await response.json();
    return data;
}

const getFilms = async () => {

    const films = await getData(url, headers);
    return films;
}

const renderPage = async () => {
    const films = await getFilms();
    let available = false;
    let check = films.length;
    console.log(films)
    for (film of films) {
        const filmCopies = film.filmCopies;

        for (filmCopy of filmCopies) {
            if (filmCopy.filmStudioId == user.username) {
                console.log(user.username)
                available = false;
                check--;
                break;
            }
            else if (filmCopy.rentedOut == false) {
                available = true;
                check++;
                break;
            }
        }
        if (available) {
            const div = document.createElement("div");
            const rentBtn = document.createElement("button");
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
            rentBtn.textContent = "Låna film";

            const returnUrl = `https://localhost:5001/api/films/rent?id=${film.filmId}&studioid=${user.filmstudioId}`;
            rentBtn.addEventListener("click", async () => {
                rentFilm(returnUrl, headers);

                location.reload();
            })
            content.appendChild(div);
            div.append(ul);
            ul.append(liName, liDirector, liCountry);
            div.append(rentBtn);
        }
    }
    if (check == 0) {
        const p = document.createElement("p");
        p.textContent = "Det finns inga filmer att hyra";
        content.appendChild(p);
    }

}
renderPage();
// const renderUnAuthPage = async () => {
//     const films = await getFilms();
//     for (film of films) {
//         const div = document.createElement("div");
//         const ul = document.createElement("ul");
//         const liName = document.createElement("li");
//         const liCountry = document.createElement("li");
//         const liDirector = document.createElement("li");
//         div.classList.add("rentedCopyDiv");
//         ul.classList.add("rentedCopyUl");
//         liName.classList.add("rentedCopyLi");
//         liCountry.classList.add("rentedCopyLi");
//         liDirector.classList.add("rentedCopyLi");
//         liName.textContent = `Namn = ${film.name}`;
//         liCountry.textContent = `Land = ${film.country}`;
//         liDirector.textContent = `Regissör = ${film.director}`;

//         content.appendChild(div);
//         div.append(ul);
//         ul.append(liName, liDirector, liCountry);
//     }
// }

//if (user) {

//}
// else {
//     const rentedButton = document.querySelector("#filmstudios");
//     rentedButton.style.display = "none";
//     renderUnAuthPage();

// }