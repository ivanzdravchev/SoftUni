const Car = require('../models/Car');
const Rent = require('../models/Rent');

module.exports = {
    addGet: (req, res) => {
        res.render('car/add');
    },
    addPost: (req, res) => {
        const carBody = req.body;
        if (!carBody.model || !carBody.imageUrl || !carBody.pricePerDay) {
            carBody.error = 'Please fill all fields!';
            res.render('car/add', carBody);
            return;
        }
        carBody.pricePerDay = Number(carBody.pricePerDay);

        Car.create(carBody).then(() => {
            res.redirect('all');
        }).catch(console.error);
    },
    allCars: (req, res) => {
        Car.find({ isRented: false })
            .then((cars) => {
                res.render('car/all', { cars });
            }).catch(console.error);
    },
    rentGet: (req, res) => {
        const carId = req.params.id;

        Car.findById(carId)
            .then((car) => {
                res.render('car/rent', car)
            }).catch(console.error);
    },
    rentPost: async (req, res) => {
        const car = req.params.id; // comes from url -> .params
        const user = req.user._id;
        const days = Number(req.body.days);

        try {
            const rent = await Rent.create({ days, user, car });
            const carById = await Car.findById(car);
            carById.isRented = true;
            await carById.save();
            res.redirect('/car/all');
        } catch(err) {
            console.log(err);
        }

        /*Rent.create({ days, user, car })
            .then((rent) => {
                Car.findById(car)
                    .then((c) => {
                        c.isRented = true;

                        return c.save();
                    })
                    .then(() => {
                        res.redirect('/car/all');
                    })
                res.redirect('/car/all');
            }).catch(console.error);*/
    },
    editGet: (req, res) => {
        Car.findById(req.params.id)
            .then((car) => {
                if (!car) {
                    res.redirect('/car/all');
                    return;
                }
                res.render('car/edit', car);
                return;
            }).catch(console.error);
    },
    editPost: (req, res) => {
        let id = req.params.id
        let carBody = req.body;
        Car.findById(id)
            .then((car) => {
                if (!car) {
                    res.redirect('/car/all');
                    return;
                }

                if (!carBody.model || !carBody.imageUrl || !carBody.pricePerDay) {
                    carBody.error = 'Please fill all fields!';
                    res.render('car/edit', carBody);
                    return;
                }

                car.model = carBody.model;
                car.imageUrl = carBody.imageUrl;
                car.pricePerDay = carBody.pricePerDay;
                car.save()
                    .then(() => {
                        res.redirect('/car/all');
                        return;
                    })
            }).catch(console.error);
    }
}