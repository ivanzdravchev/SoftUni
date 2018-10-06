function moviePrices(arr) {
    let movie = arr[0].toLowerCase();
    let day = arr[1].toLowerCase();
    let result = 0;
    if (movie == "the godfather") {
        switch(day) {
            case "monday": result = 12; break;
            case "tuesday": result = 10; break;
            case "wednesday":
            case "friday": result = 15; break;
            case "thursday": result = 12.50; break;
            case "saturday": result = 25; break;
            case "sunday": result = 30; break;
            default: result = "error";
            break;
        }
    } else if (movie == "schindler's list") {
        switch(day) {
            case "monday":
            case "tuesday":
            case "wednesday":
            case "thursday":
            case "friday": result = 8.50; break;
            case "saturday":
            case "sunday": result = 15; break;
            default: result = "error";
            break;
        }
    } else if (movie == "casablanca") {
        switch (day) {
            case "monday":
            case "tuesday":
            case "wednesday":
            case "thursday":
            case "friday": result = 8; break;
            case "saturday":
            case "sunday": result = 10; break;
            default: result = "error";
            break;
        }
    } else if (movie == "the wizard of oz") {
        switch (day) {
            case "monday":
            case "tuesday":
            case "wednesday":
            case "thursday":
            case "friday": result = 10; break;
            case "saturday":
            case "sunday": result = 15; break;
            default: result = "error";
            break;
        }
    } else {
        return "error";
    }
    return result;
}

//let arr = ["Schindler's List", "Thursday"];
//console.log(moviePrices(arr));