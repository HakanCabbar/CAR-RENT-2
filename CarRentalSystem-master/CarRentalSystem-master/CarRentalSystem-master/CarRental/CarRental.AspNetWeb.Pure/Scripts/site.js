

document.addEventListener("click", (e) => {
    if (e.target.classList.contains('delete-button')) {
        e.target.closest('.card').style.display = "none"
    }
})


