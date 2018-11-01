function commandProcessor(arr) {
    const funcs = {
        current: '',
        append: function (str) {
            this.current += str;

            return this;
        },
        removeStart: function (num) {
            this.current = this.current.substr(num);

            return this;
        },
        removeEnd: function (num) {
            this.current = this.current.substr(0, this.current.length - num);

            return this;
        },
        print: function () {
            console.log(this.current);

            return this;
        },
        exec: function (arr) {
            arr.forEach(el => {
                let tokens = el.split(' ');
                this[tokens[0]].call(this, tokens[1]);
            });
        }
    }
    funcs.exec(arr);
}

let arr = ['append hello',
    'append again',
    'removeStart 3',
    'removeEnd 4',
    'print'
];
commandProcessor(arr);