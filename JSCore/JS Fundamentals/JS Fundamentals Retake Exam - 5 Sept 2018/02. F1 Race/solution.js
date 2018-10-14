function race(arr) {
    let drivers = arr[0].split(' ').filter(x => x != '');
    for(let i = 1; i < arr.length; i++) {
        let [action, driver] = arr[i].split(' ').filter(x => x != '');
        let index = drivers.indexOf(driver);
        if (index == -1) { //driver missing
            if (action.toLowerCase() == 'join') {
                drivers.push(driver);
            }
        } else { // driver exists
            if (action.toLowerCase() == 'crash') {
                drivers = drivers.filter(x => x != driver);
            } else if (action.toLowerCase() == 'pit') {
                if (drivers.indexOf(driver) != drivers.length - 1) {
                    let temp = drivers[index];
                    drivers[index] = drivers[index + 1];
                    drivers[index + 1] = temp;
                }
            } else if (action.toLowerCase() == 'overtake') {
                if (drivers.indexOf(driver) != 0) {
                    let temp = drivers[index - 1];
                    drivers[index - 1] = drivers[index];
                    drivers[index] = temp;
                }
            }
        }
    }
    return drivers.join(' ~ ');
}

// let arr = ["Vetel Hamilton Slavi",
//     "Pit Hamilton",
//     "Overtake Vetel",
//     "Crash Slavi"
// ];

// let arr2 = ["Vetel Hamilton Raikonnen Botas Slavi",
//     "Pit Hamilton",
//     "Overtake LeClerc",
//     "Join Ricardo",
//     "Crash Botas",
//     "Overtake Ricardo",
//     "Overtake Ricardo",
//     "Overtake Ricardo",
//     "Crash Slavi"
// ];

// let myArr = [
//     "Vetel Hamilton Slavi",
//     "jOiN Hamilton",
//     "jOIN Player1",
//     "crAsh noone",
//     "cRasH Slavi",
//     "piT Player1",
//     "PIT Hamilton",
//     "OVERTAke Player1"
// ];
// console.log(race(myArr));