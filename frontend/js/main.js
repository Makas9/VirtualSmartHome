var currentPage = "menu";

function loginUser(e) {
    e.preventDefault();

    var loginData = {
        "username": document.getElementById("username").value,
        "password": document.getElementById("password").value
    };

    // send loginData via WS to the server

    window.location.href = "home.html"; // if login is successful
    // alert("Wrong username or password"); // if login is unsuccessful
}

function logoutUser(e){
    e.preventDefault();

    // destroy session

    window.location.href = "index.html";
}

function loadPage(page){
    document.getElementById("page-"+currentPage).style.display = "none";
    document.getElementById("page-"+page).style.display = "block";
    currentPage = page;
}