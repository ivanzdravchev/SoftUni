let manager = (function () {
    let resources = {
        protein: 0,
        carbohydrate: 0,
        fat: 0,
        flavour: 0
    }
    let food = {
        apple: {
            carbohydrate: 1,
            flavour: 2
        },
        coke: {
            carbohydrate: 10,
            flavour: 20
        },
        burger: {
            carbohydrate: 5,
            fat: 7,
            flavour: 3
        },
        omelet: {
            protein: 5,
            fat: 1,
            flavour: 1
        },
        cheverme: {
            protein: 10,
            carbohydrate: 10,
            fat: 10,
            flavour: 10
        }
    };

    function restock(type, quantity) {
        resources[type] += +quantity;
        return 'Success';
    }

    function prepare(type, quantity) {
        for (let ingredient of Object.keys(food[type])) {
            if (food[type][ingredient] * quantity > resources[ingredient]) {
                return `Error: not enough ${ingredient} in stock`;
            }
        }
        for (let ingredient of Object.keys(food[type])) {
            resources[ingredient] -= food[type][ingredient] * quantity;
        }
        return 'Success';
    }

    function report() {
        return [...Object.keys(resources)].map(el => {
            return `${el}=${resources[el]}`;
        }).join(' ');
    }

    return function (command) {
        let tokens = command.split(' ');
        let commandType = tokens.shift();
        switch(commandType) {
            case 'restock': return restock(...tokens);
            case 'prepare': return prepare(...tokens);
            case 'report': return report();
        }
    } 
})();

// console.log(manager('restock carbohydrate 10'));
// console.log(manager('restock flavour 10'));
// console.log(manager('prepare apple 1'));
// console.log(manager('restock fat 10'));
// console.log(manager('prepare burger 1'));
// console.log(manager('report'));