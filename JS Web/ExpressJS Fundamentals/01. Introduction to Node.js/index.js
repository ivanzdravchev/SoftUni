const storage = require('./storage');

console.log(storage.getAll());
storage.put('first', 'first value');
storage.put('second', 'second value');
storage.put('third', 'third value');
storage.put('fourth', 'fourth value');

storage.deleteItem('fourth');
storage.update('first', 'first value updated');
console.log(storage.getAll());

storage.save().then(() => {
    storage.clear();
    console.log(storage.getAll());
}).catch(err => console.log(err));

storage.load().then(() => {
    console.log(storage.getAll());
}).catch(err => console.log(err));