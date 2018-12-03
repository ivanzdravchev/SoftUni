function attachEvents() {
    let currentDiv = $('#current');
    let upcomingDiv = $('#upcoming');
    $('#submit').click(searchLocations);
    const symbols = {
        'Sunny': '&#x2600;',
        'Partly sunny': '&#x26C5;',
        'Overcast': '&#x2601;',
        'Rain': '&#x2614;',
        'Degrees': '&#176;'
    };

    function searchLocations() {
        let userInput = $('#location').val();
        currentDiv.empty();
        upcomingDiv.empty();
        let code = undefined;
        $.get({
            method: 'GET',
            url: 'https://judgetests.firebaseio.com/locations.json'
        }).then(validateLocation).catch(displayError);

        function validateLocation(locations) {
            for (let location of locations) {
                if (location.name == userInput) {
                    code = location.code;
                    break;
                }
            }
            if (code == undefined) return Promise.reject('invalid location');
            Promise.all([
                $.get(`https://judgetests.firebaseio.com/forecast/today/${code}.json`),
                $.get(`https://judgetests.firebaseio.com/forecast/upcoming/${code}.json`)
            ]).then(handleForecast).catch(displayError);
        }
    }

    function handleForecast([today, upcoming]) {
        currentDiv.append(`<div class="label">Current conditions</div>`);
        let currentDivHtml = `
            <span class="condition symbol">${symbols[today.forecast.condition]}</span>
            <span class="condition">
                <span class="forecast-data">${today.name}</span>
                <span class="forecast-data">${today.forecast.low}&#176; / ${today.forecast.high}&#176;</span>
                <span class="forecast-data">${today.forecast.condition}</span>
            </span>`;
        currentDiv.append(currentDivHtml);

        upcomingDiv.append(`<div class="label">Three-day forecast</div>`);
        // console.log(upcoming);
        for (let day of upcoming.forecast) {
            upcomingDiv.append(generateDayHtml(day));
        }
        $('#forecast').show();
    }

    function generateDayHtml(day) {
        let html = `<span class="upcoming">
            <span class="symbol">${symbols[day.condition]}</span>
            <span class="forecast-data">${day.low}&#176; / ${day.high}&#176;</span>
            <span class="forecast-data">${day.condition}</span>
        </span>`;

        return html;
    }

    function displayError(err) {
        let errorDiv = $('<div style="color:red">').text(err);
        $(document.body).prepend(errorDiv);
        setTimeout(function () {
            $(errorDiv).fadeOut(function () {
                $(errorDiv).remove();
            });
        }, 3000);
    }
}