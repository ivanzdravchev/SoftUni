function townsToJson(arr) {
    let objectsArray = [];
    for(let i = 1; i < arr.length; i++) {
        let tokens = arr[i].split('|').map(x => x.trim()).filter(x => x != '');
        objectsArray.push({ Town: tokens[0],
            Latitude: +tokens[1],
            Longitude: +tokens[2]});
    }
    return JSON.stringify(objectsArray);
}

// let arr = [
//     '| Town | Lattitude | Longitude |',
//     ' | Sofia | 42.534 | 23.323 |',
//     '| Beijing|39.99| 116.11'
// ];
// console.log(townsToJson(arr));