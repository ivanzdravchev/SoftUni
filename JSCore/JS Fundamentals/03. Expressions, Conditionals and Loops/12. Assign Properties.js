function assignProperties(arr) {
    let prop1 = arr[0];
    let prop1value = arr[1];
    let prop2 = arr[2];
    let prop2value = arr[3];
    let prop3 = arr[4];
    let prop3value = arr[5];

    let object = new Object();
    object[prop1] = prop1value;
    object[prop2] = prop2value;
    object[prop3] = prop3value;
    return object;
}

//let arr = ['name', 'Pesho', 'age', '23', 'gender', 'male'];
//console.log(assignProperties(arr));