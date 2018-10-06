function multiplicationTable(length) {
    let result = '<table border="1">';
    result += '<tr><th>x</th>';
    for(let i = 1; i <= length; i++) {
        result += '<th>' + i + '</th>';
    }
    result += '</tr>';

    for(let row = 1; row <= length; row++) {
        result += `<tr><th>${row}</th>`;
        for(let col = 1; col <= length; col++) {
            result += `<td>${row * col}</td>`;
        }
        result += '</tr>';
    }

    result += "</table>";
    return result;
}

//console.log(multiplicationTable(5));