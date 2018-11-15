let Extensible = (function() {
    let id = 0;

    return class Extensible {
        constructor() {
            this.id = id++;
        }

        extend(templateObj) {
            for (let prop in templateObj) {
                if (typeof templateObj[prop] === 'function') {
                    Object.getPrototypeOf(this)[prop] = templateObj[prop];
                } else {
                    this[prop] = templateObj[prop];
                }
            }
        }
    }
})();

// let template = {
//     extensionMethod: function () {},
//     extensionProperty: 'someString'
// }

// let obj0 = new Extensible();
// let obj = new Extensible();
// obj.extend(template);
// console.log(obj);
// console.log(Object.getPrototypeOf(obj));