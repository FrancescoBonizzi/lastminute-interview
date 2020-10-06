function loadTemplate(rawJson) {

    // JSON parse and input data validity assertions in order to identify quickly eventual errors
    var parsedData = parseData(rawJson);
    assertParsedDataValidity(parsedData);

    // Header
    setOrHideIfEmpty(parsedData.receiptCode, 'receiptCode', 'receiptCodeField');
    setOrHideIfEmpty(parsedData.formattedCreationDate, 'creationDate', 'creationDateField');

    // BilledTo
    setOrHideIfEmpty(parsedData.customer.name, 'customerName', 'customerName');
    setOrHideIfEmpty(parsedData.customer.address, 'customerAddress', 'customerAddress');
    setOrHideIfEmptyWithLink(parsedData.customer.email, 'customerEmail', 'customerEmailField', `mailto:${parsedData.customer.email}`);

    // BilledBy
    setOrHideIfEmpty(parsedData.company.name, 'companyName', 'companyName');
    setOrHideIfEmpty(parsedData.company.holderName, 'holderName', 'holderName');
    setOrHideIfEmpty(parsedData.company.address, 'address', 'address');
    setOrHideIfEmptyWithLink(parsedData.company.email, 'email', 'emailField', `mailto:${parsedData.company.email}`);
    setOrHideIfEmpty(parsedData.company.phone, 'phone', 'phoneField');
    setOrHideIfEmptyWithLink(parsedData.company.webSite, 'webSite', 'webSiteField', parsedData.company.webSite);

    // Cart item articles
    parsedData.articles.reverse().forEach(a => insertArticle(a));

    // Totals
    setOrHideIfEmpty(parsedData.formattedTaxesAmount, 'taxesValue', 'taxesField');
    setOrHideIfEmpty(parsedData.formattedTotalAmount, 'totalValue', 'totalField');
}

function setOrHideIfEmpty(value, propertyForSet, propertyForHide) {

    if (value) {
        document.getElementById(propertyForSet).innerText = value;
    } else {
        document.getElementById(propertyForHide).style = 'display: none';
    }

}

function setOrHideIfEmptyWithLink(value, propertyForSet, propertyForHide, link) {

    if (value) {
        document.getElementById(propertyForSet).innerText = value;
        document.getElementById(propertyForSet).href = link;
    } else {
        document.getElementById(propertyForHide).style = 'display: none';
    }

}

function insertArticle(article) {

    var table = document.getElementById("articles-table");

    // 1: after table header row
    var row = table.tBodies[0].insertRow(0);

    var name = row.insertCell(0);
    var quantity = row.insertCell(1);
    var formattedTaxes = row.insertCell(2);
    var formattedPrice = row.insertCell(3);
    
    name.classList.add('article-item-td');
    name.innerHTML = article.name;

    quantity.classList.add('articles-td');
    quantity.innerHTML = article.quantity;

    formattedTaxes.classList.add('articles-td');
    formattedTaxes.innerHTML = article.formattedTaxes;

    formattedPrice.classList.add('articles-td');
    formattedPrice.innerHTML = article.formattedPrice;
}

function appendValue(property, valueToAppend) {
    var control = document.getElementById(property);
    control.innerHTML = control.innerHTML + valueToAppend;
}

function parseData(rawJson) {

    if (rawJson.includes('#SHOPPING_CART_JSON_SUBSTITUTION#')) {
        throw '#SHOPPING_CART_JSON_SUBSTITUTION# has not been substituted!';
    }

    var parsedData;

    try {
        parsedData = JSON.parse(rawJson);
    } catch (error) {
        throw 'rawJson parse error: ' + error
    }   

    return parsedData;
}

function assertParsedDataValidity(parsedData) {

    if (!parsedData) {
        throw 'rawJson deserialization error. It is null or empty.'
    } 
    
    if (!parsedData.customer) {
        throw "Receipt: customer object not present";
    } 
    
    if (!parsedData.company) {
        throw "Receipt: billing company object not present";
    } 
    
    if (!parsedData.articles || parsedData.articles.length === 0) {
        throw "Receipt: no articles found";
    } 

}
