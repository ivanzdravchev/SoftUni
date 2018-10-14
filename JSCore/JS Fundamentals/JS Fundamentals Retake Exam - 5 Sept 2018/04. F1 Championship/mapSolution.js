function f1Championship(arr) {
    let map = new Map();
    for(let i = 0; i < arr.length; i++) {
        let [team, pilot, points] = arr[i].split(' -> ');
        if (!map.has(team)) {
            map.set(team, new Map());
        }
        if (!map.get(team).has(pilot)) {
            map.get(team).set(pilot, +points);
        } else {
            map.get(team).set(pilot, map.get(team).get(pilot) + +points);
        }
    }

    let sortedMap = [...map].sort((a, b) => [...b[1].values()]
        .reduce((a, b) => a + b) - [...a[1].values()]
        .reduce((a, b) => a + b))
        .slice(0, 3);
    //console.log(sortedMap);
    for (let [team, pilot] of sortedMap) {
        console.log(`${team}: ${[...pilot.values()].reduce((a, b) => a + b)}`);
        let sortedPilots = [...pilot].sort((a, b) => b[1] - a[1]);
        for (let [pilot, points] of sortedPilots) {
            console.log(`-- ${pilot} -> ${points}`);
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
