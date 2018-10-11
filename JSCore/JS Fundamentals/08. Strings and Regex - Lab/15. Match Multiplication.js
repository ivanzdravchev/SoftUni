function matchMultiplication(text) {
    text = text.replace(/(-?\d+)\s*\*\s*(-?\d+(\.\d+)?)/g,
    (matchSubstr, num1, num2) => +num1 * +num2);
    console.log(text);
}