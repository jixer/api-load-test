from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer

class HTTPRequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        self.send_response(200)
        self.end_headers()
        self.wfile.write("Hello world from Python")
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