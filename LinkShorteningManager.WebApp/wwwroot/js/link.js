class Link {
    constructor(id, url, shortKey, creationDate, clickCount) {
        this.id = id;
        this.url = url;
        this.shortKey = shortKey;
        this.creationDate = creationDate;
        this.clickCount = clickCount
    }
}

function createErrorLine(message) {
    return $('<li />').html(message);;
}

function getElementById(id) {
    return $('#' + id);
}

function getInputValue(id) {
    return $('#' + id).val();
}

function setInputValue(id, value) {
    $('#' + id).val(value);
}

function setInnerBlockData(id, data) {
    getElementById(id).html(data);
}

function getInnerTextOfElement(id) {
    return getElementById(id).html();
}

function resetErrors() {
    setInnerBlockData("errorList", "");
}

function resetShortUrlValue() {
    setInputValue("shortKeyInput", "")
}

function viewErrors(errors) {
    errors.forEach((error) => {
        addError(error);
    });
}

function addError(errorMessage) {
    let errorList = getElementById("errorList");
    let errorLine = createErrorLine(errorMessage);
    errorList.append(errorLine);
}

function createLinkWithDataInputs(generateId) {
    let id = getInnerTextOfElement("linkId");

    if (generateId) {
        id = generateUUID();
    }

    let url = getInputValue("urlInput");
    let shortKey = getInputValue("shortKeyInput");
    let creationDate = getInputValue("creationDateInput"); 
    let clickCount = getInputValue("clickCountInput"); 

    return new Link(
        id,
        url == "" ? null : url,
        shortKey == "" ? null : shortKey,
        creationDate == "" ? null : creationDate,
        clickCount == "" ? null : clickCount
    )
}

function isValidUrl(value) {
    let regex = /https?:\/\/[a-zA-Z0-9.\/]+.[a-z]*[a-zA-Z0-9.\/_?=%\-\&]*/;
    let found = value.match(regex);

    if (found != null && found.length == 1) {
        return true;
    }

    return false;
}

function generateUUID() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g,
        c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

async function getShortUrl() {
    let errors = [];

    if (!isValidUrl(getInputValue("urlInput"))) {
        errors.push("Url is not valid!")
    }

    resetErrors();

    if (errors.length != 0) {
        viewErrors(errors);
    }
    else {
        let response = await fetch('/Link/GetShortKey/', {
            method: 'GET',
        });

        if (!response.ok) {
            viewErrors(Object.values(await response.json()));
        } else {
            setInputValue("shortKeyInput", await response.json());
        }
    }
}

async function addLink() {
    let link = createLinkWithDataInputs(true);

    let response = await fetch('/Link/Add/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(link)
    });

    resetErrors();

    if (!response.ok) {
        viewErrors(Object.values(await response.json()));
    } else {
        window.location.href = "/";
    }
}

async function updateLink() {
    let link = createLinkWithDataInputs(false);
    
    let response = await fetch('/Link/Update/', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(link)
    });

    resetErrors();

    if (!response.ok) {
        viewErrors(Object.values(await response.json()));
    } else {
        window.location.href = "/";
    }
}

async function deleteLink() {
    let linkId = getInnerTextOfElement("linkId");
    
    let response = await fetch('/Link/Delete/' + linkId, {
        method: 'DELETE',
    });

    resetErrors();

    if (!response.ok) {
        viewErrors(Object.values(await response.json()));
    } else {
        window.location.href = "/";
    }
}

export {
    deleteLink, updateLink, addLink, resetShortUrlValue, getShortUrl
}