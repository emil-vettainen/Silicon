//document.addEventListener('DOMContentLoaded', function () {
//    var alertModal = new bootstrap.Modal(document.getElementById('alertModal'));
//    alertModal.show();
//});

var alertModal = new bootstrap.Modal(document.getElementById('alertModal'));
alertModal.show();

var alertModal = new bootstrap.Modal(document.getElementById('alertModal'));

document.getElementById('yourOkButtonId').addEventListener('click', function () {
    alertModal.hide();
});
