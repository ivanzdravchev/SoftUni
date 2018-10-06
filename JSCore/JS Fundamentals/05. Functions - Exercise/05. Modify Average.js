function modifyNumber(num) {
    function modify(str) {
        return str + '9';
    }
    function digitSum(str) {
        let sum = 0;
        for (let i = 0; i < str.length; i++) {
            sum += +str[i];
        }
        return sum / str.length;
    }

    let str = num.toString();
    while(digitSum(str) <= 5) {
        str = modify(str);
    }
    return +str;
}

//console.log(modifyNumber(1));
//console.log(modifyNumber(58));