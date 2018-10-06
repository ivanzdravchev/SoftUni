function numberSequence(length, scope) {
    let arr = [1];
    for(let i = 1; i < length; i++) {
        let sum = 0;
        for(let j = i - scope; j < i; j++) {
            sum += arr[j] || 0;
        }
        arr[i] = sum;
    }
    return arr.join(" ");
}

//console.log(numberSequence(6, 3));