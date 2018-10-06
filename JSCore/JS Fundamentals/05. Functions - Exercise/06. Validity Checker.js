function validityChecker(arr) {
    function validate(point1, point2) {
        let sideOne = Math.abs(point1.x - point2.x) ** 2;
        let sideTwo = Math.abs(point1.y - point2.y) ** 2;
        let missingSide = Math.sqrt(sideOne + sideTwo);
        if(missingSide === parseInt(missingSide)) {
            return true;
        } else {
            return false;
        }
    }

    function printResult(point1, point2) {
        if (validate(point1, point2)) {
            console.log(`{${point1.x}, ${point1.y}} to {${point2.x}, ${point2.y}} is valid`);
        } else {
            console.log(`{${point1.x}, ${point1.y}} to {${point2.x}, ${point2.y}} is invalid`);
        }
    }

    let pointA = {x: arr[0], y: arr[1]};
    let pointB = {x: arr[2], y: arr[3]};
    let defaultPoint = {x: 0, y: 0};

    printResult(pointA, defaultPoint);
    printResult(pointB, defaultPoint);
    printResult(pointA, pointB);
}

//let arr = [3, 0, 0, 4];
//let arr2 = [2, 1, 1, 1];
//validityChecker(arr);
//validityChecker(arr2);