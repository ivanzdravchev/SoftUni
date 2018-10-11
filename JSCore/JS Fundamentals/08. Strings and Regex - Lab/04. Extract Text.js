function extractText(str) {
    let arr = [];
    let startIndex = str.indexOf('(');
    let endIndex = str.indexOf(')', startIndex);
    while (startIndex > -1 && endIndex > -1) {
        let substring = str.substring(++startIndex, endIndex);
        arr.push(substring);
        startIndex = str.indexOf('(', endIndex);
        endIndex = str.indexOf(')', startIndex);
    }
    return arr.join(', ');
}

// let str = 'Rakiya (Bulgarian brandy) is self-made liquor (alcoholic drink)';
// console.log(extractText(str));