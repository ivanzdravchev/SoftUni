function makeReservation() {
    $('#submit').on('click', extractInfo);
    $('#edit').on('click', edit);
    $('#continue').on('click', paymentOptions);

    function extractInfo() {
        let fullName = $('input:eq(0)').val();
        let email = $('input:eq(1)').val();
        let phone = $('input:eq(2)').val();
        let address = $('input:eq(3)').val();
        let postalCode = $('input:eq(4)').val();

        if (fullName == '' || email == '') {
            return;
        }

        $('#infoPreview').append($('<li>').text('Name: ' + fullName));
        $('#infoPreview').append($('<li>').text('E-mail: ' + email));
        $('#infoPreview').append($('<li>').text('Phone: ' + phone));
        $('#infoPreview').append($('<li>').text('Address: ' + address));
        $('#infoPreview').append($('<li>').text('Postal Code: ' + postalCode));

        $('input').val('');
        $('#submit').attr('disabled', 'disabled');
        $('#edit').removeAttr('disabled');
        $('#continue').removeAttr('disabled');
    }

    function edit() {
        let fullName = $('li:eq(0)')[0].textContent.substr(6);
        let email = $('li:eq(1)')[0].textContent.substr(8);
        let phone = $('li:eq(2)')[0].textContent.substr(7);
        let address = $('li:eq(3)')[0].textContent.substr(9);
        let postalCode = $('li:eq(4)')[0].textContent.substr(13);
        
        $('#fullName').val(fullName);
        $('#email').val(email);
        $('#phoneNumber').val(phone);
        $('#address').val(address);
        $('#postalCode').val(postalCode);

        $('#infoPreview').empty();
        $('#submit').removeAttr('disabled');
        $('#edit').attr('disabled', 'disabled');
        $('#continue').attr('disabled', 'disabled');
    }

    function paymentOptions() {
        $('#edit').attr('disabled', 'disabled');
        $('#continue').attr('disabled', 'disabled');

        let html = $('#container')
            .append($('<h2>').text('Payment details'))
            .append($('<select>').attr('id', 'paymentOptions').addClass('custom-select')
            .append('<option selected disabled hidden>Choose</option')
            .append('<option value="creditCard">Credit Card</option>')
            .append('<option value="bankTransfer">Bank Transfer</option>'))
            .append($('<div>').attr('id', 'extraDetails'));

        $('#container').append(html);
        $('#paymentOptions').on('change', extraDetails);
    }

    function extraDetails() {
        let selected = $('#paymentOptions > option:selected').val();
        $('#extraDetails').empty();
        if (selected == 'creditCard') {
            $('#extraDetails')
                .append($('<div>').addClass('inputLabel').text('Card Number').append('<input>')).append('<br>')
                .append($('<div>').addClass('inputLabel').text('Expiration Date').append('<input>')).append('<br>')
                .append($('<div>').addClass('inputLabel').text('Security Numbers').append('<input>')).append('<br>')
                .append($('<button>').attr('id', 'checkOut').text('Check Out'));
        }
        if (selected == 'bankTransfer') {
            $('#extraDetails')
                .append($('<p>You have 48 hours to transfer the amount to:<br>IBAN: GR96 0810 0010 0000 0123 4567 890</p>'))
                .append($('<button>').attr('id', 'checkOut').text('Check Out'));
        }
        $('#checkOut').on('click', function() {
            $('#wrapper').empty();
            $('#wrapper').append($('<h4>').text('Thank you for your reservation!'));
        });
    }
}