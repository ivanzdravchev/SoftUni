function firstAndLastElements(arr) {
    let firstElements = arr.slice(1, 1 + arr[0]);
    let lastElements = arr.slice(arr.length - arr[0], arr.length);
    return firstElements.join(" ") + "\n" + lastElements.join(" ");
}

//let arr = [2, 7, 8, 9];
//console.log(firstAndLastElements(arr));