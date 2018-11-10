let expect = require('chai').expect;

function rgbToHexColor(red, green, blue) {
    if (!Number.isInteger(red) || (red < 0) || (red > 255))
        return undefined; // Red value is invalid
    if (!Number.isInteger(green) || (green < 0) || (green > 255))
        return undefined; // Green value is invalid
    if (!Number.isInteger(blue) || (blue < 0) || (blue > 255))
        return undefined; // Blue value is invalid
    return "#" +
        ("0" + red.toString(16).toUpperCase()).slice(-2) +
        ("0" + green.toString(16).toUpperCase()).slice(-2) +
        ("0" + blue.toString(16).toUpperCase()).slice(-2);
}

describe('Testing rgbToHex functionality', function() {
    describe('Invalid Input Cases', function() {
        it('First parameter Invalid returns undefined', function() {
            expect(rgbToHexColor('Invalid', 100, 200)).to.be.undefined;
        });
        it('Second parameter Invalid returns undefined', function () {
            expect(rgbToHexColor(50, 'Invalid', 200)).to.be.undefined;
        });
        it('Third parameter Invalid returns undefined', function () {
            expect(rgbToHexColor(50, 100, 'Invalid')).to.be.undefined;
        });

        it('Returns undefined for wrong First input number', function() {
            expect(rgbToHexColor(256, 100, 200)).to.be.undefined;
        });
        it('Returns undefined for wrong Second input number', function () {
            expect(rgbToHexColor(50, 256, 200)).to.be.undefined;
        });
        it('Returns undefined for wrong Third input number', function () {
            expect(rgbToHexColor(50, 100, 256)).to.be.undefined;
        });
        it('Returns undefined for wrong Third input number', function () {
            expect(rgbToHexColor(50, -100, -1)).to.be.undefined;
        });

        it('returns undefined for missing argument', function() {
            expect(rgbToHexColor(5, 10)).to.be.undefined;
        });
        it('returns undefined for weird argument', function () {
            expect(rgbToHexColor('5', [3], {8:9})).to.be.undefined;
        });
        it('returns undefined for missing arguments', function () {
            expect(rgbToHexColor(5)).to.be.undefined;
        });
        it('returns undefined for empty array', function () {
            expect(rgbToHexColor([])).to.be.undefined;
        });
        it('returns undefined for string', function () {
            expect(rgbToHexColor('Invalid')).to.be.undefined;
        });
    });

    describe('Valid Input Cases', function() {
        it('returns of type string', function () {
            expect(rgbToHexColor(255, 158, 170)).to.be.string;
        });
        it('returns valid hex color for (255, 158, 170)', function() {
            expect(rgbToHexColor(255, 158, 170)).to.be.equal('#FF9EAA');
        });
        it('returns valid hex color for (12, 13, 14)', function() {
            expect(rgbToHexColor(12, 13, 14)).to.be.equal('#0C0D0E');
        });
        it('returns valid hex color for (0, 0, 0)', function () {
            expect(rgbToHexColor(0, 0, 0)).to.be.equal('#000000');
        });
        it('returns valid hex color for (255, 255, 255)', function () {
            expect(rgbToHexColor(255, 255, 255)).to.be.equal('#FFFFFF');
        });
    });
});