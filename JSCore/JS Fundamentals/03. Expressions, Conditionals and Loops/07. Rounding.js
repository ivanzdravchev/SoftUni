function rounding(arr) {
    let number = arr[0];
    let round = arr[1] > 15 ? 15 : arr[1];
    let multiplier = 1;
    for(let i = 0; i < round; i++) {
        multiplier *= 10;
    }
    return Math.round(number * multiplier) / multiplier;
}

//let arr = [3.141592321321, 2];
//console.log(rounding(arr));