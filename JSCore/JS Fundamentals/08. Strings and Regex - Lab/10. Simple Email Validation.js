function simpleEmailValidation(email) {
    let regex = /^([A-Za-z0-9]+@[a-z]+.[a-z]+)$/g;
    let wat = email.match(regex);
    return regex.test(email) == false ? 'Invalid' : 'Valid';
}

// console.log(simpleEmailValidation('valid@email.com'));
// console.log(simpleEmailValidation('invalid@emai1l.com'));