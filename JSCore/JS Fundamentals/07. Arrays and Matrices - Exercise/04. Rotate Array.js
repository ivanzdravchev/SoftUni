function rotateArray(arr) {
    let times = arr.pop();
    times %= arr.length;
    for(let i = 0; i < times; i++) {
        arr.unshift(arr.pop());
    }
    return arr.join(" ");
}

// let arr = ['1', '2', '3', '4', '2'];
// console.log(rotateArray(arr));