function printLines(num) {
    let html = "<ul>\n"
    for(let i = 1; i <= num; i++) {
        if(i % 2 == 1) {
            html += `<li><span style='color:green'>${i}</span><li>\n`; 
        }
        else {
            html += `<li><span style='color:blue'>${i}</span><li>\n`;
        }
    }
    html += "</ul>";
    return html;
}

//console.log(printLines(10));