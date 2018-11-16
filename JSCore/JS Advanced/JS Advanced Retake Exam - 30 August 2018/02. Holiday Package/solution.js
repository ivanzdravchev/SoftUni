let assert = require('chai').assert;

class HolidayPackage {
    constructor(destination, season) {
        this.vacationers = [];
        this.destination = destination;
        this.season = season;
        this.insuranceIncluded = false; // Default value
    }

    showVacationers() {
        if (this.vacationers.length > 0)
            return "Vacationers:\n" + this.vacationers.join("\n");
        else
            return "No vacationers are added yet";
    }

    addVacationer(vacationerName) {
        if (typeof vacationerName !== "string" || vacationerName === ' ') {
            throw new Error("Vacationer name must be a non-empty string");
        }
        if (vacationerName.split(" ").length !== 2) {
            throw new Error("Name must consist of first name and last name");
        }
        this.vacationers.push(vacationerName);
    }

    get insuranceIncluded() {
        return this._insuranceIncluded;
    }

    set insuranceIncluded(insurance) {
        if (typeof insurance !== 'boolean') {
            throw new Error("Insurance status must be a boolean");
        }
        this._insuranceIncluded = insurance;
    }

    generateHolidayPackage() {
        if (this.vacationers.length < 1) {
            throw new Error("There must be at least 1 vacationer added");
        }
        let totalPrice = this.vacationers.length * 400;

        if (this.season === "Summer" || this.season === "Winter") {
            totalPrice += 200;
        }

        totalPrice += this.insuranceIncluded === true ? 100 : 0;

        return "Holiday Package Generated\n" +
            "Destination: " + this.destination + "\n" +
            this.showVacationers() + "\n" +
            "Price: " + totalPrice;
    }
}



describe('HolidayPackage.js functionality tests', function () {
    let instance;
    beforeEach('create class instance', function () {
        instance = new HolidayPackage('Italy', 'Summer');
    });
    describe('Testing INVALID test cases', function () {
        it('insuranceIncluded must have default value - false', function() {
            assert.equal(instance.insuranceIncluded, false);
        });
        it('show vacationers with empty array', function () {
            assert(instance.showVacationers() == 'No vacationers are added yet');
        });

        it('add vacationer throws if not given a string', function() {
            assert.throws(() => instance.addVacationer(5));
            //expect(() => instance.addVacationer(5)).to.throw();
            assert.throws(() => instance.addVacationer(['str1', 'str2']));
        });
        it('add vacationer throws if given empty space', function () {
            assert.throws(() => instance.addVacationer(' '));
        });
        it('add vacationer throws if more than two words', function () {
            assert.throws(() => instance.addVacationer('String is too long'));
        });
        it('add vacationer throws if less than two words', function () {
            assert.throws(() => instance.addVacationer('short'));
        });

        it('Set insuranceIncluded throws if given string', function () {
            assert.throws(() => instance.insuranceIncluded('str'));
        });
        it('Set insuranceIncluded throws if given true as string', function () {
            assert.throws(() => instance.insuranceIncluded('true'));
        });
        it('Set insuranceIncluded throws if given false as string', function () {
            assert.throws(() => instance.insuranceIncluded('false'));
        });

        it('generateHolidayPackage() throws if no vacationers', function() {
            assert.throws(() => instance.generateHolidayPackage());
        });
    });

    describe('Testing VALID test cases', function() {
        it('showVacationers() returns all people correctly', function () {
            instance.addVacationer('Pesho Peshovski');
            instance.addVacationer('Gosho Goshov');
            assert.equal(instance.showVacationers(), 'Vacationers:\nPesho Peshovski\nGosho Goshov');
        });

        it('setInsurance works correctly', function() {
            instance.insuranceIncluded = true;
            assert.equal(instance.insuranceIncluded, true);
        });

        it('generateHolidayPackage works correctly (with insurance)', function() {
            instance.addVacationer('Pesho Peshovski');
            instance.addVacationer('Gosho Goshov');
            instance.insuranceIncluded = true;
            let result = `Holiday Package Generated\nDestination: Italy\nVacationers:\nPesho Peshovski\nGosho Goshov\nPrice: 1100`;
            assert.equal(instance.generateHolidayPackage(), result);
        });
        it('generateHolidayPackage works correctly (without insurance)', function () {
            instance.addVacationer('Pesho Peshovski');
            instance.addVacationer('Gosho Goshov');
            instance.insuranceIncluded = false;
            let result = `Holiday Package Generated\nDestination: Italy\nVacationers:\nPesho Peshovski\nGosho Goshov\nPrice: 1000`;
            assert.equal(instance.generateHolidayPackage(), result);
        });
    });
});