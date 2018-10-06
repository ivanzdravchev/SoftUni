function calculateDistance(array) {
    let v1meters = (array[0] / 3600) * array[2];
    let v2meters = (array[1] / 3600) * array[2];
    return Math.abs(v1meters - v2meters) * 1000;
}

//let arr = [0, 60, 3600];
//console.log(calculateDistance(arr));