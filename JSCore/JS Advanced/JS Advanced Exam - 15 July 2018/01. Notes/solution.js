function addSticker() {
    let title = $('input.title').val();
    let content = $('input.content').val();

    if (title == '' || content == '') {
        return;
    }

    let html = $('<li>').addClass('note-content')
        .append($('<a>').addClass('button').text('x').on('click', function () {
            this.parentNode.parentNode.removeChild(this.parentNode);
        }))
        .append($('<h2>').text(title))
        .append($('<hr>'))
        .append($('<p>').text(content));

    $('#sticker-list').append(html);

    $('input.title').val('');
    $('input.content').val('');
}