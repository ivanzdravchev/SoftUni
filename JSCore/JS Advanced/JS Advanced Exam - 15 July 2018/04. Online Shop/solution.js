function onlineShop(selector) {
    let form = `<div id="header">Online Shop Inventory</div>
    <div class="block">
        <label class="field">Product details:</label>
        <br>
        <input placeholder="Enter product" class="custom-select">
        <input class="input1" id="price" type="number" min="1" max="999999" value="1"><label class="text">BGN</label>
        <input class="input1" id="quantity" type="number" min="1" value="1"><label class="text">Qty.</label>
        <button id="submit" class="button" disabled>Submit</button>
        <br><br>
        <label class="field">Inventory:</label>
        <br>
        <ul class="display">
        </ul>
        <br>
        <label class="field">Capacity:</label><input id="capacity" readonly>
        <label class="field">(maximum capacity is 150 items.)</label>
        <br>
        <label class="field">Price:</label><input id="sum" readonly>
        <label class="field">BGN</label>
    </div>`;
    $(selector).html(form);

    let productInput = $('input.custom-select');
    let priceInput = $('#price');
    let quantityInput = $('#quantity');
    let submitButton = $('#submit');
    let ul = $('.display');

    let capacity = $('#capacity');
    let sum = $('#sum');

    $(productInput).on('input', toggleSubmit);
    $(submitButton).on('click', submit);

    function toggleSubmit() {
        if ($(productInput)[0].valuee == '') {
            $(submitButton).prop('disabled', true);
        } else {
            $(submitButton).prop('disabled', false);
        }
    }

    function submit() {
        let productName = $(productInput).val();
        let productPrice = $(priceInput).val();
        let productQuantity = $(quantityInput).val();

        let item = $('<li>').text(`Product: ${productName} Price: ${productPrice} Quantity: ${productQuantity}`);
        $(ul).append(item);
        resetFields();

        $(capacity).val(+$(capacity).val() + +productQuantity);
        $(sum).val(+$(sum).val() + +productPrice);
        if ($(capacity).val() >= 150) {
            $(capacity).val('full').addClass('fullCapacity');
            disableAll();
        }
    }

    function resetFields() {
        $(productInput).val('');
        $(priceInput).val(1);
        $(quantityInput).val(1);
        $(submitButton).prop('disabled', true);
    }

    function disableAll() {
        $(productInput).prop('disabled', true);
        $(priceInput).prop('disabled', true);
        $(quantityInput).prop('disabled', true);
        $(submitButton).prop('disabled', true);
    }
}