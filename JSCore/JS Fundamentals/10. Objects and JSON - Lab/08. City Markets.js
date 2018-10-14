function cityMarkets(arr) {
    let map = new Map();
    for (let line of arr) {
        let [town, product, quantityAndPrice] = line.split(' -> ');
        let [quantity, price] = quantityAndPrice.split(' : ');
        let income = +quantity * +price;
        if (!map.has(town)) {
            map.set(town, new Map());
            map.get(town).set(product, income);
        } else {
            if (!map.get(town).has(product)) {
                map.get(town).set(product, income);
            } else {
                map.get(town).set(product, town.get(product) + income);
            }
        }
    }

    let result = '';
    for (let [town, value] of map) {
        result += `Town - ${town}\n`;
        for (let [product, price] of map.get(town)) {
            result += `$$$${product} : ${price}\n`;
        }
    }
    return result;
}

// let arr = [
//     'Sofia -> Laptops HP -> 200 : 2000',
//     'Sofia -> Raspberry -> 200000 : 1500',
//     'Sofia -> Audi Q7 -> 200 : 100000',
//     'Montana -> Portokals -> 200000 : 1',
//     'Montana -> Qgodas -> 20000 : 0.2',
//     'Montana -> Chereshas -> 1000 : 0.3'
// ];
// console.log(cityMarkets(arr));