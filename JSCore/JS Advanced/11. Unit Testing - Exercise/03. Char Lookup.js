let assert = require('chai').assert;

function lookupChar(string, index) {
    if (typeof (string) !== 'string' || !Number.isInteger(index)) {
        return undefined;
    }
    if (string.length <= index || index < 0) {
        return "Incorrect index";
    }

    return string.charAt(index);
}

describe('Testing lookupChar(string, index) functionality', function() {
    describe('Invalid cases', function() {
        it('returns undefined if first parameter is number', function() {
            assert(lookupChar(13, 5) == undefined, 'return value mismatch');
        });
        it('returns undefined if first parameter is array', function () {
            assert(lookupChar([], 5) == undefined, 'return value mismatch');
        });
        it('returns undefined if first parameter is object', function () {
            assert(lookupChar({str: 'example'}, 5) == undefined, 'return value mismatch');
        });

        it('returns undefined if second parameter is string', function() {
            assert(lookupChar('example', 'another string') == undefined, 'return value mismatch');
        });
        it('returns undefined if second parameter is not integer', function () {
            assert(lookupChar('example', 3.14) == undefined, 'return value mismatch');
        });
        it('returns undefined if second parameter is array', function () {
            assert(lookupChar('example', ['str']) == undefined, 'return value mismatch');
        });
        it('returns undefined if second parameter is object', function () {
            assert(lookupChar('example', {name: 'something'}) == undefined, 'return value mismatch');
        });

        it('returns error message if index too low', function() {
            assert.equal(lookupChar('string', -5), 'Incorrect index');
        });
        it('returns error message if index too high', function () {
            assert.equal(lookupChar('string', 6), 'Incorrect index');
        });
    });

    describe('Valid cases', function() {
        it('returns correct value at 0 index', function() {
            assert.equal(lookupChar('sample', 0), 's');
        });
        it('returns correct value at last index', function() {
            assert.equal(lookupChar('sample', 5), 'e');
        });
    });
});