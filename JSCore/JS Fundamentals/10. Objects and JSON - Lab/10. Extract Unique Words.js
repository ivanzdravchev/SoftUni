function extractUniqueWords(arr) {
    let set = new Set();
    for (let line of arr) {
        let matches = line.match(/[A-Za-z0-9_]+/g);
        matches.forEach(word => set.add(word.toLowerCase()));
    }
    console.log([...set.values()].join(', '));
}

// let arr = [
//     'Lorem lorem ipsum text more tEXT',
//     'too lazy to go copy it zzz.. too lazy'
// ];
// extractUniqueWords(arr);