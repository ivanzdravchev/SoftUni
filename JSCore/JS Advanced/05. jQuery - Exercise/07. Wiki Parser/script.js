function wikiParser(selector) {
    let text = $(selector).text();
    text = text.replace(/'{3}([\w ]+)'{3}/g, '<b>$1</b>')
        .replace(/'{2}([\w ]+)'{2}/g, '<i>$1</i>')
        .replace(/={3}([\w ]+)={3}/g, '<h3>$1</h3>')
        .replace(/={2}([\w ]+)={2}/g, '<h2>$1</h2>')
        .replace(/={1}([\w ]+)={1}/g, '<h1>$1</h1>')
        .replace(/\[{2}([\w ]+)\]{2}/g, '<a href="/wiki/$1">$1</a>')
        .replace(/\[{2}([\w ]+)\|([\w ]+)\]{2}/g, '<a href="/wiki/$1">$2</a>');

    $(selector).html(text);
}