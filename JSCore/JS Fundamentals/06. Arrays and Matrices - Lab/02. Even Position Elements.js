function evenPositionElements(arr) {
    let str = "";
    for (const el in arr) {
        if(el % 2 == 0)
            str += arr[el] + " ";
    }
    return str;
}

//let arr = ['10', '20', '30'];
//console.log(evenPositionElements(arr));