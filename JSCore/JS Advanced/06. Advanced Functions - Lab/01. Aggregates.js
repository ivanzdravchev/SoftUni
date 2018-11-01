function aggregates(arr) {
    console.log('Sum = ' + arr.reduce((acc, el) => acc + el));
    console.log('Min = ' + arr.reduce((acc, el) => Math.min(acc, el)));
    console.log('Max = ' + arr.reduce((acc, el) => Math.max(acc, el)));
    console.log('Product = ' + arr.reduce((acc, el) => acc * el));
    console.log('Join = ' + arr.reduce((acc, el) => acc + el , ''));
}

// let arr = [2, 3, 10, 5];
// aggregates(arr);