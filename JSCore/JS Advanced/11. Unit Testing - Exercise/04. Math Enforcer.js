let assert = require('chai').assert;
let expect = require('chai').expect;

let mathEnforcer = {
    addFive: function (num) {
        if (typeof (num) !== 'number') {
            return undefined;
        }
        return num + 5;
    },
    subtractTen: function (num) {
        if (typeof (num) !== 'number') {
            return undefined;
        }
        return num - 10;
    },
    sum: function (num1, num2) {
        if (typeof (num1) !== 'number' || typeof (num2) !== 'number') {
            return undefined;
        }
        return num1 + num2;
    }
};

describe('Tests for mathEnforcer object functionality', function() {
    describe('Checking for missing object methods', function() {
        it('all functions should exist', function() {
            assert(typeof mathEnforcer.addFive == 'function', 'addFive function is missing');
            assert(typeof mathEnforcer.subtractTen == 'function', 'subtractTen function is missing');
            assert(typeof mathEnforcer.sum == 'function', 'sum function is missing');
        });
    });

    describe('Checking Invalid inputs', function() {
        it('addFive returns undefined for given string', function() {
            assert.isUndefined(mathEnforcer.addFive('random'));
        });
        it('addFive returns undefined for given object', function () {
            assert.isUndefined(mathEnforcer.addFive({num: 5}));
        });
        it('addFive returns undefined for given array with 1 number', function () {
            assert.isUndefined(mathEnforcer.addFive([5]));
        });

        it('subtractTen returns undefined for given string', function() {
            assert.isUndefined(mathEnforcer.subtractTen('sample'));
        });
        it('subtractTen returns undefined for given object', function () {
            assert.isUndefined(mathEnforcer.subtractTen({num: 12}));
        });
        it('subtractTen returns undefined for given array with 1 number', function () {
            assert.isUndefined(mathEnforcer.subtractTen([12]));
        });

        it('sum returns undefined if first parameter is invalid', function() {
            assert.isUndefined(mathEnforcer.sum('string', 5));
            assert.isUndefined(mathEnforcer.sum([5], 10));
            assert.isUndefined(mathEnforcer.sum({num: 7}, 7));
        });
        it('sum returns undefined if second parameter is invalid', function() {
            assert.isUndefined(mathEnforcer.sum(5, 'string'));
            assert.isUndefined(mathEnforcer.sum(10, [5]));
            assert.isUndefined(mathEnforcer.sum(7, {num: 7}));
        });
    });

    describe('Checking valid inputs', function() {
        it('addFive returns correct number', function() {
            assert(mathEnforcer.addFive(5) == 10, 'addFive integers error');
            assert(mathEnforcer.addFive(-11) == -6, 'addFive negative integers error');
            assert(mathEnforcer.addFive(3.14) == 8.14, 'addFive decimals error');
        });
        it('subtractTen returns correct number', function() {
            assert(mathEnforcer.subtractTen(12) == 2, 'subtractTen integers error');
            assert(mathEnforcer.subtractTen(-4) == -14, 'subtractTen negative integers error');
            assert(mathEnforcer.subtractTen(17.77) == 7.77, 'subtractTen decimals error');
        });
        it('sum returns correct number', function() {
            assert(mathEnforcer.sum(5, 17) == 22, 'sum integers error');
            assert(mathEnforcer.sum(-15, -9) == -24, 'sum negative integers error');
            assert(mathEnforcer.sum(12.34, 43.21) == 55.55, 'sum decimals error');
        });
    });
});