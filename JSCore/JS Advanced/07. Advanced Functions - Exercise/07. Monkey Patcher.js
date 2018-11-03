function monkeyPatcher(command) {
    switch (command) {
        case 'upvote':
            this.upvotes += 1;
            break;
        case 'downvote':
            this.downvotes += 1;
            break;
        case 'score':
            return score(this);
    }

    function score(obj) {
        let upvotes = obj.upvotes;
        let downvotes = obj.downvotes;
        let score = upvotes - downvotes;
        let rating = '';
        let bonusVotes = 0;

        if (upvotes + downvotes > 50) {
            bonusVotes = Math.ceil(Math.max(upvotes, downvotes) * 0.25);
        }

        if (upvotes + downvotes < 10) {
            rating = 'new';
        } else if (upvotes > (upvotes + downvotes) * 0.66) {
            rating = 'hot';
        } else if (upvotes >= downvotes && (upvotes > 100 || downvotes > 100)) {
            rating = 'controversial';
        } else if (upvotes < downvotes) {
            rating = 'unpopular';
        } else {
            rating = 'new';
        }
        return [upvotes + bonusVotes, downvotes + bonusVotes, score, rating];
    }
}

// let obj = {
//     id: '3',
//     author: 'emil',
//     content: 'hi',
//     upvotes: 1,
//     downvotes: 0
// };

// monkeyPatcher.call(obj, 'upvote');
// monkeyPatcher.call(obj, 'downvote');
// console.log(monkeyPatcher.call(obj, 'score'));