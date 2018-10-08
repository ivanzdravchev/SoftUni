function sortArray(arr) {
    let resultArr = arr.sort((x, y) => {
        return x.length - y.length || x.localeCompare(y);
    });
    return resultArr.join('\n');
}

// let arr = ['test', 'Denyy', 'benyy', 'om', 'Default'];
// console.log(sortArray(arr));