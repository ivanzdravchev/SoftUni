function playingCards(face, suit) {
    let faces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];
    let suits = ['S', 'H', 'D', 'C'];

    if (!faces.includes(face)) {
        throw new Error('Invalid card face');
    }
    if (!suits.includes(suit)) {
        throw new Error('Invalid card suit');
    }

    let suitsToChar = {
        'S': '\u2660',
        'H': '\u2665',
        'D': '\u2666',
        'C': '\u2663'
    }

    let card = {
        face,
        suit,
        toString: function () {
            return this.face + suitsToChar[suit];
        }
    }
    return card;
}

// console.log('' + playingCards('A', 'S'));
// console.log('' + playingCards('1', 'C'));