function tickets(arr, criteria) {
    class Ticket {
        constructor(destination, price, status) {
            this.destination = destination;
            this.price = price;
            this.status = status;
        }
    }

    let ticketArr = [];
    for (let ticket of arr) {
        let tokens = ticket.split('|');
        ticketArr.push(new Ticket(tokens[0], +tokens[1], tokens[2]));
    }

    return ticketArr.sort((a, b) => a[criteria] > b[criteria]);
}

// let input = ['Philadelphia|94.20|available',
//         'New York City|95.99|available',
//         'New York City|95.99|sold',
//         'Boston|126.20|departed'];

// console.log(tickets(input, 'destination'));