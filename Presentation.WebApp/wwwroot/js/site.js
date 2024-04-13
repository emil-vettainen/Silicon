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
        document.getElementById('searchQuery').addEventListener('change', function (event) {
            //if (event.key === "Enter") {
            //    event.preventDefault()
            //    return
            //}
            updateCoursesByFilter()
        })

    } catch (e) {

    }
}

//function filterCourses() {
//    const categoryElement = document.querySelector('.selected');
//    const category = categoryElement ? categoryElement.getAttribute('data-value') : "";

//    const searchQuery = document.getElementById('searchQuery').value



//    fetch(`/Courses/UpdateCoursesByFilter?category=${encodeURIComponent(category || "")}&searchQuery=${encodeURIComponent(searchQuery)}`)

//        .then(response => {
//            if (!response.ok) {
//                throw new Error('Network response was not ok');
//            }
//            return response.text(); // Servern svarar med HTML
//        })
//        .then(data => {
//            document.getElementById('boxes').innerHTML = data; // Infoga HTML i elementet
//        })
//        .catch(error => {
//            console.error('Fetch error:', error);
//        });
//}





//function updateCoursesByFilter() {
//    const categoryElement = document.querySelector('.selected');
//    const category = categoryElement ? categoryElement.getAttribute('data-value') : "";

//    const searchQuery = document.getElementById('searchQuery').value

//    const url = `/courses?category=${encodeURIComponent(category || "")}&searchQuery=${encodeURIComponent(searchQuery)}`


//    fetch(url)
//        .then(res => res.text())
//        .then(data => {
//            const parser = new DOMParser()
//            const dom = parser.parseFromString(data, 'text/html')
//            document.querySelector('#boxes').innerHTML = dom.querySelector('#boxes').innerHTML

//            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
//            /*document.querySelector('.pagination').innerHTML = pagination*/
//        })
//}

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

            // Kontrollera om svaret innehåller några kursboxar
            if (!coursesBox || !coursesBox.innerHTML.trim()) {
                // Om inga kursboxar finns, visa "Not Found!"
                document.querySelector('#boxes').innerHTML = '<div><h6 class="pt-5 text-center">No courses found.</h6></div>'
            } else {
                // Annars, uppdatera innehållet med de hittade kursboxarna
                document.querySelector('#boxes').innerHTML = coursesBox.innerHTML
            }

            // Hantera paginering om sådan finns
            const paginationElement = document.querySelector('.pagination');
            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : ''
            if (paginationElement) {
                paginationElement.innerHTML = pagination;
            }
        })
        .finally(() => {
            onAjaxComplete(); // Initialisera tooltips igen efter AJAX-anropet
        })
}


function onAjaxComplete() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
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





function addHighlight() {
    const container = document.getElementById('highlights-container');
    const index = container.querySelectorAll('.highlight').length;
    const highlightHtml = `
        <div class="input-group highlight">
            <label>Highlight</label>
            <input type="text" name="Course.Highlights[${index}].Highlight" class="form-control" />
            <button type="button" onclick="removeHighlight(this)">Remove</button>
        </div>`;
    container.insertAdjacentHTML('beforeend', highlightHtml);
}

function removeHighlight(button) {
    button.parentNode.remove();
}



function addContent() {
    const container = document.getElementById('content-container');
    const index = container.querySelectorAll('.content-item').length;
    const contentHtml = `
        <div class="input-group content-item">
            <label for="Course_Content_${index}__Title">Title</label>
            <input type="text" name="Course.Content[${index}].Title" class="form-control" />
            <label for="Course_Content_${index}__Description">Description</label>
            <input type="text" name="Course.Content[${index}].Description" class="form-control" />
            <button class="btn-theme-medium" type="button" onclick="removeContent(this)">Remove</button>
        </div>`;
    container.insertAdjacentHTML('beforeend', contentHtml);
}

function removeContent(button) {
    button.closest('.content-item').remove();
}



