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
        const targetName = element.dataset.valEqualtoOther.replace('*.', '') 
        const targetElement = document.querySelector('input[type="password"][data-val="true"]');
        const targetPassword = targetElement ? targetElement.value : null;
 
        const isEmpty = !element.value
        const isMatch = element.value === targetPassword

        if (isEmpty) {
            formErrorHandler(element, false, element.dataset.valRequired)
        }
        else if (!isMatch) {
            formErrorHandler(element, false, element.dataset.valEqualto)
        }
        else {
            formErrorHandler(element, true)
        }

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
        formErrorHandler(element, false)
    }
}


const checkAndAddClass = (input) => {
    if (input.classList.contains('input-validation-error'))
        input.classList.add('is-invalid'); // L�gg till din anpassade klass

}


//let forms = document.querySelectorAll('form')
//let inputs = forms[0].querySelectorAll('input')
//let textareas = forms[0].querySelectorAll('textarea')



//textareas.forEach(textarea => {
//    if (textarea.dataset.val === 'true') {
//        if (textarea.classList.contains('input-validation-error'))
//            textarea.classList.add('is-invalid');

//        textarea.addEventListener('keyup', (e) => {
//            textValidator(e.target)
//        })
//    }
//})





//inputs.forEach(input => {

//    if (input.dataset.val === 'true') {

//        if (input.classList.contains('input-validation-error'))
//            input.classList.add('is-invalid')

//        if (input.type === 'checkbox') {

//            input.addEventListener('change', (e) => {
//                checkboxValidator(e.target)
//            })
//        }

//        //if (input.dataset.current) {
//        //    input.addEventListener('keyup', (e) => {
//        //        textValidator(e.target)
//        //    })
//        //}



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
    initValidation(); // Initiera valideringen n�r sidan laddas
   /* setupMutationObserver(); // Starta MutationObserver*/
});


//function initValidation() {
//    let forms = document.querySelectorAll('form')
//    forms.forEach(form => {
//        form.addEventListener('input', function (event) {
//            let input = event.target;
//            if 
//        })
//    })
//}



function initValidation() {
    let forms = document.querySelectorAll('form');
    forms.forEach(form => {
        let inputs = form.querySelectorAll('input');
        let textareas = form.querySelectorAll('textarea')


        inputs.forEach(input => {
            if (input.dataset.val === 'true') {
                if (input.classList.contains('input-validation-error')) {
                    input.classList.add('is-invalid');
                }
                validateInput(input);
            }
        });


        textareas.forEach(textarea => {
            if (textarea.dataset.val === 'true') {
                if (textarea.classList.contains('input-validation-error'))
                    textarea.classList.add('is-invalid');

                textarea.addEventListener('keyup', (e) => {
                    textValidator(e.target)
                })
            }
        })

    });
}

function validateInput(input) {

    if (input.dataset.validation === 'currentPassword') {
        input.addEventListener('keyup', (e) => textValidator(e.target))
    } else if (input.type === 'checkbox') {
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

//function setupMutationObserver() {
//    const observer = new MutationObserver(mutations => {
//        mutations.forEach(mutation => {
//            mutation.addedNodes.forEach(node => {
//                // Kontrollera om det tillagda noden �r relevant f�r din valideringslogik
//                if (node.nodeType === 1) { // Element nod
//                    // Om noden inneh�ller formul�r eller input-f�lt, �terinitiera valideringslogiken
//                    initValidation();
//                }
//            });
//        });
//    });

//    observer.observe(document.body, { childList: true, subtree: true });
//}






