function printChessboard(n) {
    let html = '<div class="chessboard">\n';
    for(let i = 1; i <= n; i++) {
        html += "  <div>\n";
        let color = (i % 2 == 1) ? 'black' : 'white';
        for(let j = 1; j <= n; j++) {
            if(j % 2 == 1) {
                html += `    <span class="${color}"></span>\n`
            }
            else {
                html += `    <span class="${color}"></span>\n`
            }
            color = (color === 'black') ? 'white' : 'black';
        }
        html += "  </div>\n";
    }
    html += "</div>";
    return html;
}

function addCss() {
    let css = document.createElement("style");
    css.innerHTML = `
        body { background: #CCC; }
        .chessboard { display: inline-block; }
        .black, .white {
            width: 50px; height: 50px;
            display: inline-block; }
        .black { background: black; }
        .white { background: white }`;

    document.getElementsByTagName("head")[0].appendChild(css);
    document.body.innerHTML = printChessboard(8);
}

addCss();