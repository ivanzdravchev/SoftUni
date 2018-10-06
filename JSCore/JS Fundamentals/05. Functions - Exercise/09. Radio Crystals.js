function radioCrystals(arr) {
    function printStep(step, count, chunk) {
        if (count > 0) {
            console.log(`${step} x${count}`);
            console.log(`Transporting and washing`);
            chunk = Math.floor(chunk);
        }
    }
    let thickness = arr[0];
    for(let i = 1; i < arr.length; i++) {
        let chunk = arr[i];
        let count = 0;
        console.log(`Processing chunk ${chunk} microns`);
        while (thickness <= chunk / 4) {
            count++;
            chunk /= 4;
        }
        printStep('Cut', count, chunk);
        count = 0;
        while (thickness <= chunk * 0.8) {
            count++;
            chunk *= 0.8;
        }
        printStep('Lap', count, chunk);
        count = 0;
        while (thickness <= chunk - 20) {
            count++;
            chunk -= 20;
        }
        printStep('Grind', count, chunk);
        count = 0;
        while (thickness <= chunk - 1) {
            count++;
            chunk -= 2;
        }
        printStep('Etch', count, chunk);
        if(thickness == chunk + 1) {
            console.log(`X-ray x1`);
            chunk++;
        }
        console.log(`Finished crystal ${thickness} microns`);
    }
}

//let arr = [1375, 50000];
//let arr2 = [1000, 4000, 8100, 999];
//radioCrystals(arr2);