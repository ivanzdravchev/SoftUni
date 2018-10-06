function calculateDegrees(input) {
    input %= 400;
    if(input < 0) {
        input = 400 + input;
    }
    input *= 0.9;
    return input;
    
}

//console.log(calculateDegrees(-50));