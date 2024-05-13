// Check if there's an existing WebSocket connection stored in sessionStorage
let socketUrl = sessionStorage.getItem('socketUrl');
let socket;
let storedValue = localStorage.getItem('pTagValue');

var numberFromCSHTML = @Html.Raw(Json.Serialize(ViewBag.Number));
document.getElementById("asd").innerHTML = numberFromCSHTML;
// If a value exists in localStorage, set the <p> tag text to it
if (storedValue) {
    document.getElementById('webSocketResponse').innerHTML = storedValue;
}
console.log("yeah");
if (socketUrl) {
    socket = new WebSocket(socketUrl); // Reuse the existing WebSocket connection
} else {
    socket = new WebSocket("wss://localhost:7062/ws"); // Create a new WebSocket connection
}

// Store the WebSocket connection URL in sessionStorage
sessionStorage.setItem('socketUrl', socket.url);

// Add event listeners
socket.onopen = (event) => {
    console.log("WebSocket connection established.");
};

socket.onmessage = (event) => {
    console.log(event.data)
    let webSocketResponse = document.getElementById("webSocketResponse");
    webSocketResponse.innerHTML = `Ezt kaptam a szervertől: ${event.data}`;
    localStorage.setItem('pTagValue', newValue);
    console.log("yeah");
};

socket.onclose = (event) => {
    if (event.wasClean) {
        console.log(`WebSocket connection closed cleanly, code=${event.code}, reason=${event.reason}`);
    } else {
        console.error(`WebSocket connection died`);
    }
};
console.log("yeah");
function sendMessage(message) {
    socket.send(message);
}

