function binaryToDecimal(input) {
    let sum = 0;
    for(let i = 0; i < input.length; i++) {
        if(input[i] == 1) {
            let multiplier = 1;
            for(let j = 0; j < input.length - i - 1; j++) {
                multiplier *= 2;
            }
            sum += multiplier;
        }
    }
    return sum;
}

//console.log(binaryToDecimal('00001001'));