function compoundInterest(array) {
    let principalSum = array[0];
    let interestRate = array[1] / 100;
    let compoundFrequency = 12 / array[2];
    let years = array[3];

    let result = principalSum * Math.pow(1 + interestRate / compoundFrequency, compoundFrequency * years);
    return Math.round(result * 100) / 100;
}

//let arr = [1500, 4.3, 3, 6];
//console.log(compoundInterest(arr));