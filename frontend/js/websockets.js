const serverURL = 'ws://html5rocks.websocket.org/echo';
const connection = new WebSocket(serverURL);

// When the connection is open, send some data to the server
connection.onopen = function () {
    console.log("Connected to WS server: " + serverURL);
    connection.send('Ping'); // Send the message 'Ping' to the server
};

// Log errors
connection.onerror = function (error) {
    console.log('WebSocket Error ' + error);
};

// Log messages from the server
connection.onmessage = function (e) {
    console.log('Server: ' + e.data);
};