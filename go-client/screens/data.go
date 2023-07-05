package screens

import (
	"r1/client/s3"

	"fyne.io/fyne/v2"
)

type (
	Screens struct {
		Screens     map[string]Screen
		ScreenIndex map[string][]string
		S3Service   *s3.S3Service
	}

	Screen struct {
		Title, Intro string
		View         func(w fyne.Window) fyne.CanvasObject
	}
)

func NewScreensConstructor(s3 *s3.S3Service) *Screens {

	a := &Screens{
		S3Service: s3,
	}

	screens := map[string]Screen{
		"hello": {"Hello", "", helloScreen},
		"revit": {"Revit Page", "", revitScreen},
		"кайф":  {"Кайф", "", a.kaifScreen},
	}

	screenIndex := map[string][]string{
		"": {"hello", "revit", "кайф"},
	}

	a.Screens = screens
	a.ScreenIndex = screenIndex

	return a
}
