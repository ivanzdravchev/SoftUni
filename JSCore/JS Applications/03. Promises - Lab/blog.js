function attachEvents() {
    let kinvey = 'https://baas.kinvey.com/appdata/';
    let appKey = 'kid_ByHKWmgy4';

    let postsUrl = kinvey + appKey + '/posts';
    let commentsUrl = kinvey + appKey + '/comments';
    let auth = {'Authorization': 'Basic ' + btoa('guest:guest')};

    $('#btnLoadPosts').click(loadPosts);
    $('#btnViewPost').click(viewPost);

    function loadPosts() {
        $.get({
            url: postsUrl,
            headers: auth
        }).then(getPosts).catch(displayError);
    }

    function getPosts(posts) {
        for (let index in posts) {
            let id = posts[index]._id;
            let title = posts[index].title;
            $('#posts').append($(`<option value="${id}">${title}</option>`));
        }
    }

    function displayError(err) {
        let errorDiv = $('<div style="color:red">').text(`Error: ${err.status} (${err.statusText})`);
        $(document.body).prepend(errorDiv);
        setTimeout(function() {
            $(errorDiv).fadeOut(function() {
                $(errorDiv).remove();
            });
        }, 3000);
    }

    function viewPost() {
        let postId = $('#posts').val();
        if (!postId) return;

        let requestPosts = $.get({
            url: postsUrl + `/${postId}`,
            headers: auth
        });
        let requestComments = $.get({
            url: commentsUrl + `/?query={"post_id":"${postId}"}`,
            headers: auth
        });

        Promise.all([requestPosts, requestComments])
            .then(displayPostsWithComments).catch(displayError);
    }

    function displayPostsWithComments([post, comments]) {
        $('#post-title').text(post.title);
        $('#post-body').text(post.body);
        // console.log(comments);
        $('#post-comments').empty();
        for (let comment of comments) {
            $('#post-comments').append($('<li>').text(comment.text));
        }
    }
}