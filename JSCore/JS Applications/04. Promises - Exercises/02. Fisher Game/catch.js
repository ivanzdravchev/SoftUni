function attachEvents() {
    $('.load').click(loadCatches);
    $('.add').click(addCatch);
    let hostUrl = 'https://baas.kinvey.com/appdata/kid_H1N2kFbkE/biggestCatches/';
    let auth = {
        "Authorization": "Basic " + btoa('guest:guest'),
        "Content-type": "application/json"
    };

    function addCatch() {
        let angler = $('#addForm input.angler').val().trim();
        let weight = $('#addForm input.weight').val().trim();
        let species = $('#addForm input.species').val().trim();
        let location = $('#addForm input.location').val().trim();
        let bait = $('#addForm input.bait').val().trim();
        let captureTime = $('#addForm input.captureTime').val().trim();
        let requestBody = {
            "angler": angler,
            "weight": +weight,
            "species": species,
            "location": location,
            "bait": bait,
            "captureTime": +captureTime
        };
        $.ajax({
            method: "POST",
            url: hostUrl,
            headers: auth,
            data: JSON.stringify(requestBody)
        }).then(loadCatches).catch(displayError);
    }

    function loadCatches() {
        $.get({
            url: hostUrl,
            headers: auth
        }).then(displayCatches).catch(displayError);
    }

    function displayCatches(catches) {
        $('#catches').empty();
        for (let entry of catches) {
            getCatchHTML(entry);
        }

        function getCatchHTML(entry) {
            let catchDiv = $(`<div class="catch" data-id="${entry._id}">`);
            let divHtml = `
                <label>Angler</label>
                    <input type="text" class="angler" value="${entry.angler}" />
                    <label>Weight</label>
                    <input type="number" class="weight" value="${entry.weight}" />
                    <label>Species</label>
                    <input type="text" class="species" value="${entry.species}" />
                    <label>Location</label>
                    <input type="text" class="location" value="${entry.location}" />
                    <label>Bait</label>
                    <input type="text" class="bait" value="${entry.bait}" />
                    <label>Capture Time</label>
                    <input type="number" class="captureTime" value="${entry.captureTime}" />`;
            catchDiv.append(divHtml)
                .append($('<button>Update</button>').addClass('update').click(() => updateEntry(entry._id)))
                .append($('<button>Delete</button>').addClass('delete').click(() => deleteEntry(entry._id)));

            $('#catches').append(catchDiv);
        }
    }

    function updateEntry(entryID) {
        let angler = $(`div[data-id=${entryID}] input.angler`).val().trim();
        let weight = $(`div[data-id=${entryID}] input.weight`).val().trim();
        let species = $(`div[data-id=${entryID}] input.species`).val().trim();
        let location = $(`div[data-id=${entryID}] input.location`).val().trim();
        let bait = $(`div[data-id=${entryID}] input.bait`).val().trim();
        let captureTime = $(`div[data-id=${entryID}] input.captureTime`).val().trim();
        let requestBody = {
            "angler": angler,
            "weight": weight,
            "species": species,
            "location": location,
            "bait": bait,
            "captureTime": captureTime
        };
        $.ajax({
            method: "PUT",
            url: hostUrl + entryID,
            headers: auth,
            data: JSON.stringify(requestBody)
        }).then(loadCatches).catch(displayError);
    }

    function deleteEntry(entryID) {
        $.ajax({
            method: "DELETE",
            url: hostUrl + entryID,
            headers: auth
        }).then(loadCatches).catch(displayError);
    }

    function displayError(err) {
        console.log(err);
    }
}