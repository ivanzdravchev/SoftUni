class Calculator {
    constructor() {
        this.expenses = [];
    }

    add(data) {
        this.expenses.push(data);
    }

    divideNums() {
        let divide;
        for (let i = 0; i < this.expenses.length; i++) {
            if (typeof (this.expenses[i]) === 'number') {
                if (i === 0 || divide === undefined) {
                    divide = this.expenses[i];
                } else {
                    if (this.expenses[i] === 0) {
                        return 'Cannot divide by zero';
                    }
                    divide /= this.expenses[i];
                }
            }
        }
        if (divide !== undefined) {
            this.expenses = [divide];
            return divide;
        } else {
            throw new Error('There are no numbers in the array!')
        }
    }

    toString() {
        if (this.expenses.length > 0)
            return this.expenses.join(" -> ");
        else return 'empty array';
    }

    orderBy() {
        if (this.expenses.length > 0) {
            let isNumber = true;
            for (let data of this.expenses) {
                if (typeof data !== 'number')
                    isNumber = false;
            }
            if (isNumber) {
                return this.expenses.sort((a, b) => a - b).join(', ');
            } else {
                return this.expenses.sort().join(', ');
            }
        } else return 'empty';
    }
}

let assert = require('chai').assert;

describe('Tests for Calculator class functionality', function() {
    let calculator;
    this.beforeEach('create calculator', function() {
        calculator = new Calculator();
    });
    describe('Testing INVALID Cases and initial state', function() {
        it('array exists and is empty upon creation', function() {
            assert(calculator.expenses instanceof Array, 'array missing');
            assert(calculator.expenses.length == 0, 'array not empty');
        });

        it('divideNums throws error if no numbers present', function() {
            calculator.add('55');
            calculator.add({name: 'someone', age: 18});
            calculator.add([1, -2, 3.14]);
            assert.throws(() => calculator.divideNums());
        });
        it('returns if cannot divide by 0', function () {
            calculator.add(5);
            calculator.add(0);
            assert(calculator.divideNums() == 'Cannot divide by zero');
        });

        it('toString returns correct message if array empty', function() {
            assert(calculator.toString() == 'empty array');
        });
        it('orderBy returns correct message if array empty', function() {
            assert(calculator.orderBy() == 'empty');
        });
    });

    describe('Testing VALID Cases', function() {
        it('divideNums returns correct number', function() {
            calculator.add(9);
            calculator.add('pointless string');
            calculator.add(4);
            assert.approximately(calculator.divideNums(), 2.25, 0.01);
        });

        it('toString displays correct string', function() {
            calculator.add(5);
            calculator.add('str');
            calculator.add(3.14);
            assert(calculator.toString() == '5 -> str -> 3.14');
        });

        it('orderBy works properly - integers only case', function() {
            calculator.add(37);
            calculator.add(155);
            calculator.add(9);
            calculator.add(-71)
            assert(calculator.orderBy() == '-71, 9, 37, 155');
        });
        it('orderBy works properly - integers and strings case', function() {
            calculator.add(115);
            calculator.add('str');
            calculator.add(75);
            calculator.add('thanos car');
            assert(calculator.orderBy() == '115, 75, str, thanos car');
        });
    });
});