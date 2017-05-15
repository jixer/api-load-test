var connect = require('connect');
var http = require('http');

var app = connect();

// // gzip/deflate outgoing responses
// var compression = require('compression');
// app.use(compression());

// store session state in browser cookie
// var cookieSession = require('cookie-session');
// app.use(cookieSession({
//     keys: ['secret1', 'secret2']
// }));

// parse urlencoded request bodies into req.body
 var bodyParser = require('body-parser');
 app.use(bodyParser.json({}));

// respond to all requests
app.use(function(req, res){
  if (req.method == "POST") {
    var data = req.body;
    var rString = "Hello " + data.FirstName + " " + data.LastName + " from Node.JS!";
    var r = { Result: rString };
    res.writeHead(201, {'Content-Type': 'application/json'});
    res.end(JSON.stringify(r));
  }
  else {
    res.end('Hello World from Node.JS!\n');
  }
});

//create node.js http server and listen on port
http.createServer(app).listen(3000);
console.log("Running on port 3000");