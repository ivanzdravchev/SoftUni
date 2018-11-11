function requestValidator(obj) {
    let methods = ['GET', 'POST', 'DELETE', 'CONNECT'];
    let version = ['HTTP/0.9', 'HTTP/1.0', 'HTTP/1.1', 'HTTP/2.0'];
    let regex = new RegExp('^[A-Za-z\.0-9]+$', 'g');
    let messageRegex = new RegExp('^[^<>\\\\&\'\"]*$', 'g');

    if (!obj.hasOwnProperty('method') || !methods.includes(obj.method)) {
        throw new Error('Invalid request header: Invalid Method');
    }
    if (!obj.hasOwnProperty('uri') || (!regex.test(obj.uri) && obj.uri != '*')) {
        throw new Error('Invalid request header: Invalid URI');
    }
    if (!obj.hasOwnProperty('version') || !version.includes(obj.version)) {
        throw new Error('Invalid request header: Invalid Version');
    }
    if (!obj.hasOwnProperty('message') || !messageRegex.test(obj.message)) {
        throw new Error('Invalid request header: Invalid Message');
    }

    return obj;
}

// let obj = {
//     method: 'GET',
//     uri: 'svn.public.catalog',
//     version: 'HTTP/1.1',
//     message: 'asl\\\\\pls'
// };

// console.log(requestValidator(obj));