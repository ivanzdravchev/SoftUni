function roadRadar(arr) {
    let speed = arr[0];
    let area = arr[1];
    let speedLimit = getLimit(area);
    let speedDifference = speed - speedLimit;

    if (speedDifference > 0 && speedDifference <= 20) {
        console.log("speeding")
    } else if (speedDifference > 20 && speedDifference <= 40) {
        console.log("excessive speeding");
    } else if (speedDifference > 40) {
        console.log("reckless driving");
    } else {
        return "";
    }

    function getLimit(area) {
        let limit = 0;
        switch(area) {
            case "motorway": limit = 130; break;
            case "interstate": limit = 90; break;
            case "city": limit = 50; break;
            case "residential": limit = 20; break;
        }
        return limit;
    }
}

//roadRadar([40, 'city']);
//roadRadar([21, 'residential']);