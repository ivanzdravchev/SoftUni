const Cube = require('../models/Cube');

function handleQueryErrors(name, from, to) {
    let errors = [];
    if (!name) {
        errors.push('Search word is required.');
    }
    if (from) {
        if (from < 1 || from > 6) {
            errors.push('From must be between 1 and 6');
        }
        if (!to) {
            errors.push('Fill both difficulty fields.');
        }
    }
    if (to) {
        if (to < 1 || to > 6) {
            errors.push('To must be between 1 and 6');
        }
        if (!from) {
            errors.push('Fill both difficulty fields.');
        }
    }
    if (from && to) {
        if (from > to) {
            errors.push('From must be less than to');
        }
    }
    return errors;
}

module.exports = {
    homeGet: (req, res) => {
        Cube.find({})
            .select('_id name imageUrl difficulty')
            .sort('-difficulty')
            .then((cubes) => {
                res.render('index', { cubes });
            }).catch(e => console.log(e));
    },
    about: (req, res) => {
        res.render('about');
    },
    search: async (req, res) => {
        let { name, from, to } = req.query;
        from = Number(from);
        to = Number(to);

        let errors = handleQueryErrors(name, from, to);
        if (errors.length > 0) {
            res.locals.globalErrors = errors;

            try {
                const cubes = await Cube.find().sort('-difficulty');
                res.render('index', { cubes });
                return;
            } catch(err) {
                console.log(err);
            }
        }

        if (name && from && to) {
            Cube.find({})
                .where('difficulty')
                .gte(from)
                .lte(to)
                .then((cubes) => {
                    const filtered = cubes
                        .filter(c => c.name.toLowerCase().includes(name.toLowerCase()));

                    res.render('index', { cubes: filtered });
                }).catch(err => console.log(err));
        }
        if (name && !from && !to) {
            console.log('im here');
            Cube.find({})
                .then((cubes) => {
                    const filtered = cubes
                        .filter(c => c.name.toLowerCase().includes(name.toLowerCase()));
                    console.log(filtered);
                    res.render('index', { cubes: filtered });
                }).catch(err => console.log(err));
        }
    }
}