function sumByTown(arr) {
    let dictionary = {};
    for (let i = 0; i < arr.length; i += 2) {
        let [town, income] = [arr[i], +arr[i + 1]];
        if (dictionary[town] == undefined) {
            dictionary[town] = income;
        } else {
            dictionary[town] += income;
        }
    }
    return JSON.stringify(dictionary);
}

// let arr = ['Sofia', '20', 'Varna', '15', 'Sofia', '7', 'Varna', '11'];
// console.log(sumByTown(arr));