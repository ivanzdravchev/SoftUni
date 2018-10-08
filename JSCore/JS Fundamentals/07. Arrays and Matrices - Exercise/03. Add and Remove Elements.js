function addAndRemoveElements(arr) {
    let num = 1;
    let resultArray = [];
    arr.map(command => {
        if(command === 'add') {
            resultArray.push(num);
        } else {
            resultArray.pop();
        }
        num++;
    });
    return resultArray.join("\n") || "Empty";
}

// let arr = ['add', 'add', 'remove', 'add', 'remove', 'add'];
// console.log(addAndRemoveElements(arr));