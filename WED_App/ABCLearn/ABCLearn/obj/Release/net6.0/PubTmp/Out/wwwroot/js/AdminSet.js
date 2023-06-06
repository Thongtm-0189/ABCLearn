const loginModal = document.querySelector('#addModal')
const AddBtn = document.querySelector('.addModelbtn')
const closeForm = document.querySelector('.closeForm')
const cancelForm = document.querySelector('.cancelForm')

//AddBtn.addEventListener("click", formTable);

///closeForm.addEventListener("click", formTable);

//cancelForm.addEventListener("click", formTable);

function formTable() {
    console.log("ok")
    $('#addModal').modal('toggle');
}