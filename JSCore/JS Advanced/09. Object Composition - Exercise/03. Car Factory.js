function carFactory(car) {
    let carFactory = (function() {
        let newCar = {
            model: car.model
        };

        let engines = {
            120: { power: 120, volume: 2400 },
            90: { power: 90, volume: 1800 },
            200: { power: 200, volume: 3500 }
        }
        newCar.engine = engines[Object.keys(engines).sort((a, b) => a - b)
            .filter(k => k >= car.power)[0]];

        newCar.carriage = {
            type: car.carriage,
            color: car.color
        };

        let size = car.wheelsize % 2 == 0 ? car.wheelsize - 1 : car.wheelsize;
        newCar.wheels = [size, size, size, size];

        return newCar;
    })();

    return carFactory;
}

// let car = {
//     model: 'VW Golf II',
//     power: 90,
//     color: 'blue',
//     carriage: 'hatchback',
//     wheelsize: 14
// };

console.log(carFactory(car));