function JSONToHtmlTable(arr) {
    function escapeChars(str) {
        str = str.replace(/&/g, '&amp;');
        str = str.replace(/"/g, '&quot;');
        str = str.replace(/</g, '&lt;');
        str = str.replace(/>/g, '&gt;');
        str = str.replace(/\'/g, '&#39;');
        return str;
    }

    let result = '<table>\n  <tr>';
    let parsedArray = JSON.parse(arr);
    let keys = Object.keys(parsedArray[0]);

    for (let key of keys) {
        result += `<th>${key}</th>`;
    }

    result += '</tr>\n';
    for (let obj of parsedArray) {
        result += '  <tr>';
        for (let value of Object.keys(obj)) {
            result += `<td>${escapeChars(obj[value].toString())}</td>`;
        }
        result += '</tr>\n';
    }
    result += '</table>';
    return result;
}

// let arr = [
//     '[{"Name":"Pesho","Age":25,"City":"Sofia"}]'
// ];
// console.log(JSONToHtmlTable(arr));