const encryption = require('../util/encryption');
const User = require('../models/User');
const Rent = require('../models/Rent');

module.exports = {
    registerGet: (req, res) => {
        res.render('user/register');
    },
    registerPost: async (req, res) => {
        const userBody = req.body;

        if (!userBody.username || !userBody.password || !userBody.repeatPassword) {
            userBody.error = 'Please fill all fields!';
            res.render('user/register', userBody);
            return;
        }

        if (userBody.password !== userBody.repeatPassword) {
            userBody.error = 'Both passwords should match!';
            res.render('user/register', userBody);
            return;
        }

        const salt = encryption.generateSalt();
        const hashedPass = encryption.generateHashedPassword(salt, userBody.password);
        try {
            const user = await User.create({
                username: userBody.username,
                hashedPass,
                firstName: userBody.firstName,
                lastName: userBody.lastName,
                salt,
                roles: ['User']
            });

            req.logIn(user, (err) => {
                if (err) {
                    userBody.error = err;
                    res.render('user/register', userBody);
                    return;
                } else {
                    res.redirect('/');
                }
            });
        } catch (err) {
            userBody.error = err;
            res.render('user/register', userBody);
        }
    },
    logout: (req, res) => {
        req.logout();
        res.redirect('/');
    },
    loginGet: (req, res) => {
        res.render('user/login');
    },
    loginPost: async (req, res) => {
        const userBody = req.body;
        try {
            const user = await User.findOne({ username: userBody.username });
            if (!user) {
                userBody.error = 'Invalid username!';
                res.render('user/login', userBody);
                return;
            }
            if (!user.authenticate(userBody.password)) {
                userBody.error = 'Invalid password!';
                res.render('user/login', userBody);
                return;
            }
            req.logIn(user, (err) => {
                if (err) {
                    userBody.error = err;
                    res.render('user/login', userBody);
                    return;
                } else {
                    res.redirect('/');
                }
            });
        } catch(err) {
            userBody.error = err;
            res.render('user/login', userBody);
        }
    },
    myRents: (req, res) => {
        Rent.find({ user: req.user._id })
            .populate('car')
            .then((rents) => {
                let cars = [];
                for (let rent of rents) {
                    let car = rent.car;
                    car.expiresOn = rent.days;
                    cars.push(rent.car);
                }
                res.render('user/rented', { cars });
            }).catch(console.error);
    }
};