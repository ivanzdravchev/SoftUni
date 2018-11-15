class Rat {
    constructor(name) {
        this.name = name;
        this.rats = [];
    }

    unite(otherRat) {
        if (otherRat instanceof Rat) {
            this.rats.push(otherRat);
        }
    }

    getRats() {
        return this.rats;
    }

    toString() {
        let result = this.name;
        for (let rat of this.rats) {
            result += '\r\n##' + rat.name;
        }
        return result
    }
}

// let rat = new Rat('Rattler');
// rat.unite(new Rat('valid rat'));
// rat.unite('invalid rat');

// console.log(rat.toString());