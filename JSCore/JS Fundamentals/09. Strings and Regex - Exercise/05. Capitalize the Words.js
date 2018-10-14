function capitalizeWords(str) {
    return str.split(' ')
        .map(word => word[0].toUpperCase() +
         word.substr(1).toLowerCase())
        .join(' ');
}

// console.log(capitalizeWords('Capitalize these wOrds'));