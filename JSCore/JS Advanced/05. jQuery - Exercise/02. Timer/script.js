function timer() {
    let startBtn = $('#start-timer');
    let stopBtn = $('#stop-timer');
    let interval;
    let seconds = 0;

    startBtn.on('click', start);
    stopBtn.on('click', stop);

    function start() {
        $(startBtn).prop('disabled', true);
        $(stopBtn).prop('disabled', false);

        interval = setInterval(function () {
            seconds += 1;
            $('#hours').text(("0" + Math.floor(seconds / 3600)).slice(-2));
            $('#minutes').text(("0" + Math.floor(seconds / 60 % 60)).slice(-2));
            $('#seconds').text(("0" + seconds % 60).slice(-2));
        }, 1000);
    }

    function stop() {
        clearInterval(interval);
        $(startBtn).prop('disabled', false);
        $(stopBtn).prop('disabled', true);
    }
}