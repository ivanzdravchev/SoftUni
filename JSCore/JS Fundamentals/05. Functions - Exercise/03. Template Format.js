function templateFormat(arr) {
    function appendQuestion(str, question) {
        str += '  <question>\n';
        str += `    ${question}\n`;
        str += '  </question>\n';
        return str;
    }

    function appendAnswer(str, answer) {
        str += '  <answer>\n';
        str += `    ${answer}\n`;
        str += '  </answer>\n';
        return str;
    }

    let xml = '<?xml version="1.0" encoding="UTF-8"?>\n<quiz>\n';
    for(let i = 0; i < arr.length; i += 2) {
        xml = appendQuestion(xml, arr[i]);
        xml = appendAnswer(xml, arr[i + 1]);
    }
    xml += '</quiz>';
    return xml;
}

//console.log(templateFormat(['Some question..', 'The answer']));
