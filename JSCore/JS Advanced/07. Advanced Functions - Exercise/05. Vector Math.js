let result = (function vectorMath() {
    return {
        add: (vectorA, vectorB) => { return [vectorA[0] + vectorB[0], vectorA[1] + vectorB[1]]},
        multiply: (vector, scalar) => { return [vector[0] * scalar, vector[1] * scalar]},
        length: (vector) => { return Math.sqrt(vector[0] * vector[0] + vector[1] * vector[1])},
        dot: (vectorA, vectorB) => { return vectorA[0] * vectorB[0] + vectorA[1] * vectorB[1] },
        cross: (vectorA, vectorB) => { return vectorA[0] * vectorB[1] - vectorA[1] * vectorB[0] }
    };
})();

// console.log(result.add([1, 1], [1, 0]));
// console.log(result.multiply([3.5, -2], 2));
// console.log(result.length([3, -4]));
// console.log(result.dot([1, 0], [0, -1]));
// console.log(result.cross([3, 7], [1, 0]));