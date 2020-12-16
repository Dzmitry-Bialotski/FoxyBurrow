var clientTimeZoneOffset = new Date().getTimezoneOffset();
var options = { year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: 'numeric', second: 'numeric', hour12: true };

const items = document.getElementsByClassName("date-output");
for (let i = 0; i < items.length; i++) {
    var aaaText = items[i].innerText;
    var aaaDate = new Date(aaaText);
    var aaaTime = aaaDate.getTime();
    var date = new Date(aaaTime - clientTimeZoneOffset * 60000).toLocaleString('en-US', options).replace(",", "");
    items[i].innerText = date;
}