function orderRectangles(matrix) {
    function createRectangle(width, height) {
        let rect = {
            width,
            height,
            area: () => width * height,
            compareTo: (other) => {
                if (rect.area() > other.area()) {
                    return -1;
                } else if (rect.area() < other.area()) {
                    return 1;
                } else {
                    if (width > other.width) {
                        return -1;
                    } else if (width < other.width) {
                        return 1;
                    } else {
                        return 0;
                    }
                }
            }
        };
        return rect;
    }
    let rectArr = [];
    for (let rect of matrix) {
        rectArr.push(createRectangle(rect[0], rect[1]));
    }
    return rectArr.sort((rec1, rec2) => rec1.compareTo(rec2));
}

// let matrix = [
//     [10, 5], [3, 20], [5, 12]
// ];
// console.log(orderRectangles(matrix));