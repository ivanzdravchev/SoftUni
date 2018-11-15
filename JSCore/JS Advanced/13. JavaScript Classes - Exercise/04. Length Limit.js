class Stringer {
    constructor(string, length) {
        this.string = string;
        this.length = length;
    }

    get innerString() {
        return this.string;
    }

    get innerLength() {
        return this.length;
    }

    increase(length) {
        this.length += length;
    }

    decrease(length) {
        this.length < length ? this.length = 0 : this.length -= length;
    }

    toString() {
        return this.string.length <= this.length ? 
            this.string.substr(0, this.length) :
            this.string.substr(0, this.length) + '...';
    }
}

// let stringer = new Stringer('test', 5);
// stringer.decrease(3);
// console.log(stringer.toString());