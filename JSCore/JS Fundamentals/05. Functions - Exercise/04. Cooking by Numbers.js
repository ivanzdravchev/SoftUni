function cookingNumbers(arr) {
    function cook(num, action) {
        switch(action) {
            case "chop": return num / 2;
            case "dice": return Math.sqrt(num);
            case "spice": return num + 1;
            case "bake": return number * 3;
            case "fillet": return number * 0.8;
        }
    }

    let number = +arr[0];
    for(let i = 1; i < arr.length; i++) {
        number = cook(number, arr[i]);
        console.log(number);
    }
}

//let arr = ['9', 'dice', 'spice', 'chop', 'bake', 'fillet'];
//cookingNumbers(arr);