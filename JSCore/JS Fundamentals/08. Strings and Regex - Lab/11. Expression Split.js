function expressionSplit(str) {
    let regex = '';
    let arr = str.split(/[\s(),.;]/g);
    return arr.filter(el => el != '').join('\n');
}

// console.log(expressionSplit('let sum = 4 * 4,b = "wow";'));