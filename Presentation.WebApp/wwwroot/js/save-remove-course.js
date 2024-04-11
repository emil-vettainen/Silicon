function saveCourse(courseId) {
    fetch(`/account/savecourse?courseId=${courseId}`, {
        method: 'POST',
        headers: {
            'X-Requested-With' : 'XMLHttpRequest'
        },
    })

        .then(response => {
            if (!response.ok) {
                window.location.reload()
            }
            return response.json()
        })

        .then(data => {
            displayMessage(data.status, data.message)
        })

        .catch(error => {
            console.log('Error', error)
        })
}


function displayMessage(message, status) {
    const modalTitle = document.getElementById('alertModalLabel');
    const modalBody = document.querySelector('#alertModal .modal-body');
    const modal = new bootstrap.Modal(document.getElementById('alertModal'));

    modalTitle.innerHTML = `Message: <span class="${status}">${status.toUpperCase()}</span>`; // Lägg till klasser för styling baserat på status
    modalBody.textContent = message;
    modal.show();
}


function removeCourse(courseId) {
    fetch(`/account/RemoveOneCourse?courseId=${courseId}`, {
        method: 'POST',
        headers: {
            'X-Requested-With': 'XMLHttpRequest'
        },
    })

        .then(response => {
            window.location.reload()
        })
        .catch(error => {
            console.log('Error', error)
        })
}


function removeAllCourses() {
    fetch(`/account/RemoveAllCourses`, {
        method: 'POST',
        headers: {
            'X-Requested-With': 'XMLHttpRequest'
        },
    })
        .then(response => {
            window.location.reload()
        })
        .catch(error => {
            console.log('Error', error)
        })
}


function deleteCourseAdmin(courseId) {
    fetch(`/admin/DeleteCourse?courseId=${courseId}`, {
        method: 'DELETE',
        headers: {
            'X-Requested-With': 'XMLHttpRequest',
        },
    })
        .then(response => {
            window.location.reload()
        })
        .catch(error => {
            console.log('Error', error)
        })
}