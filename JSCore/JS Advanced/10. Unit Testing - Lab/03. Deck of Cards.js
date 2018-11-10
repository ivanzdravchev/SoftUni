function deckOfCards(deck) {
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

    let arr = [];
    for (let card of deck) {
        let face = card.substr(0, card.length - 1);
        let suit = card.substr(card.length - 1, 1);
        try {
            arr.push(playingCards(face, suit));
        } catch (err) {
            console.log('Invalid card: ' + card);
            return;
        }
    }
    console.log(arr.join(' '));
}

// deckOfCards(['AS', '10D', 'KH', '2C']);
// deckOfCards(['5S', '3D', 'QD', '1C']);