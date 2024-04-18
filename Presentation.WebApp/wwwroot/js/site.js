document.addEventListener('DOMContentLoaded', function () {

    select()
    searchQuery()
    handleProfileImageUpload()

    let sw = document.querySelector('#theme-toggle')
    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"

        fetch(`/sitesettings/changetheme?theme=${theme}`)
            .then(res => {
                if (res.ok)
                    window.location.reload()
                else {
                    console.log("...")
                }
            })
    })



})


function select() {
    try {
        let select = document.querySelector('.select')
        let selected = select.querySelector('.selected')
        let selectOptions = select.querySelector('.select-options')


        selected.addEventListener('click', function () {
            selectOptions.style.display = (selectOptions.style.display == 'block') ? 'none' : 'block'
        })

        let options = selectOptions.querySelectorAll('.option')
        options.forEach(function (option) {
            option.addEventListener('click', function () {
                selected.innerHTML = this.textContent
                selectOptions.style.display = 'none'
                selected.setAttribute('data-value', this.getAttribute('data-value'))
                updateCoursesByFilter()
            })
        })

    } catch (e) {
        console.log(e)
    }
}


function searchQuery() {
    try {
        document.getElementById('searchQuery').addEventListener('keyup', function (event) {
            if (event.key === "Enter") {
                event.preventDefault()
                return
            }
            updateCoursesByFilter()
        })

    } catch (e) {
        console.log(e)
    }
}


function updateCoursesByFilter() {
    const categoryElement = document.querySelector('.selected')
    const category = categoryElement ? categoryElement.getAttribute('data-value') : ""
    const searchQuery = document.getElementById('searchQuery').value

    const url = `/courses?category=${encodeURIComponent(category || "")}&searchQuery=${encodeURIComponent(searchQuery)}`

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, 'text/html')
            const coursesBox = dom.querySelector('#boxes')

            if (!coursesBox || !coursesBox.innerHTML.trim()) {
                document.querySelector('#boxes').innerHTML = '<div><h6 class="pt-5 text-center">Unfortunately, we found no courses matching your search.</h6></div>'
            } else {
                document.querySelector('#boxes').innerHTML = coursesBox.innerHTML
            }
            const paginationElement = document.querySelector('.pagination');
            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
            if (paginationElement) {
                paginationElement.innerHTML = pagination;
            }
        })
        .finally(() => {
            onAjaxComplete();
        })
}


function onAjaxComplete() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}



const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))



function handleProfileImageUpload() {
    try {

        let uploader = document.getElementById('uploadFile')
        if (uploader != undefined) {
            uploader.addEventListener('change', function () {
                if (this.files.length > 0)
                    this.form.submit()
            })
        }
    } catch (e) {
        console.log(e)
    }
}



async function subscriberForm(event) {
    event.preventDefault()
    const form = document.getElementById('newsletterForm')
    const data = new FormData(form)

    const response = await fetch('/default/subscribe', {
        method: 'POST',
        body: data,
        headers: {
            'X-Requested-With': 'XMLHttpRequest'
        }
    })
    const statusMessages = {
        200: { icon: "success", text: "Thank you for subscribing!" },
        400: { icon: "error", text: "Invalid email address! The Email must match xx@xx.xx" },
        401: { icon: "error", text: "Unauthorized, Please contact support!"},
        409: { icon: "warning", text: "You are already subscribed." },
        default: { icon: "error", text: "An unexpected error occurred! Please try again." }
    }

    if (response.status === 200) {
        Swal.fire({
            icon: "success",
            text: "Thank you for subscribing!",
            didClose: () => {
                window.location.reload();
            }
        })
    } else {
        const { icon, text } = statusMessages[response.status] || statusMessages.default;
        Swal.fire({
            icon,
            text
        })
    }
}