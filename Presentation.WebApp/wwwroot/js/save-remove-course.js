function saveCourse(courseId) {
    fetch(`/account/savecourse?courseId=${courseId}`, {
        method: 'POST',
        headers: {
            'X-Requested-With' : 'XMLHttpRequest'
        },
    })

        .then(response => {
            window.location.reload()
        })
        .catch(error => {
            console.log('Error', error)
        })
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