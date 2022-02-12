const loginBtn = document.querySelector("#login");
const studioBtn = document.querySelector("#filmstudios");
const filmBtn = document.querySelector("#films");
const registerBtn = document.querySelector("#register");


loginBtn.addEventListener("click", () => {
    location.href = "login.html";
})
studioBtn.addEventListener("click", () => {
    location.href = "filmstudios.html";
})
filmBtn.addEventListener("click", () => {
    location.href = "films.html";
})
registerBtn.addEventListener("click", () => {
    location.href = "register.html";
})
