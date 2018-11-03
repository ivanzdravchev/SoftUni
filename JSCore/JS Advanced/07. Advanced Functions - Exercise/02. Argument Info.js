function argumentsInfo() {
    let map = new Map();
    for (let i = 0; i < arguments.length; i++) {
        let value = arguments[i];
        let type = typeof value;
        console.log(`${type}: ${value}`);
        if (!map.has(type)) {
            map.set(type, 0)
        }
        map.set(type, map.get(type) + 1);
    }
    [...map].sort((a, b) => {
        return b[1] - a[1];
    }).forEach(el => {
        console.log(`${el[0]} = ${el[1]}`);
    });
}

// argumentsInfo({ name: 'bob'}, 3.333, 9.999);