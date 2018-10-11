function usernames(arr) {
    let resultArr = [];
    for(let el of arr) {
        let [nick, domain] = el.split('@');
        let domainTokens = domain.split('.');
        let username = nick + '.';
        domainTokens.forEach(part => {
            username += part[0];
        });
        resultArr.push(username);
    }
    return resultArr.join(', ');
}

// let arr = ['pesho@gmail.com', 'foo@bar.com'];
// console.log(usernames(arr));