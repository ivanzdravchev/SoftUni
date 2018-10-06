function triangleOfStars(num) {
    let result = "";
    for(let i = 1; i <= num; i++) {
        result += "*".repeat(i) + "\n";
    }
    for(let i = num - 1; i > 0; i--) {
        result += "*".repeat(i) + "\n";
    }
    return result;
}

//console.log(triangleOfStars(5));