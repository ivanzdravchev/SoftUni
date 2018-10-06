function calculateVat(input) {
    let sum = 0;
    let vat = 0;
    input.forEach(el => {
        sum += +el;
    });
    vat = sum * 0.2;
    console.log(`sum = ${sum}`);
    console.log(`VAT = ${vat}`);
    console.log(`total = ${sum + vat}`);
}

//let arr = ["1.20", "2.60", "3.50"];
//calculateVat(arr);