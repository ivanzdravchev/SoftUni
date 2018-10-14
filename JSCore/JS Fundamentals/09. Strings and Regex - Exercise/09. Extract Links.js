function extractLinks(arr) {
    let links = [], match;
    let regex = /www\.[A-Za-z0-9-]+(\.[a-z]+)+/g;
    for (let line of arr) {
        while (match = regex.exec(line)) {
            links.push(match[0]);
        }
    }
    return links.join('\n');
}

// let arr = [
//     'Join WebStars now for free, at www.web-stars.com',
//     'You can also support our partners:',
//     'Internet - www.internet.com',
//     'WebSpiders - www.webspiders101.com',
//     'Sentinel - www.sentinel.-ko '];
// console.log(extractLinks(arr));