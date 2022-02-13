const studioBtn = document.querySelector("#filmstudios");
const tempButtons = document.querySelector("#tempButtons");

const user = JSON.parse(sessionStorage.getItem("user"));

if (user) {
    const greeting = document.querySelector("#greetUser");
    greeting.textContent = `Hej ${user.username}`;
    const logOutBtn = document.createElement("button");
    const studioBtn = document.createElement("button");
    studioBtn.id = "studioBtn";
    studioBtn.textContent = "Hyrda filmer";
    logOutBtn.id = "logout";
    logOutBtn.textContent = "Logga ut";
    logOutBtn.addEventListener("click", () => {
        sessionStorage.removeItem("user");
        location.href = "index.html";
    })
    studioBtn.addEventListener("click", () => {
        location.href = "filmstudios.html";
    })
    tempButtons.append(logOutBtn, studioBtn);
}
else {
    const logInBtn = document.createElement("button");
    logInBtn.id = "login";
    logInBtn.textContent = "Logga in";
    logInBtn.addEventListener("click", () => {
        location.href = "login.html";
    })
    const regBtn = document.createElement("button");
    regBtn.id = "register";
    regBtn.textContent = "Registrera dig";
    regBtn.addEventListener("click", () => {
        location.href = "register.html";
    })

    tempButtons.appendChild(logInBtn);
    tempButtons.appendChild(regBtn);

}

