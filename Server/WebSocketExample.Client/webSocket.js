//Amennyiben itt hiba lépne fel, az a szerver oldali hiba miatt van, mivel a szerver oldalon futó websocket szerver nem tudja kiszolgálni a kérést
const socket = new WebSocket("wss://localhost:7062/ws");//létrehozunk egy socketet, ami a szerverrel fog kommunikálni

socket.onopen = (event) => {//létrehozunk egy eventet, ami a szerver válaszát fogja tartalmazni, amit a szerver küldött csatlakozáskor
    //console.log("WebSocket connection established.");
};

socket.onmessage = (event) => {//létrehozunk egy eventet, ami a szerver válaszát fogja tartalmazni, amit a szerver küldött
    console.log(event.data)
    let webSocketResponse = document.getElementById("webSocketResponse");
    webSocketResponse.innerHTML = `Ezt kaptam a szervertől: ${event.data}`;
};

socket.onclose = (event) => {//létrehozunk egy eventet, ami a szerver válaszát fogja tartalmazni, amit a szerver küldött lecsatlakozáskor
    if (event.wasClean) {
        console.log(`WebSocket connection closed cleanly, code=${event.code}, reason=${event.reason}`);
    } else {
        console.error(`WebSocket connection died`);
    }
};

function sendMessage(message) {//létrehozunk egy függvényt, ami elküldi a szervernek a paraméterként kapott üzenetet
    socket.send(message);
}