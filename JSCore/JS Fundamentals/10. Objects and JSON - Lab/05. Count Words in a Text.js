function countWords(arr) {
    let text = arr.join(' ');
    let dictionary = {};
    let wordArray = text.match(new RegExp(`[A-Za-z0-9_]+`, 'g'));
    for (let word of wordArray) {
        dictionary[word] ? dictionary[word]++ : dictionary[word] = 1
    }
    return JSON.stringify(dictionary);
}

// let arr = ['Far too slow, you\'re far too slow.'];
// console.log(countWords(arr));