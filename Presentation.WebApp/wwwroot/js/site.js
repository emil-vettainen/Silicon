document.addEventListener('DOMContentLoaded', function () {
    handleProfileImageUpload()

    let sw = document.querySelector('#theme-toggle')
    sw.addEventListener('change', function () {
        let theme = this.checked ? "dark" : "light"
        fetch(`/sitesettings/changetheme?theme=${theme}`)
        .then(res => {
            if (res.ok)
                window.location.reload()
            else {
                Swal.fire({
                    icon: "error",
                    text: "An unexpected error occurred! Please try again"
                })
            }
        })
    })
})


const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))


function handleProfileImageUpload() {
    let uploader = document.getElementById('uploadFile')
    if (uploader != undefined) {
        uploader.addEventListener('change', function () {
            if (this.files.length > 0)
                this.form.submit()
        })
    }
}


async function subscriberForm(event) {
    event.preventDefault()
    const form = document.getElementById('newsletterForm')
    const data = new FormData(form)

    try {
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
            401: { icon: "error", text: "Unauthorized, Please contact support!" },
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
    } catch (error) {
        console.error('Error', error)
        Swal.fire({
            icon: "error",
            text: "An unexpected error occurred! Please try again"
        })
    }
}