function DNAex(arr) {
    let regex = /([A-Za-z!@#$?]+)=(\d+)--(\d+)<<([A-Za-z]+)/g;
    let dictionary = {};
    for (let i = 0; i < arr.length; i++) {
        if(arr[i] == 'Stop!') {
            break;
        }
        let match = regex.exec(arr[i]);
        while (match) {
            let geneName = match[1].split(/[!@#$?]+/).filter(x => x != '').join('');
            let nameLength = match[2];
            let countOfGenes = match[3];
            let organism = match[4];
            if (nameLength == geneName.length) {
                if(dictionary[organism] == undefined) {
                    dictionary[organism] = +countOfGenes;
                } else {
                    dictionary[organism] += +countOfGenes;
                }
            }
            match = regex.exec(arr[i]);
        }
    }
    let otherDic = Object.keys(dictionary).sort((a, b) => dictionary[b] - dictionary[a] );
    //console.log(dictionary);
    for (let key in otherDic) {
        console.log(`${otherDic[key]} has genome size of ${dictionary[otherDic[key]]}`);
    }
}

// let arr = [
//     '!@ab?si?di!a@=7--152<<human',
//     'b!etu?la@=6--321<<dog',
//     '!curtob@acter##ium$=14--230<<dog',
//     '!some@thin@g##=9<<human',
//     'Stop!'
// ];

// let arr2 = [
//     '=12<<cat',
//     '!vi@rus?=2--142',
//     '?!cur##viba@cter!!=11--800<<cat',
//     '!fre?esia#=7--450<<mouse',
//     '@pa!rcuba@cteria$=13--351<<mouse',
//     '!richel#ia??=8--900<<human',
//     'Stop!'
// ]
// DNAex(arr2);