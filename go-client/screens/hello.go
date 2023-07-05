package screens

import (
	"net/url"

	"fyne.io/fyne/v2"
	"fyne.io/fyne/v2/container"
	"fyne.io/fyne/v2/widget"
)

func helloScreen(_ fyne.Window) fyne.CanvasObject {
	return container.NewCenter(container.NewVBox(
		widget.NewLabelWithStyle("Приложение для R1", fyne.TextAlignCenter, fyne.TextStyle{Bold: true}),
		widget.NewLabel("R1 Client!"),
		container.NewHBox(
			widget.NewHyperlink("r1", parseURL("https://r1pro.ru/")),
		),
		widget.NewLabel(""),
	))
}

func parseURL(urlStr string) *url.URL {
	link, err := url.Parse(urlStr)
	if err != nil {
		fyne.LogError("Could not parse URL", err)
	}

	return link
}
