function validate() {
    let usernameRegex = /^[A-Za-z0-9]{3,20}$/;
    let passwordRegex = /^[A-Za-z0-9_]{5,15}$/;
    let emailRegex = /(.*)@(.*)\.(.*)/;
    let companyRegex = /^[1-9]{1}[0-9]{3}$/;

    $('#company').on('change', toggleCompany);

    $('#submit').on('click', function (event) {
        event.preventDefault();
        let valid = true;

        if ($('#username').val().match(usernameRegex)) {
            $('#username').css('border', 'none');
        } else {
            $('#username').css('border-color', 'red');
            valid = false;
        }

        if ($('#email').val().match(emailRegex)) {
            $('#email').css('border', 'none');
        } else {
            $('#email').css('border-color', 'red');
            valid = false;
        }

        if ($('#password').val().match(passwordRegex)) {
            $('#password').css('border', 'none');
        } else {
            $('#password').css('border-color', 'red');
            valid = false;
        }

        if ($('#confirm-password').val().match(passwordRegex) && $('#confirm-password').val() == $('#password').val()) {
            $('#confirm-password').css('border', 'none');
        } else {
            $('#confirm-password').css('border-color', 'red');
            valid = false;
        }

        if ($('#company').is(':checked')) {
            if ($('#companyNumber').val().match(companyRegex)) {
                $('#companyNumber').css('border', 'none');
            } else {
                $('#companyNumber').css('border-color', 'red');
                valid = false;
            }
        }

        if (valid) {
            $('#valid').css('display', 'block');
        } else {
            $('#valid').css('display', 'none');
        }
    });

    function toggleCompany() {
        if ($('#company').is(':checked')) {
            $('#companyInfo').css('display', 'block');
        } else {
            $('#companyInfo').css('display', 'none');
        }
    }
}