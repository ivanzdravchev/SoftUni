let expect = require('chai').expect;

function isSymmetric(arr) {
    if (!Array.isArray(arr))
        return false; // Non-arrays are non-symmetric
    let reversed = arr.slice(0).reverse(); // Clone and reverse
    let equal = (JSON.stringify(arr) == JSON.stringify(reversed));
    return equal;
}

describe('Testing isSymmetric(arr) functionality', function() {
    it('returns false if not Array', function() {
        expect(isSymmetric({pesho: 'gosho'})).to.be.false;
    });
    it('returns false if string', function() {
        expect(isSymmetric('Invalid')).to.be.false;
    });
    it('returns false if not symmetric ([1, 2, 3, 4 ,2, 1])', function () {
        expect(isSymmetric([1, 2, 3, 4, 2, 1])).to.be.false;
    });
    it('returns true if symmetric ([[1, 2, 3, 2 ,1])', function() {
        expect(isSymmetric([1, 2, 3, 2 ,1])).to.be.true;
    })
    it('returns true if it is symmetric ([-1, 2, -1])', function() {
        expect(isSymmetric([-1, 2, -1])).to.be.true;
    });
    it('returns false if not array', function() {
        expect(isSymmetric(1, 2, 2, 1)).to.be.false;
    });
    it('returns false if not equal', function() {
        expect(isSymmetric(1, 2, -2, -1)).to.be.false;
    });
    it('returns true for empty array', function() {
        expect(isSymmetric([])).to.be.true;
    });
    it('returns true for only one element in array', function() {
        expect(isSymmetric([1])).to.be.true;
    });
    it('returns true', function() {
        expect(isSymmetric([5, 'hi', {a:5}, new Date(), {a:5}, 'hi', 5])).to.be.true;
    });
    it('returns false', function() {
        expect(isSymmetric([5, 'hi', {a:5}, new Date(), {x:5}, 'hi', 5])).to.be.false;
    });
});