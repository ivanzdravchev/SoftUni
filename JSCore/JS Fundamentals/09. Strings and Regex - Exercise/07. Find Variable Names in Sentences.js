function findVariables(string) {
    let regex = /\b_([A-Za-z0-9]+)\b/g;
    let variables = [];
    let match = regex.exec(string);
    while (match) {
        variables.push(match[1]);
        match = regex.exec(string);
    }
    return variables.join(',');
}

// console.log(findVariables('Person _id_ and _age variables'));