function addDestination() {
    let table = $('table#destinations');
    let city = $('input:eq(0)').val();
    let country = $('input:eq(1)').val();
    let season = $('#seasons').find(':selected').text();

    $('input:eq(0)').val('');
    $('input:eq(1)').val('');

    if (city == '' || country == '') {
        return;
    }

    let html = $('<tr>')
        .append($('<td>').text(`${city}, ${country}`))
        .append($('<td>').text(season));
    table.append(html);
    switch (season) {
        case 'Spring': $('#spring').val(+$('#spring').val() + 1); break;
        case 'Summer': $('#summer').val(+$('#summer').val() + 1); break;
        case 'Autumn': $('#autumn').val(+$('#autumn').val() + 1); break;
        case 'Winter': $('#winter').val(+$('#winter').val() + 1); break;
    }
}