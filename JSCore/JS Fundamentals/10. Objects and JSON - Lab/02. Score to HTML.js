function scoreToHtml(arr) {
    function escapeChars(str) {
        str = str.replace(/&/g, '&amp;');
        str = str.replace(/"/g, '&quot;');
        str = str.replace(/</g, '&lt;');
        str = str.replace(/>/g, '&gt;');
        str = str.replace(/\'/g, '&#39;');
        return str;
    }
    let parsedArray = JSON.parse(arr);
    //console.log(parsedArray);
    let result = '<table>\n';
    result += '  <tr><th>name</th><th>score</th></tr>\n';
    for(let i = 0; i < parsedArray.length; i++) {
        let name = escapeChars(parsedArray[i].name);
        let score = parsedArray[i].score;
        result += `  <tr><td>${name}</td><td>${score}</td></tr>\n`;
    }
    result += '</table>';
    return result;
}

// let arr = [
//     '[{"name":"\'\'Pesho","score":479},{"name":"Gosho","score":205},{"name": "A, B & C","score":999}]'
// ];
// console.log(scoreToHtml(arr));