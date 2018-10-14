function findOccurrences(text, word) {
    let collection = text.match(new RegExp(`\\b${word}\\b`, 'gi'));
    console.log(collection ? collection.length : 0);
}

// findOccurrences('How and how hOw?', 'how');
// findOccurrences('None', 'how');