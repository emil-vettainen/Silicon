async function deleteCourseAdmin(courseId) {
    const result = await Swal.fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: "Do you really want to remove this course?",
        showCancelButton: true,
        cancelButtonColor: '#EF4444',
        confirmButtonColor: '#6366F1',
        confirmButtonText: 'Yes, remove!'
    });

    if (result.isConfirmed) {
        try {
            const response = await fetch(`/admin/DeleteCourse?courseId=${courseId}`, {
                method: 'DELETE',
                headers: {
                    'X-Requested-With': 'XMLHttpRequest',
                },
            });
            const statusMessages = {
                404: { icon: "warning", text: "No course to remove" },
                default: { icon: "error", text: "An unexpected error occurred! Please try again" }
            };
            if (response.status === 200) {
                Swal.fire({
                    icon: "success",
                    text: "Course has been removed",
                    didClose: () => {
                        window.location.reload();
                    }
                });
            } else {
                const { icon, text } = statusMessages[response.status] || statusMessages.default;
                Swal.fire({ icon, text });
            }
        } catch (error) {
            console.error('Error', error);
            Swal.fire({
                icon: "error",
                text: "There was a problem reaching the server. Please try again later."
            });
        }
    }
}

function addHighlight() {
    const container = document.getElementById('highlights-container');
    const index = container.querySelectorAll('.highlight').length;
    const highlightHtml = `
        <div class="input-group highlight">
            <label class="pb-2 pt-2" for="Course.Highlights[${index}]">Highlight</label>
            <input type="text" name="Course.Highlights[${index}].Highlight" class="form-control" />
            <button class="btn-danger-small" type="button" onclick="removeHighlight(this)">Remove</button>
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
            <label class="pb-2 pt-2"  for="Course_Content_${index}__Title">Title</label>
            <input type="text" name="Course.Content[${index}].Title" class="form-control" />
            <label class="pb-2 pt-2" for="Course_Content_${index}__Description">Description</label>
            <input type="text" name="Course.Content[${index}].Description" class="form-control" />
            <button class="btn-danger-small" type="button" onclick="removeContent(this)">Remove</button>
        </div>`;
    container.insertAdjacentHTML('beforeend', contentHtml);
}

function removeContent(button) {
    button.closest('.content-item').remove();
}