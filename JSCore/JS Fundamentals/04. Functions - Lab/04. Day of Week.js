function dayOfWeek(str) {
    let result = "error";
    switch(str.toLowerCase()) {
        case "monday": result = 1; break;
        case "tuesday": result = 2; break;
        case "wednesday": result = 3; break;
        case "thursday": result = 4; break;
        case "friday": result = 5; break;
        case "saturday": result = 6; break;
        case "sunday": result = 7; break;
    }
    //console.log(typeof result);
    return result;
}

//console.log(dayOfWeek("mOnDaY"));
//console.log(dayOfWeek("not monday"));