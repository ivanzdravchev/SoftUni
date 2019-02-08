const homeController = require('../controllers/home');
const userController = require('../controllers/user');
const articleController = require('../controllers/article');
const auth = require('./auth');

module.exports = (app) => {
    app.get('/', homeController.index);
    app.get('/user/register', auth.isAnonymous, userController.registerGet);
    app.post('/user/register', auth.isAnonymous, userController.registerPost);

    app.get('/user/login', auth.isAnonymous, userController.loginGet);
    app.post('/user/login', auth.isAnonymous, userController.loginPost);

    app.get('/user/logout', auth.isAuthed, userController.logout);
    
    app.get('/article/create', auth.isAuthed, articleController.createGet);
    app.post('/article/create', auth.isAuthed, articleController.createPost);

    app.get('/article/details/:articleId', articleController.details);

    app.get('/article/edit/:articleId', articleController.editGet);
    app.post('/article/edit/:articleId', articleController.editPost);

    app.get('/article/delete/:articleId', articleController.deleteGet);
    app.post('/article/delete/:articleId', articleController.deletePost);

    app.all('*', homeController.index);
};
