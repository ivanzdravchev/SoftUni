function triangleArea(a, b, c) {
    let halfPerimeter = (a + b + c) / 2;
    let area = Math.sqrt(halfPerimeter * (halfPerimeter - a) * (halfPerimeter - b) * (halfPerimeter - c));
    return area;
}

//console.log(triangleArea(2, 3.5, 4));