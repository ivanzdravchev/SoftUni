class Vacationer {
    constructor(fullName, creditCard) {
        this.fullName = fullName;
        this.idNumber = this.generateIDNumber();
        this.creditCard = {
            cardNumber: 1111,
            expirationDate: '',
            securityNumber: 111
        }
        if (creditCard != undefined) {
            this.addCreditCardInfo(creditCard);
        }
        this.wishList = [];
    }

    get fullName() {
        return this._fullName;
    }

    set fullName(name) {
        if (name.length != 3) {
            throw new Error('Name must include first name, middle name and last name');
        }

        name.forEach(n => {
            if (!/^[A-Z][a-z]+$/.test(n)) {
                throw new Error('Invalid full name');
            }
        });

        this._fullName = {
            firstName: name[0],
            middleName: name[1],
            lastName: name[2]
        };
    }

    generateIDNumber() {
        let vowels = ['a', 'e', 'o', 'i', 'u'];
        let firstLetterAscii = this.fullName.firstName.charCodeAt(0);
        let middleNameLength = this.fullName.middleName.length;
        let bonusNum = 7;
        if (vowels.includes(this.fullName.lastName[this.fullName.lastName.length - 1])) {
            bonusNum = 8;
        }
        return (231 * firstLetterAscii + 139 * middleNameLength).toString() + bonusNum;
    }

    addCreditCardInfo(info) {
        if (info.length != 3) {
            throw new Error('Missing credit card information');
        }
        if (typeof info[0] != 'number' || typeof info[2] != 'number') {
            throw new Error('Invalid credit card details');
        }
        this.creditCard = {
            cardNumber: info[0],
            expirationDate: info[1],
            securityNumber: info[2]
        }
    }

    addDestinationToWishList(destination) {
        if (this.wishList.includes(destination)) {
            throw new Error('Destination already exists in wishlist');
        }
        this.wishList.push(destination);
        this.wishList.sort((a, b) => a.length - b.length);
    }

    getVacationerInfo() {
        let result = `Name: ${this.fullName.firstName} ${this.fullName.middleName} ${this.fullName.lastName}\n`;
        result += `ID Number: ${this.idNumber}\n`;
        result += `Wishlist:\n`;
        result += `${this.wishList.length == 0 ? 'empty' : this.wishList.join(', ')}\n`;
        result += `Credit Card:\n`;
        result += `Card Number: ${this.creditCard.cardNumber}\n`;
        result += `Expiration Date: ${this.creditCard.expirationDate}\n`;
        result += `Security Number: ${this.creditCard.securityNumber}`;

        return result;
    }
}

// let vacationer1 = new Vacationer(["Vania", "Ivanova", "Zhivkova"]);
// vacationer1.addDestinationToWishList('Spain');
// vacationer1.addDestinationToWishList('Germany');
// vacationer1.addDestinationToWishList('Bali');
// let vacationer2 = new Vacationer(["Tania", "Ivanova", "Zhivkova"], [123456789, "10/01/2018", 777]);

// console.log(vacationer1.getVacationerInfo());
// console.log(vacationer2.getVacationerInfo());