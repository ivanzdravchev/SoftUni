function calculateArea(width1, height1, width2, height2) {
    let area1 = width1 * height1;
    let area2 = width2 * height2;
    let commonArea = Math.min(height1, height2) * Math.min(width1, width2);
    return area1 + area2 - commonArea;
}

//console.log(calculateArea(2, 4, 5, 3));