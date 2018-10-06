function lastMonth(arr) {
    let day = arr[0];
    let month = arr[1];
    let year = arr[2];

    let date = new Date(year, month - 1, 0);
    return date.getDate();
}

//let arr = [17, 3, 2002];
//console.log(lastMonth(arr));