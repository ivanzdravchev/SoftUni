; const snakeGame = (function () {
    function createGame(config) {
        gameBoard.createGameboard(config);
    }

    function start() {
        gameBoard.start();
    }

    return {
        createGame : createGame,
        start: start
    };
})();