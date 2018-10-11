function formFiller(username, mail, phone, lines) {
    lines.forEach(line => {
        line = line.replace(/<![A-Za-z]+!>/, username);
        line = line.replace(/<@[A-Za-z]+@>/, mail);
        line = line.replace(/<\+[A-Za-z]+\+>/, phone);
        console.log(line);
    });
}