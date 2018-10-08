function extractSequence(arr) {
    let newArray = [];
    let biggestNum = arr.shift();
    newArray.push(biggestNum);
    arr.forEach(el => {
        if(el >= biggestNum) {
            newArray.push(el);
            biggestNum = el;
        }
    });
    return newArray.join('\n');
}

function secondSolution(arr) {
    let resultString = arr.filter((x, i) => x >= Math.max(...arr.slice(0, i + 1))).join('\n');
    return resultString;
}

// let arr = [1, 3, 8, 4, 10, 12, 3, 2, 24];
// let arr2 = [5, 4, 2];
// console.log(extractSequence(arr));
// console.log(secondSolution(arr));