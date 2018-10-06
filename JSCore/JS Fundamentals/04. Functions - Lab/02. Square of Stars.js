function printSquare(num = 5) {
    let result = "";
    for(let i = 1; i <= num; i++) {
        result += "* ".repeat(num) + "\n";
    }
    return result;
}

//console.log(printSquare());