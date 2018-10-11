function matchDates(arr) {
    let regex = /\b([0-9]{1,2})-([A-Z][a-z]{2})-([0-9]{4})\b/g;
    let dates = [];
    for(let line of arr) {
        let match = regex.exec(line);
        while (match) {
            dates.push(match[0] + ` (Day: ${match[1]}, Month: ${match[2]}, Year: ${match[3]})`);
            match = regex.exec(line);
        }
    }
    
    return dates.join('\n');
}

// let arr = ['Some 20-Dec-1978 text 30-Dec-1999', 'more text 1-Jan-1900'];
// console.log(matchDates(arr));