function biggestNumberInMatrix(matrix) {
    let max = -99999;
    for(let row = 0; row < matrix.length; row++) {
        for(let col = 0; col < matrix[0].length; col++) {
            if(matrix[row][col] > max) {
                max = matrix[row][col];
            }
        }
    }
    return max;
}

// let matrix = [
//     [20, 50, 10],
//     [8, 33, 145]
// ];
// console.log(biggestNumberInMatrix(matrix));