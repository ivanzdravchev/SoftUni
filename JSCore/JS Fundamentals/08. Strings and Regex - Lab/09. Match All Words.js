function matchAllWords(str) {
    let regex = /\w+/g;
    let words = [];
    words = str.match(regex);
    return words.join('|');
}

// let str = 'A Regular Expression needs to_ _have the global flag in order to match all occurrences in the text.';
// console.log(matchAllWords(str));