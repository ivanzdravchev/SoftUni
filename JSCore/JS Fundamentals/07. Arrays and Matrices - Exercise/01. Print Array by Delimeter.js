function printArray(arr) {
    let delimeter = arr.pop();
    return arr.join(delimeter);
}

// let arr = ['One', 'Two', 'Three', '-'];
// console.log(printArray(arr));