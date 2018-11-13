class Rectangle {
    constructor(width, height, color) {
        this.width = width;
        this.height = height;
        this.color = color;
    }
    calcArea() {
        return this.height * this.width;
    }
}

// let rectangle = new Rectangle(4, 5, 'red');
// console.log(rectangle);