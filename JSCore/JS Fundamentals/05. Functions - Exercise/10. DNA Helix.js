function DNA_Helix(num) {
    let dna = 'ATCGTTAGGGATCGTTAGGG'; // length = 10 x 2
    let position = 0;
    for(let i = 1; i <= num; i++) {
        if (i % 4 == 1) {
            console.log(`**${dna[position++]}${dna[position++]}**`);
        } else if (i % 4 == 2) {
            console.log(`*${dna[position++]}--${dna[position++]}*`);
        } else if (i % 4 == 3) {
            console.log(`${dna[position++]}----${dna[position++]}`);
        } else if (i % 4 == 0) {
            console.log(`*${dna[position++]}--${dna[position++]}*`);
        }
        if(position > 10) {
            position -= 10;
        }
    }
}

//DNA_Helix(10);