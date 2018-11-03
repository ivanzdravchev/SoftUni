function keplerProblem(mean, ecc) {
    console.log(+(approx(mean, ecc, 0).toFixed(9)));

    function approx(E0, ecc, series) {
        if (Math.abs(mean - (E0, ecc * Math.sin(E0))) < 1e-9 || series > 200) {
            return E0;
        } 
        return approx(E0 - (E0 - ecc * Math.sin(E0) - mean) / (1 - ecc * Math.cos(E0)), ecc, ++series);
    }
}

// keplerProblem(4.8, 0.2);