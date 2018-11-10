function track() {
    let bugReports = [];
    let currentID = 0;
    let currentSelector = '';

    function report(author, description, reproducible, severity) {
        let rep = {
            ID: currentID,
            author,
            description,
            reproducible,
            severity,
            status: 'Open'
        };
        currentID += 1;

        bugReports.push(rep);
        print(currentSelector);
    }

    function setStatus(id, newStatus) {
        for (let obj of bugReports) {
            if (obj.ID == id) {
                obj.status = newStatus;
            }
        }
        print(currentSelector);
    }

    function remove(id) {
        bugReports = bugReports.filter(rep => rep.ID != id);
        print(currentSelector);
    }

    function sort(method) {
        if (method == 'author') {
            bugReports.sort((a, b) => a[method].localeCompare(b[method]));
        } else {
            bugReports.sort((a, b) => a[method] - b[method]);
        }
        print(currentSelector);
    }

    function output(selector) {
        currentSelector = selector;
    }

    function print (selector) {
        let element = $(selector);
        element.empty();
        for (let i = 0; i < bugReports.length; i++) {
            let report = $('<div>');
            report.attr('id', `report_${bugReports[i].ID}`);
            report.addClass('report');
            let description = $('<div class="body">')
                .append($('<p>').text(`${bugReports[i].description}`));
            let title = $('<div class="title">')
                .append($('<span class="author">').text(`Submitted by: ${bugReports[i].author}`))
                .append($('<span class="status">').text(`${bugReports[i].status} | ${bugReports[i].severity}`));
            report.append(description).append(title);
            element.append(report);
        }
    }

    return { report, setStatus, remove, sort, output };
}

// let service = track();
// service.output($('#content'));
// service.report('The Author', 'Issue', true, 9001);