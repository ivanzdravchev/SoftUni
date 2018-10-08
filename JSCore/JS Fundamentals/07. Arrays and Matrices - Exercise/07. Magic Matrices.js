function magicMatrices(matrix) {
    function rowSum(arr) {
        return arr.reduce((acc, curr) => acc + curr, 0);
    }
    function colSum(matrix, column) {
        let colSum = 0;
        for(let row = 0; row < matrix.length; row++) {
            colSum += matrix[row][column];
        }
        return colSum;
    }
    let sum = rowSum(matrix[0]);
    for(let i = 0; i < matrix.length; i++) {
        if (sum != rowSum(matrix[i])) {
            return false;
        }
        if (sum != colSum(matrix, i)) {
            return false;
        }
    }
    return true;
}

// let matrix = [
//     [4, 5, 6],
//     [6, 5, 4],
//     [5, 5, 5]
// ];

// magicMatrices(matrix);