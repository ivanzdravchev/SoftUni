function processOddNumbers(arr) {
    let returnArray = [];
    for(let i = 0; i < arr.length; i++) {
        if(i % 2 == 1) {
            returnArray.unshift(arr[i] * 2);
        }
    }
    return returnArray.join(" ");
}

//let arr = [10, 15, 20, 25];
//console.log(processOddNumbers(arr));

//console.log(arr.reverse());
//console.log(arr);