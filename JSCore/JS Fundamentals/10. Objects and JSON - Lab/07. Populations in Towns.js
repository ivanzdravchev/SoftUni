function populationsInTowns(arr) {
    let map = new Map();
    for (let line of arr) {
        let [city, population] = line.split(' <-> ');
        if (!map.has(city)) {
            map.set(city, +population);
        } else {
            map.set(city, map.get(city) + +population);
        }
    }
    for (let [key, value] of map) {
        console.log(`${key} : ${value}`);
    }
}

// let arr = [
//     'Sofia <-> 120000',
//     'Montana <-> 20000',
//     'Sofia <-> 1111',
//     'Plovdiv <-> 123456'
// ]
// populationsInTowns(arr);