function orbit(arr) {
    let rows = arr[0];
    let cols = arr[1];

    let starRow = arr[2];
    let starCol = arr[3];

    let matrix = [];
    for(let i = 0; i < rows; i++) {
        matrix[i] = [];
    }

    for(let row = 0; row < rows; row++) {
        for(let col = 0; col < cols; col++) {
            matrix[row][col] = Math.max(Math.abs(starRow - row), Math.abs(starCol - col)) + 1;
        }
    }

    return matrix.map(row => row.join(' ')).join('\n');
}

//let arr = [4, 4, 2, 2];
//console.log(orbit(arr));