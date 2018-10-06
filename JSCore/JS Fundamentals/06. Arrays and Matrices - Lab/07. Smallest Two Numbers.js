function smallestTwoNumbers(arr) {
    return arr.sort((a, b) => a - b).slice(0, 2).join(" ");
}

//let arr = [30, 15, 50, 5];
//console.log(smallestTwoNumbers(arr));