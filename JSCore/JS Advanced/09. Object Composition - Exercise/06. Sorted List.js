let collection = (function sortedList() {
    let localCollection = [];
    const service = {
        add: function (num) {
            localCollection.push(num);
            service.sort();
            this.size++;
        },
        remove: function (index) {
            if (index >= 0 && index < localCollection.length) {
                localCollection.splice(index, 1);
                service.sort();
                this.size--;
            }
        },
        get: function (index) {
            if (index >= 0 && index <= localCollection.length) {
                return localCollection[index];
            }
        },
        size: 0,
        sort: function () {
            localCollection = localCollection.sort((a, b) => a - b);
        }
    };
    return service;
})();

// collection.add(7);
// console.log("Size: " + collection.size);
// console.log('El[0] = ' + collection.get(0));
// collection.remove(0);
// console.log(collection.get(0));