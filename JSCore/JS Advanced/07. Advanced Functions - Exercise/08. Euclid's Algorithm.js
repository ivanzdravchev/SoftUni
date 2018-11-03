function euclidAlgorithm_rec(a, b) {
    if (!b) {
        return a;
    }

    return euclidAlgorithm_rec(b, a % b);
}

// console.log(euclidAlgorithm_rec(252, 105));