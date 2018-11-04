function sum() {
    let obj = (function () {
        let sel1, sel2, result;
        return {
            init: (selector1, selector2, resultSelector) => {
                sel1 = $(selector1);
                sel2 = $(selector2);
                result = $(resultSelector);
            },
            add: () => {
                result.val(+sel1.val() + +sel2.val());
            },
            subtract: () => {
                result.val(+sel1.val() - +sel2.val());
            }
        }
    })();
    obj.init($('#num1'), $('#num2'), $('#result'));
    $('#sumButton').click(obj.add);
    $('#subtractButton').click(obj.subtract);
    return obj;
}
sum();