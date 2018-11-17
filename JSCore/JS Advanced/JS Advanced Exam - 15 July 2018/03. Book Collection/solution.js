class BookCollection {
    constructor(shelfGenre, room, shelfCapacity) {
        this.shelfGenre = shelfGenre;
        this.room = room;
        this.shelfCapacity = shelfCapacity;
        this.shelf = [];
    }

    get room() {
        return this._room;
    }

    set room(room) {
        if (!['livingRoom', 'bedRoom', 'closet'].includes(room)) {
            throw new Error(`Cannot have book shelf in ${room}`);
        }
        this._room = room;
    }

    addBook(bookName, bookAuthor, genre) {
        let book = {
            bookName,
            bookAuthor
        }
        if (genre != undefined) {
            book.genre = genre;
        }
        if (this.shelfCondition > 0) {
            this.shelf.push(book);
        } else {
            this.shelf[0] = book;
        }
        this.shelf.sort((a, b) => a.bookAuthor > b.bookAuthor);

        return this;
    }

    throwAwayBook(bookName) {
        this.shelf = this.shelf.filter(n => n.bookName != bookName);
    }

    showBooks(genre) {
        let booksByGenre = this.shelf.filter(n => n.genre == genre);
        let result = `Results for search "${genre}":`;
        for (let book of booksByGenre) {
            result += `\n\uD83D\uDCD6 ${book.bookAuthor} - "${book.bookName}"`;
        }
        return result;
    }

    get shelfCondition() {
        return this.shelfCapacity - this.shelf.length;
    }

    toString() {
        if (this.shelf.length == 0) {
            return `It's an empty shelf`;
        }
        let result = `"${this.shelfGenre}" shelf in ${this.room} contains:`;
        for (let book of this.shelf) {
            result += `\n\uD83D\uDCD6 "${book.bookName}" - ${book.bookAuthor}`;
        }
        return result;
    }
}

// let bedRoom = new BookCollection('Mixed', 'bedRoom', 5);
// bedRoom.addBook("John Adams", "David McCullough", "history");
// bedRoom.addBook("The Guns of August", "Cuentos para pensar", "history");
// bedRoom.addBook("Atlas of Remote Islands", "Judith Schalansky");
// bedRoom.addBook("Paddle-to-the-Sea", "Holling Clancy Holling");
// console.log("Shelf's capacity: " + bedRoom.shelfCondition);
// console.log(bedRoom.toString());
