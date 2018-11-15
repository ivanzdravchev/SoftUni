class SortedList {
    constructor() {
        this.size = 0;
        this.arr = [];
    }

    add(element) {
        this.arr.push(element);
        this.size++;
        this.sort();
    }

    remove(index) {
        if (index >= 0 && index < this.arr.length && this.size >= 1) {
            this.arr.splice(index, 1);
            this.size--;
            this.sort();
        }
    }

    get(index) {
        if (index >= 0 && index < this.arr.length) {
            return this.arr[index];
        }
    }

    sort() {
        this.arr.sort((a, b) => a - b);
    }
}

// let list = new SortedList();
// list.remove(0);
// list.add(1);
// list.add(5);
// list.add(3);
// console.log(list.get(0));
// console.log(list.size);
