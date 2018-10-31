function domSearch(selector, sensitive) {
    $(selector).append($('<div>').addClass('add-controls')
        .append($('<label>').text('Enter text:'))
        .append($('<input>'))
        .append($('<a href="#">').addClass('button').text('Add').on('click', addItem)));

    $(selector).append($('<div>').addClass('search-controls')
        .append($('<label>').text('Search:'))
        .append($('<input>').on('input', search)));

    $(selector).append($('<div>').addClass('result-controls')
        .append($('<ul>').addClass('items-list')));

    function addItem() {
        let value = $('input').first().val();
        $('.items-list').append($('<li>').addClass('list-item')
            .append($('<a href="#">').addClass('button').text('X').on('click', deleteItem))
            .append($('<strong>').text(value)));

    }

    function deleteItem() {
        $(this).parent().remove();
    }

    function search() {
        let search = $(this).val();
        $('li').toArray().forEach(li => {
            if (sensitive) {
                if ($(li).text().indexOf(search) > -1) {
                    $(li).css('display', 'block');
                } else {
                    $(li).css('display', 'none');
                }
            } else {
                if ($(li).text().toLowerCase().indexOf(search) > -1) {
                    $(li).css('display', 'block');
                } else {
                    $(li).css('display', 'none');
                }
            }
        });
    }
}