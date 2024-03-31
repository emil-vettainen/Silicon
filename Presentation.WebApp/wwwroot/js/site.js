document.addEventListener('DOMContentLoaded', function () {
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





function submitForm() {
    document.getElementById('uploadForm').submit();
}

const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))



