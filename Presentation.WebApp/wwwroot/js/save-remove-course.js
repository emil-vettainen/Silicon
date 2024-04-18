async function saveCourse(courseId) {
    try {
        const response = await fetch(`/account/savecourse?courseId=${courseId}`, {
            method: 'POST',
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            }
        })
        const statusMessages = {
            200: { icon: "success", text: "Course has been saved to your favorites" },
            400: { icon: "error", text: "Invalid request" },
            409: { icon: "warning", text: "Already saved" },
            default: { icon: "error", text: "An unexpected error occurred! Please try again" }
        }
        const { icon, text } = statusMessages[response.status] || statusMessages.default
        Swal.fire({
            icon,
            text
        })
    } catch (error) {
        console.error('Error', error)
        Swal.fire({
            icon: "error",
            text: "There was a problem reaching the server. Please try again later."
        })
    }
}


async function removeCourse(courseId) {
    const result = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: "Do you really want to remove this course?",
        showCancelButton: true,
        cancelButtonColor: '#EF4444',
        confirmButtonColor: '#6366F1',
        confirmButtonText: 'Yes, remove it!'
    })
    if (result.isConfirmed) {
        try {
            const response = await fetch(`/account/RemoveOneCourse?courseId=${courseId}`, {
                method: 'POST',
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                },
            })
            const statusMessages = {
                409: { icon: "warning", text: "Invalid request" },
                default: { icon: "error", text: "An unexpected error occurred! Please try again" }
            }
            if (response.status === 200) {
                Swal.fire({
                    icon: "success",
                    text: "Course has been removed",
                    didClose: () => {
                        window.location.reload();
                    }
                });
            }
            else {
                const { icon, text } = statusMessages[response.status] || statusMessages.default
                Swal.fire({
                    icon,
                    text
                })
            }
        } catch (error) {
            console.error('Error', error)
            Swal.fire({
                icon: "error",
                text: "There was a problem reaching the server. Please try again later."
            })
        }
    }
}


async function removeAllCourses() {
    const result = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: "Do you really want to remove all courses?",
        showCancelButton: true,
        cancelButtonColor: '#EF4444',
        confirmButtonColor: '#6366F1',
        confirmButtonText: 'Yes, remove!'
    })
    if (result.isConfirmed) {
        try {
            const response = await fetch(`/account/RemoveAllCourses`, {
                method: 'POST',
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                },
            })
            const statusMessages = {
                409: { icon: "warning", text: "No saved courses to remove" },
                default: { icon: "error", text: "An unexpected error occurred! Please try again" }
            }
            if (response.status === 200) {
                Swal.fire({
                    icon: "success",
                    text: "Courses have been removed",
                    didClose: () => {
                        window.location.reload();
                    }
                });
            }
            else {
                const { icon, text } = statusMessages[response.status] || statusMessages.default
                Swal.fire({
                    icon,
                    text
                })
            }
        } catch (error) {
            console.error('Error', error)
            Swal.fire({
                icon: "error",
                text: "There was a problem reaching the server. Please try again later."
            })
        }
    }
}