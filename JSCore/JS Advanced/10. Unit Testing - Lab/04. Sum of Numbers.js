let expect = require('chai').expect;

function sum(arr) {
    let sum = 0;
    for (let num of arr) {
        sum += +num;
    }
    return sum;
}

describe('Testing if sum(arr) function works correctly', function() {
    it('should return 3 for [2, 1]', function() {
        expect(sum([2, 1])).to.be.equal(3);
    });
    it('should return 1 for [1]', function() {
        expect(sum([1])).to.be.equal(1);
    });
    it('should return 0 for []', function() {
        expect(sum([])).to.be.equal(0);
    });
    it('should return 3 for [1.5, 2.5, -1]', function() {
        expect(sum([1.5, 2.5, -1])).to.be.equal(3);
    });
    it('should return NaN for "Invalid Data"', function() {
        expect(sum(['Invalid Data'])).to.be.NaN;
    });
});