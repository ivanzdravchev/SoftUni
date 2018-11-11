let assert = require('chai').assert;
let jsdom = require('jsdom-global')();
let $ = require('jquery');


let sharedObject = {
    name: null,
    income: null,
    changeName: function (name) {
        if (name.length === 0) {
            return;
        }
        this.name = name;
        let newName = $('#name');
        newName.val(this.name);
    },
    changeIncome: function (income) {
        if (!Number.isInteger(income) || income <= 0) {
            return;
        }
        this.income = income;
        let newIncome = $('#income');
        newIncome.val(this.income);
    },
    updateName: function () {
        let newName = $('#name').val();
        if (newName.length === 0) {
            return;
        }
        this.name = newName;
    },
    updateIncome: function () {
        let newIncome = $('#income').val();
        if (isNaN(newIncome) || !Number.isInteger(Number(newIncome)) || Number(newIncome) <= 0) {
            return;
        }
        this.income = Number(newIncome);
    }
};

describe('Testing sharedObject functionality', function() {
    this.beforeEach('reset HTML', function() {
        document.body.innerHTML = `
        <div id="wrapper">
            <input type="text" id="name">
            <input type="text" id="income">
        </div>`;
    });

    describe('all fields and methods exist', function() {
        it('sharedObject has name property which is null initially', function() {
            assert(sharedObject.hasOwnProperty('name') == true, 'name property missing');
            assert(sharedObject.name == null, 'prop name is not null initially');
        });
        it('sharedObject has income property which is null initially', function() {
            assert(sharedObject.hasOwnProperty('income') == true, 'income property missing');
            assert(sharedObject.income == null, 'prop income is not null initially');
        });

        it('all functions exist', function() {
            assert(typeof sharedObject.changeName == 'function', 'changeName func missing');
            assert(typeof sharedObject.changeIncome == 'function', 'changeIncome func missing');
            assert(typeof sharedObject.updateName == 'function', 'updateName func missing');
            assert(typeof sharedObject.updateIncome == 'function', 'updateIncome func missing');
        });
    });

    describe('Invalid Test Cases', function() {
        it('changeName works correctly with given empty string', function() {
            sharedObject.changeName('');
            assert.isNull(sharedObject.name);
        });

        it('changeIncome works correctly with given string', function() {
            sharedObject.changeIncome('str');
            assert.isNull(sharedObject.income);
        });
        it('changeIncome works correctly with given negative integer', function() {
            sharedObject.changeIncome(-100);
            assert.isNull(sharedObject.income);
        });
        it('changeIncome works correctly with given positive decimal number', function() {
            sharedObject.changeIncome(123.45);
            assert.isNull(sharedObject.income);
        });
        it('changeIncome works correctly with given 0', function () {
            sharedObject.changeIncome(0);
            assert.isNull(sharedObject.income);
        });

        it('updateName works correctly with given empty string', function() {
            $('#name').val('');
            sharedObject.updateName();
            assert.isNull(sharedObject.name);
        });

        it('updateIncome works correctly with given string', function() {
            $('#income').val('asd');
            sharedObject.updateIncome();
            assert.isNull(sharedObject.income);
        });
        it('updateIncome works correctly with given negative integer', function () {
            $('#income').val(-15);
            sharedObject.updateIncome();
            assert.isNull(sharedObject.income);
        });
        it('updateIncome works correctly with given 0', function () {
            $('#income').val(0);
            sharedObject.updateIncome();
            assert.isNull(sharedObject.income);
        });
        it('updateIncome works correctly with given decimal number', function () {
            $('#income').val(333.33);
            sharedObject.updateIncome();
            assert.isNull(sharedObject.income);
        });
    });

    describe('Valid Test Cases', function() {
        it('changeName works correctly with valid parameter', function() {
            sharedObject.changeName('SomeName');
            assert(sharedObject.name == 'SomeName', 'Name wasn\t changed');
        });
        it('changeName changes the textbox value', function() {
            sharedObject.changeName('change');
            let value = $('#name').val();
            assert(value == 'change', 'changeName doesnt edit textbox');
        });

        it('changeIncome works correctly with valid parameter', function() {
            sharedObject.changeIncome(500);
            assert(sharedObject.income == 500, 'Income wasn\'t changed');
        });
        it('changeIncome changes the textbox value', function() {
            sharedObject.changeIncome(444);
            let value = $('#income').val();
            assert(value == 444, 'changeIncome doesnt edit textbox');
        });

        it('updateName works correctly', function() {
            $('#name').val('some name');
            sharedObject.updateName();
            assert(sharedObject.name == 'some name', 'updateName doesn\'t work');
        });

        it('updateIncome works correctly', function() {
            $('#income').val(150);
            sharedObject.updateIncome();
            assert(sharedObject.income == 150, 'updateIncome doesn\'t work');
        });
    });
});