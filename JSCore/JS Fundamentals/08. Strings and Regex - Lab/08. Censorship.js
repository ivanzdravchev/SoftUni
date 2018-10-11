function censorship(text, arr) {
    for(let current of arr) {
        let replaceText = '-'.repeat(current.length);
        text = text.split(current).join(replaceText);
    }
    return text;
}

// let text = 'roses are red, violets are blue';
// let arr = [', violets are', 'red'];
// console.log(censorship(text, arr));