function triangleOfDollars(height) {
    let result = "";
    for(let i = 1; i <= height; i++) {
        for(let j = 0; j < i; j++) {
            result += "$";
        }
        result += "\n";
    }
    return result;
}

//console.log(triangleOfDollars(4));