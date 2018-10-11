function parseData(arr) {
    let regex = /^([A-Z][A-Za-z]*) - ([1-9][0-9]*) - ([A-Za-z0-9- ]+)$/;
    for(let el of arr) {
        let match = regex.exec(el);
        if (match) {
            console.log(`Name: ${match[1]}\nPosition: ${match[3]}\nSalary: ${match[2]}`);
        }
    }
}

// let arr = [
//     'Ivan - 500 - Employee',
//     'Gosho -600 - Other',
//     'Test - 1 - o9-8o'
// ]
// parseData(arr);