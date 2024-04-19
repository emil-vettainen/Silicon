document.addEventListener('DOMContentLoaded', function () {
    select()
    searchQuery()
})

function select() {
    let select = document.querySelector('.select')
    if (select) {
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
    }
}


function searchQuery() {
    let searchInput = document.getElementById('searchQuery')
    if (searchInput) {
        addEventListener('keyup', function (event) {
            if (event.key === "Enter") {
                event.preventDefault()
                return
            }
            updateCoursesByFilter()
        })
    }
}


async function updateCoursesByFilter() {
    const categoryElement = document.querySelector('.selected')
    const category = categoryElement ? categoryElement.getAttribute('data-value') : ""
    const searchQuery = document.getElementById('searchQuery').value

    const url = `/courses?category=${encodeURIComponent(category || "")}&searchQuery=${encodeURIComponent(searchQuery)}`

    try {
        await fetch(url)
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
    } catch (error) {
        console.error('Error', error)
        Swal.fire({
            icon: "error",
            text: "An unexpected error occurred! Please try again"
        })
    }
}


function onAjaxComplete() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}