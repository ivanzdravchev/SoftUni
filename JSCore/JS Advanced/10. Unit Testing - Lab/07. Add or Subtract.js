let expect = require('chai').expect;

function createCalculator() {
    let value = 0;
    return {
        add: function (num) {
            value += Number(num);
        },
        subtract: function (num) {
            value -= Number(num);
        },
        get: function () {
            return value;
        }
    }
};

describe('Testing createCalculator() functionality', function() {
    let calculator;
    this.beforeEach(function() {
        calculator = createCalculator();
    });
    describe('test calculator validity', function() {
        it('is an object', function () {
            expect(typeof calculator).to.be.equal('object');
        });
        it('has a add() property', function () {
            let hasAdd = calculator.hasOwnProperty('add');
            expect(hasAdd).to.be.true;
        });
        it('has a subtract() property', function () {
            let hasSubtract = calculator.hasOwnProperty('subtract');
            expect(hasSubtract).to.be.true;
        });
        it('has a get() property', function () {
            let hasGet = calculator.hasOwnProperty('get');
            expect(hasGet).to.be.true;
        });
        it('sum is private and cant be changed', function () {
            expect(calculator.value).to.be.undefined;
        });
        it('returns NaN for adding strings', function() {
            calculator.add('Invalid');
            expect(calculator.get()).to.be.NaN;
        });
        it('returns NaN for subtracting strings', function () {
            calculator.subtract('string');
            expect(calculator.get()).to.be.NaN;
        });
        it('returns NaN for adding with no arguments', function() {
            calculator.add();
            expect(calculator.get()).to.be.NaN;
        });
    });
    
    describe('test proper functionality', function() {
        it('returns 0 after instant get', function() {
            expect(calculator.get()).to.be.equal(0);
        });
        it('returns 5 for adding 2 and 3', function() {
            calculator.add(2);
            calculator.add(3);
            expect(calculator.get()).to.be.equal(5);
        });
        it('returns -5 for subtracting 2 and 3', function() {
            calculator.subtract(2);
            calculator.subtract(3);
            expect(calculator.get()).to.be.equal(-5);
        });
        it('returns correct value for decimal operation', function() {
            calculator.add(5.3);
            calculator.subtract(1.1);
            expect(calculator.get()).to.be.equal(4.199999999999999);
        });
        it('returns correct value for long operations', function() {
            calculator.add(10);
            calculator.subtract(7);
            calculator.add(-2);
            calculator.subtract(-1);
            expect(calculator.get()).to.be.equal(2);
        });
        it('returns correct values if numbers are strings', function() {
            calculator.add('10');
            expect(calculator.get()).to.be.equal(10);
        });
        it('returns correct value if adding negative number', function() {
            calculator.add(-5);
            expect(calculator.get()).to.be.equal(-5);
        });
    });
});
