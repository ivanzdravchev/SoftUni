function diagonalAttack(arr) {
    let matrix = arr.map(el => el.split(' ').map(el => +el));
    let mainDiagonal = 0;
    let otherDiagonal = 0;
    for(let row = 0; row < matrix.length; row++) {
        mainDiagonal += matrix[row][row];
        otherDiagonal += matrix[row][matrix.length - row - 1];
    }
    if(mainDiagonal == otherDiagonal) {
        for(let row = 0; row < matrix.length; row++) {
            for(let col = 0; col < matrix[row].length; col++) {
                if (row != col && col != matrix[row].length - row - 1) {
                    matrix[row][col] = mainDiagonal;
                }
            }
        }
    }
    return matrix.map(row => row.join(' ')).join('\n');
}

// let arr = [
//     '5 3 12 3 1',
//     '11 4 23 2 5',
//     '101 12 3 21 10',
//     '1 4 5 2 2',
//     '5 22 33 11 1'
// ];
// console.log(diagonalAttack(arr));