document.querySelector("#btnChatBox").addEventListener("click", () => {
    document.querySelector("#chatboxcard").style.display = "block";
    document.querySelector("#dictionarycard").style.display = "none";
})
document.querySelector("#btnDictionary").addEventListener("click", () => {
    document.querySelector("#chatboxcard").style.display = "none";
    document.querySelector("#dictionarycard").style.display = "block";
})

var btnbox = document.querySelector("#btnBox");
var show = true;
btnbox.addEventListener("click", () => {
    if (show) {
        document.querySelector("#meet-layout").setAttribute("class", "col-md-12 col-sm-12 position-relative min-vh-100 row");
        document.querySelector("#chat-dictionary").setAttribute("class", "col-md-3 col-sm-3 min-vh-100 col min-vh-100 d-none");
    } else {
        document.querySelector("#meet-layout").setAttribute("class", "col-md-9 col-sm-9 position-relative min-vh-100 row");
        document.querySelector("#chat-dictionary").setAttribute("class", "col-md-3 col-sm-3 min-vh-100 min-vh-100 col");
    }
    show = !show;
})