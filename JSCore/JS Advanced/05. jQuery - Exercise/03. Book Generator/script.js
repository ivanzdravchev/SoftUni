function createBook(selector, title, author, isbn) {
    $(selector).append($('<div>').attr('id', 'book' + (+$('#wrapper').children().length + 1))
        .append($('<p>').addClass('title').text(title))
        .append($('<p>').addClass('author').text(author))
        .append($('<p>').addClass('isbn').text(isbn))
        .append($('<button>').text('Select').on('click', select))
        .append($('<button>').text('Deselect').on('click', deselect)));

    function select() {
        $(this).parent().css('border', '2px solid blue');
    }

    function deselect() {
        $(this).parent().css('border', 'none');
    }
}