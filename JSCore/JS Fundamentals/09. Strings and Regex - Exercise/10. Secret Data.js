function secretData(arr) {
    let regexName = /\*[A-Z][A-Za-z]*(?=\s|\t|$)/g;
    let regexPhone = /\+[0-9-]{10}(?=\s|\t|$)/g;
    let regexID = /![A-Za-z0-9]+(?=\s|\t|$)/g;
    let regexBase = /_[A-Za-z0-9]+(?=\s|\t|$)/g;

    for(let line of arr) {
        line = line.replace(regexName, el => '|'.repeat(el.length))
            .replace(regexPhone, el => '|'.repeat(el.length))
            .replace(regexID, el => '|'.repeat(el.length))
            .replace(regexBase, el => '|'.repeat(el.length));
        console.log(line);
    }
}


// let arr = [
//     'Agent *Ivankov was in the room when it all happened.',
//     'The person in the room was heavily armed.',
//     'Agent *Ivankov had to act quick in order.',
//     'He picked up his phone and called some unknown number. ',
//     'I think it was +555-49-796...',
//     'I can\'t really remember...',
//     'He said something about "finishing work with subject !2491a23BVB34Q and returning to Base _Aurora21".',
//     'Then after that he disappeared from my sight.',
//     'As if he vanished in the shadows.',
//     'A moment, shorter than a second, later, I saw the person flying off the top floor.',
//     'I really don\'t know what happened there.',
//     'This is all I saw, that night.',
//     'I cannot explain it myself...'
// ]

// secretData(arr);