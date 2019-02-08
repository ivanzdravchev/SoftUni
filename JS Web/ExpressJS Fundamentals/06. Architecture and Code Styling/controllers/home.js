const Article = require('../models/Article');
const User = require('../models/User');

module.exports = {
  index: (req, res) => {
    Article.find()
      .populate('author')
      .then((articles) => {
        res.render('home/index', { articles });
      }).catch(console.error);
  }
}
