function tripLength(arr) {
    let point1 = {x: arr[0], y: arr[1]};
    let point2 = {x: arr[2], y: arr[3]};
    let point3 = {x: arr[4], y: arr[5]};
    
    let distance12 = Math.sqrt((point1.x - point2.x) ** 2 + (point1.y - point2.y) ** 2);
    let distance13 = Math.sqrt((point1.x - point3.x) ** 2 + (point1.y - point3.y) ** 2);
    let distance23 = Math.sqrt((point2.x - point3.x) ** 2 + (point2.y - point3.y) ** 2);

    if (distance12 <= distance13 && distance23 <= distance13) {
        console.log(`1->2->3: ${distance12 + distance23}`);
    } else if (distance12 <= distance23 && distance13 < distance23) {
        console.log(`2->1->3: ${distance13 + distance12}`)
    } else {
        console.log(`1->3->2: ${distance13 + distance23}`);
    }
}

//let arr = [0, 0, 2, 0, 4, 0];
//tripLength(arr);