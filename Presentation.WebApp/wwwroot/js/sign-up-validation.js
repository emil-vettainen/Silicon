


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




const emailValidator = (element) => {

    const isEmpty = !element.value;
    const isValidEmail = /^(([^<>()\]\\.,;:\s@"]+(\.[^<>()\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(element.value)

    if (isEmpty) {
        formErrorHandler(element, false, element.dataset.valRequired)
    }
    else if (!isValidEmail) {
        formErrorHandler(element, false, element.dataset.valRegex)
    }
    else {
        formErrorHandler(element, true)
    }


    //const regEx = /^(([^<>()\]\\.,;:\s@"]+(\.[^<>()\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    //formErrorHandler(element, regEx.test(element.value))
}




const passwordValidator = (element) => {



    if (element.dataset.valEqualtoOther !== undefined) {

        const password = document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'SignUp'))[0].value

        const isEmpty = !element.value
        const isMatch = element.value === password

        if (isEmpty) {
            formErrorHandler(element, false, element.dataset.valRequired)
        }
        else if (!isMatch) {
            formErrorHandler(element, false, element.dataset.valEqualto)
        }
        else {
            formErrorHandler(element, true)
        }


        //if (element.value === password) {
        //    formErrorHandler(element, true)
        //}
        //else {
        //    formErrorHandler(element, false, 'Passwords do not match')
        //}
    }
    else {

        const isEmpty = !element.value
        const isValidPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(element.value)

        if (isEmpty) {
            formErrorHandler(element, false, element.dataset.valRequired)
        }
        else if (!isValidPassword) {
            formErrorHandler(element, false, element.dataset.valRegex)
        }
        else {
            formErrorHandler(element, true)
        }


        //const regEx = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
        //formErrorHandler(element, regEx.test(element.value))
    }
}


const checkboxValidator = (element) => {
    if (element.checked) {
        formErrorHandler(element, true)
    }
    else {
        formErrorHandler(element, false, 'You must accept the terms and conditions')
    }
}


//const checkAndAddClass = (input) => {
//    if (input.classList.contains('input-validation-error'))
//        input.classList.add('is-invalid'); // Lägg till din anpassade klass

//}






//let forms = document.querySelectorAll('form')
//let inputs = forms[0].querySelectorAll('input')


//inputs.forEach(input => {



//    if (input.dataset.val === 'true') {

//        if (input.classList.contains('input-validation-error'))
//            input.classList.add('is-invalid')

//        if (input.type === 'checkbox') {

//            input.addEventListener('change', (e) => {
//                checkboxValidator(e.target)
//            })
//        }
//        else {

//            input.addEventListener('keyup', (e) => {

//                switch (e.target.type) {

//                    case 'text':
//                        textValidator(e.target)
//                        break;

//                    case 'email':
//                        emailValidator(e.target)
//                        break;

//                    case 'password':
//                        passwordValidator(e.target)
//                        break;
//                }
//            })

//        }





//    }


//})



document.addEventListener('DOMContentLoaded', function () {
    initValidation(); // Initiera valideringen när sidan laddas
    setupMutationObserver(); // Starta MutationObserver
});

function initValidation() {
    let forms = document.querySelectorAll('form');
    forms.forEach(form => {
        let inputs = form.querySelectorAll('input');
        inputs.forEach(input => {
            if (input.dataset.val === 'true') {
                if (input.classList.contains('input-validation-error')) {
                    input.classList.add('is-invalid');
                }

                // Binda event baserat på input-typ
                bindInputEvent(input);
            }
        });
    });
}

function bindInputEvent(input) {
    if (input.type === 'checkbox') {
        input.addEventListener('change', (e) => checkboxValidator(e.target));
    } else {
        input.addEventListener('keyup', (e) => {
            switch (e.target.type) {
                case 'text':
                    textValidator(e.target);
                    break;
                case 'email':
                    emailValidator(e.target);
                    break;
                case 'password':
                    passwordValidator(e.target);
                    break;
            }
        });
    }
}

function setupMutationObserver() {
    const observer = new MutationObserver(mutations => {
        mutations.forEach(mutation => {
            mutation.addedNodes.forEach(node => {
                // Kontrollera om det tillagda noden är relevant för din valideringslogik
                if (node.nodeType === 1) { // Element nod
                    // Om noden innehåller formulär eller input-fält, återinitiera valideringslogiken
                    initValidation();
                }
            });
        });
    });

    observer.observe(document.body, { childList: true, subtree: true });
}






