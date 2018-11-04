function cars(commands) {
    let map = new Map();
    const process = (function() {
        const service = {
            create: function(name, inherits, parent) {
                parent = parent ? map.get(parent) : null;
                let newObj = Object.create(parent);
                map.set(name, newObj);
                return newObj;
            },
            set: function(name, key, value) {
                map.get(name)[key] = value;
            },
            print: function(name) {
                let obj = map.get(name);
                let objects = [];
                for (let key in obj) {
                    objects.push(`${key}:${obj[key]}`);
                }
                console.log(objects.join(', '));
            }
        }
        return service;
    })();

    for (let command of commands) {
        let tokens = command.split(' ');
        let action = tokens.shift();
        process[action](...tokens);
    }
}

let arr = ['create c1',
    'create c2 inherit c1',
    'set c1 color red',
    'set c2 model new',
    'print c1',
    'print c2'
];
cars(arr);