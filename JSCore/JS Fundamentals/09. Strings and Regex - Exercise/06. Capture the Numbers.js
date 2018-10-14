function captureNumbers(arr) {
    let regex = /\d+/g;
    let numbers = [];
    for(let line of arr) {
        let match = regex.exec(line);
        while (match) {
            numbers.push(match[0]);
            match = regex.exec(line);
        }
    }
    return numbers.join(' ');
}

// let arr = ['Asd311', 'Time is 23:59'];
// console.log(captureNumbers(arr));