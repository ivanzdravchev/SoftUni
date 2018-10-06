function filterByAge(min, name1, age1, name2, age2) {
    let person1 = {name: name1, age: age1};
    let person2 = {name: name2, age: age2};
    if(person1.age >= min) 
        console.log(`{ name: '${person1.name}', age: ${person1.age} }`);
    if(person2.age >= min) {
        console.log(`{ name: '${person2.name}', age: ${person2.age} }`);
    }
}

//filterByAge(12, 'Ivan', 15, 'Asen', 9);