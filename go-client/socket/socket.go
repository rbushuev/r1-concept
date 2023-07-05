package socket

import (
	"context"
	"log"

	"github.com/philippseith/signalr"
)

type receiver struct {
	signalr.Hub
}

func (c *receiver) Received(command string) {
	log.Println(command)
}

func CreateConnection(ctx context.Context) error {

	conn, err := signalr.NewHTTPConnection(ctx, "https://rbushuev.azurewebsites.net/revit/chat")
	if err != nil {
		return err
	}

	a := receiver{}
	client, err := signalr.NewClient(ctx,
		signalr.WithConnection(conn),
		signalr.WithReceiver(a))
	if err != nil {
		return err
	}

	client.Start()

	return nil
}
