let assert = require('chai').assert;

function isOddOrEven(string) {
    if (typeof (string) !== 'string') {
        return undefined;
    }
    if (string.length % 2 === 0) {
        return "even";
    }

    return "odd";
}

describe('Tests for isOddOrEven(string) functionality', function() {
    describe('Invalid inputs', function() {
        it('returns undefined if given a number', function() {
            assert.equal(isOddOrEven(3), undefined);
        });
        it('returns undefind if given object', function() {
            assert(isOddOrEven({name: 'Ivan'}) == undefined, 'value mismatch');
        });
        it('returns undefined if given array of strings', function() {
            assert.isUndefined(isOddOrEven(['str1', 'str2', 'str3']));
        });
    });

    describe('Valid inputs', function() {
        it('returns odd for odd length word', function() {
            assert.equal(isOddOrEven('example'), 'odd');
        });
        it('returns even for even length word', function() {
            assert.equal(isOddOrEven('sample'), 'even');
        });
    });
});