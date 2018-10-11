function concatenateReversed(arr) {
    let text = arr.join('');
    let reversed = text.split('')
        .reverse()
        .join('');
    console.log(reversed);
}

// let arr = ['I', 'am', 'student'];
// concatenateReversed(arr);