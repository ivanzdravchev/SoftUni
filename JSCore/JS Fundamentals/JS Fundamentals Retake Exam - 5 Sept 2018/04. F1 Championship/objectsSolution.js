function f1Championship(arr) {
    let book = {};
    for (let element of arr) {
        let tokens = element.split(' -> ');
        let team = tokens[0];
        let pilotName = tokens[1];
        let points = +tokens[2];
        if (!book.hasOwnProperty(team)) {
            book[team] = {
                totalPoints: 0,
                pilots: {}
            };
        }
        if (!book[team]['pilots'].hasOwnProperty(pilotName)) {
            book[team]['pilots'][pilotName] = 0;
        }
        book[team]['pilots'][pilotName] += points;
        book[team]['totalPoints'] += points;
    }
    let winners = Object.entries(book)
        .sort((a, b) => b[1]['totalPoints'] - a[1]['totalPoints'])
        .slice(0, 3);
    for (let winnerTeam of winners) {
        console.log(`${winnerTeam[0]}: ${winnerTeam[1]['totalPoints']}`);
        let sortedPilots = Object.entries(winnerTeam[1]['pilots']).sort((a, b) => b[1] - a[1]);
        for (let pilot of sortedPilots) {
            console.log(`-- ${pilot[0]} -> ${pilot[1]}`);
        }
    }
}

// let arr = [
//     "Ferrari -> Kimi Raikonnen -> 25",
//     "Ferrari -> Sebastian Vettel -> 18",
//     "Mercedes -> Lewis Hamilton -> 10",
//     "Mercedes -> Valteri Bottas -> 8",
//     "Red Bull -> Max Verstapen -> 6",
//     "Red Bull -> Daniel Ricciardo -> 4",
//     "Test -> Test Testing -> 55",
//     "Test -> More Testing -> 42"
// ];

// let arr2 = [
//     "Ferrari -> Kimi Raikonnen -> 25",
//     "Ferrari -> Kimi Raikonnen -> 27"
// ];

// f1Championship(arr);
