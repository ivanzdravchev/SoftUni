function oddEven(num) {
    if(!Number.isInteger(num))
        return "invalid";
    return num % 2 == 0 ? "even" : "odd";
}

//console.log(oddEven(-3));
//console.log(-3 % 2);