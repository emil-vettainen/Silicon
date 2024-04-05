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
                filterCourses()
            })
        })

    } catch (e) {
        console.log(e)
    }
}


function searchQuery() {
    try {
        document.getElementById('searchQuery').addEventListener('keydown', function (event) {
            if (event.key === "Enter") {
                event.preventDefault()
                return
            }

            filterCourses()
        })

    } catch (e) {

    }
}

function filterCourses() {
    const categoryElement = document.querySelector('.selected');
    const category = categoryElement ? categoryElement.getAttribute('data-value') : ""; 
 
    const searchQuery = document.getElementById('searchQuery').value



    fetch(`/Courses/UpdateCoursesByFilter?category=${encodeURIComponent(category || "")}&searchQuery=${encodeURIComponent(searchQuery)}`)

        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text(); // Servern svarar med HTML
        })
        .then(data => {
            document.getElementById('boxes').innerHTML = data; // Infoga HTML i elementet
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}





function updateCoursesByFilter() {
    const category = document.querySelector('.select .selected').innerHTML || 'all'
    const url = `/Courses/Courses?category=${encodeURIComponent(category)}`

    fetch(url)
        .then(res => res.text())
        .then(data => {
            const parser = new DOMParser()
            const dom = parser.parseFromString(data, 'text/html')
            document.querySelector('.items').innerHTML = dom.querySelector('.items').innerHTML
        })
}



function handleProfileImageUpload() {
    try {

        let uploader = document.getElementById('uploadFile')
        if (uploader != undefined) {
            uploader.addEventListener('change', function() {
                if (this.files.length > 0)
                    this.form.submit()
            })
        }
    } catch (e)
    {
        console.log(e)
    }
}



function submitForm() {
    document.getElementById('uploadForm').submit();
}

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))



