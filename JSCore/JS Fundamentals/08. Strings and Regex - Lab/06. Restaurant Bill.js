function restaurantBill(arr) {
    let products = arr.filter((item, index) => index % 2 == 0);
    let bill = arr.filter((item, index) => index % 2 == 1)
        .reduce((acc, el) => acc += +el, 0);
    return `You purchased ${products.join(', ')} for a total sum of ${bill}`;
}

// let arr = ['Beer', '2.65', 'Soup', '7.80', 'Lasagna', '5.69'];
// console.log(restaurantBill(arr));