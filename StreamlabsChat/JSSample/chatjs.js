// Please use event listeners to run functions.
document.addEventListener('onLoad', function (obj) {
	//	 obj will be empty for chat widt
	// this will fire only once when the widget loads
});

document.addEventListener('onEventReceived', function (obj) {	// obj will contain information about the event
	var text = JSON.stringify(obj.detail);
	Send(text);
});

function Send(json) {
	var ip = "localhost";
	var port = 50055;
	var socket = new WebSocket(`ws://${ip}:${port}/`);
	socket.sendction(event) {
		socket.send(json);
		socket.close();
	};
}