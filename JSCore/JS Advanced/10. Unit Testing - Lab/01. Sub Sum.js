function subSum(arr, startIndex, endIndex) {
    if (!Array.isArray(arr)) {
        return NaN;
    }
    if (startIndex < 0) {
        startIndex = 0;
    }
    if (endIndex >= arr.length) {
        endIndex = arr.length - 1;
    }
    let sum = 0;
    for (let i = startIndex; i <= endIndex; i++) {
        sum += +arr[i];
    }
    return sum;
}

// console.log(subSum({}, 1, 3));
// console.log(subSum([1, 2, 3, 4], 1, 3));