const formErrorHandler = (element, validationResult, customMessage = "") => {
    let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

    if (validationResult) {
        element.classList.remove('input-validation-error')
        element.classList.remove('is-invalid')
        element.classList.add('is-valid')
        spanElement.classList.remove('field-validation-error')
        spanElement.classList.add('field-validation-valid')
        spanElement.innerHTML = ''
    }
    else {
        element.classList.add('input-validation-error')
        element.classList.add('is-invalid')
        element.classList.remove('is-valid')
        spanElement.classList.add('field-validation-error')
        spanElement.classList.remove('field-validation-valid')
        spanElement.innerHTML = customMessage || element.dataset.valRequired;
    }
}

const textValidator = (element, minLength = 1) => {
    if (element.value.length >= minLength) {

        formErrorHandler(element, true)
    }
    else {
        formErrorHandler(element, false)
    }
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input')

inputs.forEach(input => {
    if (input.dataset.val === 'true') {
        if (input.classList.contains('input-validation-error'))
            input.classList.add('is-invalid')

        input.addEventListener('keyup', (e) => {
            switch (e.target.type) {

                case 'email':
                    textValidator(e.target)
                    break;

                case 'password':
                    textValidator(e.target)
                    break;
            }
        })
    }
})