function aggregateElements(arr) {
    let totalSum = 0;
    let divisionSum = 0;
    let concatString = "";
    for(let i = 0; i < arr.length; i++) {
        totalSum += arr[i];
        divisionSum += 1 / arr[i];
        concatString += arr[i];
    }
    return `${totalSum}\n${divisionSum}\n${concatString}`;
}

//let arr = [3, 5, 8, 16];
//console.log(aggregateElements(arr));