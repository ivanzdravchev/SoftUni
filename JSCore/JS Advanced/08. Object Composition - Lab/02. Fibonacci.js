let fibonacci = (function() {
    let previousNumber = 0;
    let currentNumber = 1;

    return function() {
        let newNum =  currentNumber + previousNumber;
        previousNumber = currentNumber;
        currentNumber = newNum;
        return previousNumber;
    }
})();

// console.log(fibonacci());
// console.log(fibonacci());
// console.log(fibonacci());

