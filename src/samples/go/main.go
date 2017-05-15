package main

import (
	"encoding/json"
	"io"
	"net/http"
)

func hello(w http.ResponseWriter, r *http.Request) {
	if r.Method == "POST" {
		decoder := json.NewDecoder(r.Body)
		var c ContactPayload
		decoder.Decode(&c)

		rString := "Hello " + c.FirstName + " " + c.LastName + " from Go!"
		w.WriteHeader(http.StatusCreated)
		r := StringResultPayload{rString}
		encoder := json.NewEncoder(w)
		encoder.Encode(r)
	} else {
		io.WriteString(w, "Hello world from Go!")
	}
}

func main() {
	http.HandleFunc("/", hello)
	println("Listening on *:8000")
	http.ListenAndServe(":8000", nil)
}

type ContactPayload struct {
	FirstName string
	LastName  string
}

type StringResultPayload struct {
	Result string
}
