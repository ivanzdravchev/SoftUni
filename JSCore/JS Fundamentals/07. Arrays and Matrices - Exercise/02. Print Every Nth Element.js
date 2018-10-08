function printEveryNthElement(arr) {
    let step = +arr.pop();
    let newArr = arr.filter((el, index) => index % step == 0);
    return newArr.join('\n');
}

// let arr = ['5', '20', '17', '77', '42', '2'];
// console.log(printEveryNthElement(arr));