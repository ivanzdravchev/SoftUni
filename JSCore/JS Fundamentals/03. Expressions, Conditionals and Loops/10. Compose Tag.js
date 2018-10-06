function composeTag(arr) {
    let location = arr[0];
    let altText = arr[1];
    return `<img src="${location}" alt="${altText}">`;
}

//let arr = ['image.jpg', 'image'];
//console.log(composeTag(arr));