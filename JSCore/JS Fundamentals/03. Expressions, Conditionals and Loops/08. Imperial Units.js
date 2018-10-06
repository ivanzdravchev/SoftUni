function imperialUnits(inches) {
    let inchesLeft = inches % 12;
    let ft = Math.floor(inches / 12);
    return `${ft}'-${inchesLeft}"`;
}

//console.log(imperialUnits(11));