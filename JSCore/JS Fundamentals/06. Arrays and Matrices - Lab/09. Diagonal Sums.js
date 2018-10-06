function diagonalSum(matrix) {
    let mainDiagonalSum = 0, secondaryDiagonalSum = 0;
    for (let row = 0; row < matrix.length; row++) {
        mainDiagonalSum += matrix[row][row];
        secondaryDiagonalSum += matrix[row][matrix.length - row - 1];
    }
    return `${mainDiagonalSum} ${secondaryDiagonalSum}`;
}

// let matrix = [
//     [20, 40],
//     [10, 60]
// ];
// console.log(diagonalSum(matrix));