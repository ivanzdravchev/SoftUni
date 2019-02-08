const Article = require('../models/Article');

module.exports = {
    createGet: (req, res) => {
        res.render('article/create');
    },
    createPost: (req, res) => {
        let articleBody = req.body;
        let user = req.user;
        if (!articleBody.title || !articleBody.content) {
            articleBody.error = 'Please write a title and content.';
            res.render('article/create', articleBody);
            return;
        }
        articleBody.author = req.user._id;
        Article.create(articleBody).then((result) => {
            user.articles.push(result._id);
            return user.save();
        }).then(() => {
            res.redirect('/');
        }).catch(console.error);
    },
    details: (req, res) => {
        let { articleId } = req.params;
        Article.findById(articleId)
            .populate('author')
            .then((article) => {
                if (!article) {
                    res.redirect('/');
                    return;
                }
                let isEligible = false;
                if (req.user) {
                    isEligible = req.user.isAuthor(article) || req.user.isInRole('Admin');
                }
                res.render('article/details', { article, isEligible });
            }).catch(console.error);
    },
    editGet: (req, res) => {
        let { articleId } = req.params;
        Article.findById(articleId)
            .then((article) => {
                if (!article) {
                    res.redirect('/');
                    return;
                }
                let isEligible = false;
                if (req.user) {
                    isEligible = req.user.isAuthor(article) || req.user.isInRole('Admin');
                }
                if (!isEligible) {
                    res.render('article/details', { article, isEligible });
                    return;
                }
                res.render('article/edit', article);
            }).catch(console.error);
    },
    editPost: (req, res) => {
        let { articleId } = req.params;
        let articleBody = req.body;
        Article.findById(articleId)
            .then((article) => {
                if (!article) {
                    res.redirect('/');
                    return;
                }
                if (!articleBody.title || !articleBody.content) {
                    articleBody.error = 'Please write a title and content.';
                    res.render('article/edit', articleBody);
                    return;
                }
                article.title = articleBody.title;
                article.content = articleBody.content;
                article.save()
                    .then(() => {
                        res.redirect('/');
                    })
            }).catch(console.error);

    },
    deleteGet: (req, res) => {
        let { articleId } = req.params;
        Article.findById(articleId)
            .then((article) => {
                if (!article) {
                    res.redirect('/');
                    return;
                }
                let isEligible = false;
                if (req.user) {
                    isEligible = req.user.isAuthor(article) || req.user.isInRole('Admin');
                }
                if (!isEligible) {
                    res.render('article/details', { article, isEligible });
                    return;
                }
                res.render('article/delete', article);
            }).catch(console.error);
    },
    deletePost: (req, res) => {
        let { articleId } = req.params;
        Article.findById(articleId)
            .then((article) => {
                if (!article) {
                    res.redirect('/');
                    return;
                }
                Article.findByIdAndRemove(articleId).exec();
                console.log(req.user.articles);
                req.user.articles.pull(articleId);
                return req.user.save();
            })
            .then(() => {
                res.redirect('/');
            })
            .catch(console.error);
    }
};