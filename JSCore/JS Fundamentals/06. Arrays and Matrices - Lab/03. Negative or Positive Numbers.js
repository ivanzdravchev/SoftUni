function addNumbers(arr) {
    let array = [];
    for(let i = 0; i < arr.length; i++) {
        if(arr[i] < 0) {
            array.unshift(arr[i]);
        } else {
            array.push(arr[i]);
        }
    }
    return array.join("\n");
}

//let arr = [3, -2, 0, -1];
//console.log(addNumbers(arr));