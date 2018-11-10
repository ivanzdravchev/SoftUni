(function stringExtension() {
    String.prototype.ensureStart = function(str) {
        return this.indexOf(str) != 0 ? str + this : this.toString();
    };

    String.prototype.ensureEnd = function(str) {
        return this.indexOf(str) != this.length - str.length ? this + str : this.toString();
    };

    String.prototype.isEmpty = function() {
        return this.length == 0 ? true : false;
    };

    String.prototype.truncate = function(n) {
        if (this.length <= n) {
            return this.toString();
        } else if (n < 4) {
            return '.'.repeat(n);
        } else if (this.indexOf(' ') == -1) {
            return this.substr(0, n - 3) + '...';
        } else {
            let wordsArr = this.split(' ');
            let result = wordsArr.shift();

            for (let word of wordsArr) {
                if ((result + word).length + 3 < n) {
                    result += ' ' + word;
                } else {
                    return result + '...';
                }
            }
        }
    };

    String.format = function(string, ...params) {
        for (let i = 0; i < params.length; i++) {
            string = string.replace(`{${i}}`, params[i]);
        }
        return string;
    }
})();

// let str = 'my string';
// str = str.ensureStart('my');
// console.log(str);
// str = str.ensureStart('hello ');
// console.log(str);
// str = str.truncate(16);
// console.log(str);
// str = str.truncate(14);
// console.log(str);
// str = str.truncate(8);
// console.log(str);
// str = str.truncate(4);
// console.log(str);
// str = str.truncate(2);
// console.log(str);
// str = String.format('The {0} {1} fox', 'quick', 'brown');
// console.log(str);
// str = String.format('jumps {0} {1}', 'dog');
// console.log(str);