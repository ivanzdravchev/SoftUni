function nowPlaying(arr) {
    let song = arr[0];
    let artist = arr[1];
    let duration = arr[2];
    return `Now Playing: ${artist} - ${song} [${duration}]`;
}

//let arr = ['A Song', 'Artist', '2:22'];
//console.log(nowPlaying(arr));