function quadraticEquation(a, b, c) {
    let discriminant = b ** 2 - (4 * a * c);
    if (discriminant < 0) {
        return "No";
    } else if (discriminant == 0) {
        return -b / (2 * a);
    } else {
        let res1 = (-b + Math.sqrt(discriminant)) / (2 * a);
        let res2 = (-b - Math.sqrt(discriminant)) / (2 * a);
        if (res1 > res2) {
            return `${res2}\n${res1}`;
        } else {
            return `${res1}\n${res2}`;
        }
    }
}

//console.log(quadraticEquation(6, 11, -35));