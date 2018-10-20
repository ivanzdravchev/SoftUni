; const gameBoard = (function () {
    let configData;
    let interval;
    const food = {
        x: null,
        y: null
    }
    let score = 0;
    let alive = true;

    function getRandomInt(max) {
        return Math.floor(Math.random() * Math.floor(max));
    }

    function generateGameboardUI(size) {
        let gameBoard = "<table>";

        for (let i = 0; i < size; i++) {
            gameBoard += "<tr>";

            for (let j = 0; j < size; j++) {
                gameBoard += `<td id="${j}-${i}"></td>`;
            }

            gameBoard += "</tr>";
        }
        gameBoard += "</table>";
        document.getElementById('game').innerHTML = gameBoard;

        return gameBoard;
    }

    function createGameboard(config) {
        configData = config;

        generateGameboardUI(config.boardSize);
        snake.createSnake(config);
    }

    function checkSnakeHeadPosition() {
        const snakeData = snake.getSnake();

        if (snakeData.headPositionX < 0 || snakeData.headPositionX > configData.boardSize - 1) {
            gameOver();
        }

        if (snakeData.headPositionY < 0 || snakeData.headPositionY > configData.boardSize - 1) {
            gameOver();
        }

        if (snakeData.body.includes(`${snakeData.headPositionX}-${snakeData.headPositionY}`)) {
            gameOver();
        }
    }

    function setupUserInput() {
        document.addEventListener('keydown', (event) => {
            if (event.keyCode === 37) {
                snake.setMoveDirection('left');
            } else if (event.keyCode === 38) {
                snake.setMoveDirection('up')
            } else if (event.keyCode === 39) {
                snake.setMoveDirection('right')
            } else if (event.keyCode === 40) {
                snake.setMoveDirection('down')
            }
        });
    }

    function gainPoint() {
        score += 1;
    }

    function checkSnakeEating() {
        const snakeData = snake.getSnake();

        if (snakeData.headPositionX === food.x && snakeData.headPositionY === food.y) {
            //debugger;
            gainPoint();
            snake.grow();
            generateFood();

            document.getElementById('score').innerText = `Score: ${score}`;
        }
    }

    function setupSnake() {
        snake.setMoveDirection('right');

        interval = setInterval(() => {
            snake.moveHead();
            checkSnakeHeadPosition();
            checkSnakeEating();
            if (alive) {
                snake.updateSnakePosition();
            }
        }, configData.speed);
    }

    function generateFood() {
        const x = getRandomInt(configData.boardSize);
        const y = getRandomInt(configData.boardSize);

        const snakeData = snake.getSnake();

        if (snakeData.body.includes(`${x}-${y}`)) {
            return generateFood();
        }

        food.x = x;
        food.y = y;

        document.getElementById(`${x}-${y}`).className = "food";
    }

    function start() {
        setupSnake();
        setupUserInput();
        generateFood();
    }

    function gameOver() {
        alert("Game Over");
        clearInterval(interval);
        alive = false;
    }

    return {
        createGameboard,
        start
    };
})();