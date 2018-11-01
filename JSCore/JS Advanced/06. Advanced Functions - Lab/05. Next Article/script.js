function getArticleGenerator(articles) {
    return function() {
        if (articles.length > 0) {
            $('#content').append($('<article>').append($('<p>').text(articles.shift())));
        }
    }
}