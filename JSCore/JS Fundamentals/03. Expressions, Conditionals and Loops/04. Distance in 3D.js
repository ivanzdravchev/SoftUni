function calculateDistance(array) {
    let distance = Math.sqrt((array[0] - array[3]) ** 2 +
     (array[1] - array[4]) ** 2 + (array[2] - array[5]) ** 2);
    return distance;
}

//let arr = [1, 1, 0, 5, 4, 0];
//console.log(calculateDistance(arr));