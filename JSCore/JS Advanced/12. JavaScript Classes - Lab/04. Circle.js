class Circle {
    constructor(radius) {
        this.radius = radius;
    }

    get diameter() {
        return this.radius * 2;
    }

    set diameter(diameter) {
        this.radius = diameter / 2;
    }

    get area() {
        return Math.PI * this.radius * this.radius;
    }
}

// let circle = new Circle(0.8);
// console.log(`Radius: ${circle.radius}`);
// console.log(`Diameter: ${circle.diameter}`);
// console.log(`Area: ${circle.area}`);