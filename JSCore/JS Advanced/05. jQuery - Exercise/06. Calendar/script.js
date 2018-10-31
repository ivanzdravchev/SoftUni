function calendar([day, month, year]) {
    let months = ['January', 'February', 'March', 'April', 'May', 'June',
        'July', 'August', 'September', 'October', 'November', 'December'];
    let weekDays = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];
    $('#content').append($('<table>').append($('<caption>').text(months[month - 1] + " " + year)).append($('<tr>')));
    for (let i = 0; i < 7; i++) {
        $('tr:first').append($('<th>').text(weekDays[i]));
    }

    let currDay = 1;
    let totalDays, firstDateWeekDay, lastDateWeekDay = 0; // 0 - 6 Mon - Sun
    let temp = new Date(year, month, 0);
    totalDays = temp.getDate();
    lastDateWeekDay = temp.getDay() - 1;
    if (lastDateWeekDay == -1) {
        lastDateWeekDay = 6;
    }
    temp = new Date(year, month - 1, 1);
    firstDateWeekDay = temp.getDay() - 1;
    if (firstDateWeekDay == -1) {
        firstDateWeekDay = 6;
    }

    let weeks = Math.ceil(totalDays / 7);
    if ((firstDateWeekDay + totalDays + (6 - lastDateWeekDay)) > weeks * 7) {
        weeks++;
    }
    for (let i = 0; i < weeks; i++) {
        $('table').append($('<tr>'));
        for (let j = 0; j < 7; j++) {
            if (i == 0 && firstDateWeekDay > j) {
                $('tr:last').append($('<td>').text(''));
            } else if (i == 0 && firstDateWeekDay <= j) {
                $('tr:last').append($('<td>').text(currDay));
                currDay++;
            } else if (i == weeks - 1 && lastDateWeekDay >= j) {
                $('tr:last').append($('<td>').text(currDay));
                currDay++;
            } else if (i == weeks - 1 && lastDateWeekDay < j) {
                $('tr:last').append($('<td>').text(''));
            } else {
                $('tr:last').append($('<td>').text(currDay));
                currDay++;
            }
        }
    }
    $('td:contains(' + day + ')').addClass('today');
}