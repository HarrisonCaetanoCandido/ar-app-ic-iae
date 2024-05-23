let http = require('http');

http.createServer((req, res) => {
  res.writeHead(200, {'Content-Type': 'text/text'})
  
  let value1 = Math.random()*100;
  let value2 = Math.random()*100;
  let value3 = Math.random()*100;
  let value4 = Math.random()*100;
  let data = value1.toFixed(1) + "," + value2.toFixed(1) + "," + value3.toFixed(1) + "," + value4.toFixed(1);
  
  res.end(data);
}).listen(8080);
