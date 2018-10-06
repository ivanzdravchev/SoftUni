function oddNumbers(limit) {
    let result = "";
    for(let i = 1; i <= limit; i++) {
        if(i % 2 == 1) 
            result += `${i}\n`;
    }
    return result
}

//console.log(oddNumbers(15));