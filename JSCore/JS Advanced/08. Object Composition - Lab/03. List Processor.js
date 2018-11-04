function listProcessor(commands) {
    const process = (function () {
        let arr = [];
        const service = {
            add: function (str) {
                arr.push(str);
            },
            remove: function (str) {
                arr = arr.filter(x => x != str);
            },
            print: function () {
                console.log(arr.join());
            }
        };
        return service;
    })();
    for (let command of commands) {
        let tokens = command.split(' ');
        process[tokens[0]](tokens[1]);
    }
}

// let arr = ['add hello', 'add again', 'remove hello', 'add again', 'print'];
// listProcessor(arr);