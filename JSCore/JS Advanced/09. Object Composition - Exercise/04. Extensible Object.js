let extensibleObject = (function() {
    let myObj = Object.create({});

    myObj.extend = function(template) {
        for (let prop in template) {
            if (typeof template[prop] === 'function') {
                Object.getPrototypeOf(myObj)[prop] = template[prop];
            } else {
                myObj[prop] = template[prop];
            }
        }
    }

    return myObj;
})();

// let template = {
//     extensionMethod: function () {},
//     extensionProperty: 'someString'
// }

// extensibleObject.extend(template);
// console.log(extensibleObject);
// console.log(Object.getPrototypeOf(extensibleObject));