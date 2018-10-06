function wordsUppercase(str) {
    return str.toUpperCase().split(/\W+/).filter(w => w != '').join(", ");
}

//console.log(wordsUppercase("Hi, hOw, are you??"));