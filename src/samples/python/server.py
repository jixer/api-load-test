from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
import json

class HTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.end_headers()
        self.wfile.write("Hello world from Python")
        return
    def do_POST(self):
        len = int(self.headers.getheader("Content-Length", 0))
        body = self.rfile.read(len)
        oBody = json.loads(body)

        sRespMessage = "Hello " + oBody["FirstName"] + " " + oBody["LastName"] + " from Python!"
        oResp = dict(Result=sRespMessage)
        sResp = json.dumps(oResp)

        self.send_response(201)
        self.send_header("Content-Type", "application/json")
        self.end_headers()
        self.wfile.write(sResp)
        return

def main():
    print "Starting HTTP listener on port 8080"
    server = HTTPServer(('', 8080), HTTPRequestHandler)

    try:
        server.serve_forever()
    except KeyboardInterrupt:
        print '^C received, shutting down the web server'
        server.socket.close()

if __name__=="__main__":
    main()