function countWordsWithMaps(arr) {
    let map = new Map();
    let words = arr[0].toLowerCase().match(new RegExp(`[A-Za-z0-9_]+`, 'gi')).sort();
    for (let word of words) {
        if (!map.has(word)) {
            map.set(word, 0);
        }
        map.set(word, map.get(word) + 1);
    }
    for (let [key, value] of map) {
        console.log(`'${key}' -> ${value} times`);
    }
}

// let arr = ['Far too slow, you\'re far too slow.'];
// countWordsWithMaps(arr);