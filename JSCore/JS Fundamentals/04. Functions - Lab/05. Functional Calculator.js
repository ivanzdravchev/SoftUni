function calculate(num1, num2, op) {
    let result = 0;
    switch(op) {
        case "+": result = num1 + num2; break;
        case "-": result = num1 - num2; break;
        case "*": result = num1 * num2; break;
        case "/": result = num1 / num2; break;
    }
    return result;
}

//console.log(calculate(18, -2, '*'));