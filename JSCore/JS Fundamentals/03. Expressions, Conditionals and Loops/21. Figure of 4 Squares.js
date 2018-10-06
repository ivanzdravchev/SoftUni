function drawFigure(num) {
    let result = "";
    let rows = num;
    if (num % 2 == 0) {
        rows--;
    }
    for(let row = 1; row <= rows; row++) {
        if (row == 1 || row == rows || row == Math.ceil(rows / 2)) {
            result += `+${"-".repeat(num - 2)}+${"-".repeat(num - 2)}+\n`;
        } else {
            result += `|${" ".repeat(num - 2)}|${" ".repeat(num - 2)}|\n`;
        }
    }
    return result;
}

//console.log(drawFigure(4));