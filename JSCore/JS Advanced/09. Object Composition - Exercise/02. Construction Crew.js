function constructionCrew(worker) {
    if (worker.handsShaking == true) {
        worker.handsShaking = false;
        worker.bloodAlcoholLevel += worker.weight * 0.1 * worker.experience;
    }
    return worker;
}

// let worker = {
//     weight: 80,
//     experience: 1,
//     bloodAlcoholLevel: 0,
//     handsShaking: true
// }
// console.log(constructionCrew(worker));