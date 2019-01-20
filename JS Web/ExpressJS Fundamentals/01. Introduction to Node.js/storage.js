const fs = require('fs');
let storage = {};

function validateKey(key) {
    if (typeof key !== 'string') {
        throw new Error('Key must be string!');
    }
}

function keyExists(key) {
    return storage.hasOwnProperty(key);
}

function put(key, value) {
    validateKey(key);
    if (keyExists(key)) {
        throw new Error('Key already exists!');
    }
    storage[key] = value;
}

function get(key) {
    validateKey(key);
    if (!keyExists(key)) {
        throw new Error('Key does not exist!');
    }
    return storage[key];
}

function getAll() {
    if (Object.keys(storage).length !== 0) {
        return storage;
    }
    return "Storage is empty.";
}

function update(key, newValue) {
    validateKey(key);
    if (!keyExists(key)) {
        throw new Error('Key does not exist!');
    }
    storage[key] = newValue;
}

function deleteItem(key) {
    validateKey(key);
    if (!keyExists(key)) {
        throw new Error('Key does not exist!');
    }
    delete storage[key];
}

function clear() {
    storage = {};
}

function save() {
    return new Promise((resolve, reject) => {
        let data = JSON.stringify(storage);
        fs.writeFile('storage.json', data, (err) => {
            if (err) {
                reject(err);
                return;
            }
            resolve();
        });
    });
    
}

function load() {
    return new Promise((resolve, reject) => {
        fs.readFile('storage.json', (err, data) => {
            if (err) {
                reject(err);
                return;
            }
            storage = JSON.parse(data);
            resolve();
        });
    });
    
}

module.exports = {
    put,
    get,
    getAll,
    update,
    deleteItem,
    clear,
    save,
    load
}