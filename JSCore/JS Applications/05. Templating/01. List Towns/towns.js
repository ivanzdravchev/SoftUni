function attachEvents() {
    $('#btnLoadTowns').click(listItems);

    function listItems() {
        let arr = [];
        let inputCities = $('#towns').val().split(', ');
        $('#towns').val('');
        for (let city of inputCities) {
            arr.push({city});
        }
        let cities = {cities: arr};

        let source = $('#towns-template').html();
        let template = Handlebars.compile(source);
        $('#root').empty();
        $('#root').html(template(cities));
    }
}