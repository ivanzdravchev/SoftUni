function aggregateTable(arr) {
    let townArray = [];
    let sum = 0;
    for(let el of arr) {
        let pair = el.split('|').filter(x => x != '');
        townArray.push(pair[0].trim());
        sum += +pair[1].trim();
    }
    return townArray.join(', ') + `\n${sum}`;
}

// let arr = [
//     '| Sofia       |  300',
//     '| Veliko Tarnovo    |  500',
//     '| Yambol    | 275'
// ];
// console.log(aggregateTable(arr));