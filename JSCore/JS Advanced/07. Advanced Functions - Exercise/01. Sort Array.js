function sort(arr, order) {
    let result = arr.sort((a, b) => {
        return order === 'asc' ? a - b : b - a;
    });
    return result;
}

// console.log(sort([14, 7, 17, 6, 8], 'asc'));