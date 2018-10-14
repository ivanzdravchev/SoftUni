function rakiya(arr) {
    let peachWeight = 0;  // 2.5
    let plumsWeight = 0;  // 10
    let cherryWeight = 0; // 25
    let otherWeight = 0;  // 1kg for 0.2l
    for (let i = 0; i < arr.length; i++) {
        let [fruit, weight] = arr[i].split(' ').filter(x => x != '');
        weight *= 1000;
        if (fruit == 'peach') {
            peachWeight += +weight;
        } else if (fruit == 'plum') {
            plumsWeight += +weight;
        } else if (fruit == 'cherry') {
            cherryWeight += +weight;
        } else {
            otherWeight += +weight;
        }
    }
    let cherryKompots = Math.floor((cherryWeight / 9) / 25);
    let peachKompots = Math.floor((peachWeight / 140) / 2.5);
    let plumKompots = Math.floor((plumsWeight / 20) / 10);
    let rakiyaLiters = (otherWeight / 5000) * 100 / 100;
    return `Cherry kompots: ${cherryKompots}\n` +
        `Peach kompots: ${peachKompots}\n` +
        `Plum kompots: ${plumKompots}\n` +
        `Rakiya liters: ${rakiyaLiters.toFixed(2)}`;
}

// let arr = [
//     'cherry 1.2',
//     'peach 2.2',
//     'plum 5.2',
//     'peach 0.1',
//     'cherry 0.2',
//     'cherry 5.0',
//     'plum 10',
//     'cherry 20.0',
//     'papaya 20'
// ];

// let arr2 = [
//     'apple 6',
//     'peach 25.158',
//     'strawberry 0.200',
//     'peach 0.1',
//     'banana 1.55',
//     'cherry 20.5',
//     'banana 16.8',
//     'grapes 205.65',
//     'watermelon 20.54'
// ];
// console.log(rakiya(arr2));