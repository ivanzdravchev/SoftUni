let deck = (function (){
    let Suits = {
        SPADES: '\u2660',
        HEARTS: '\u2665',
        DIAMONDS: '\u2666',
        CLUBS: '\u2663'
    };
    let faces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];

    class Card {
        constructor(face, suit) {
            this.face = face;
            this.suit = suit;
        } 
        
        get face() {
            return this._face;
        }

        set face(face) {
            if (!faces.includes(face)) {
                throw new Error('Invalid card face');
            }
            this._face = face;
        }

        get suit() {
            return this._suit;
        }

        set suit(suit) {
            if (!Object.keys(Suits).map(x => Suits[x]).includes(suit)) {
                throw new Error('Invalid suit');
            }
            this._suit = suit;
        }

        toString() {
            return `${this.face}${this.suit}`;
        }
    }

    return {
        Suits,
        Card
    };
})();

// let Suits = deck.Suits;
// let Card = deck.Card;

// let card = new Card('A', Suits.SPADES);
// card.face = 'K';
// card.suit = Suits.HEARTS;

// console.log('' + card);