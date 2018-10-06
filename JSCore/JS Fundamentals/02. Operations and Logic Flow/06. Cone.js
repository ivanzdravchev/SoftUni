function cone(radius, height) {
    let volume = Math.PI * (radius ** 2) * (height / 3);
    let slant = Math.sqrt(radius * radius + height * height);
    let surfaceArea = Math.PI * radius * (radius + slant);
    console.log(`volume = ${volume}`);
    console.log(`area = ${surfaceArea}`);
}

//cone(3, 5);