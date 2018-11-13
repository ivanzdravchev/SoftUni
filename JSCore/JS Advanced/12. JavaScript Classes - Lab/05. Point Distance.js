class Point {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    static distance(p1, p2) {
        return Math.sqrt(Math.pow(Math.abs(p1.x - p2.x), 2) +
             Math.pow(Math.abs(p1.y - p2.y), 2)); 
    }
}

// let p1 = new Point(0, 0);
// let p2 = new Point(4, 3);
// console.log(Point.distance(p1, p2));