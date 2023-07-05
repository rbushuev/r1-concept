package main

import (
	"log"
	"r1/client/s3"
	"r1/client/screens"

	"fyne.io/fyne/v2"
	"fyne.io/fyne/v2/app"
	"fyne.io/fyne/v2/container"
	"fyne.io/fyne/v2/driver/desktop"
	"fyne.io/fyne/v2/theme"
	"fyne.io/fyne/v2/widget"
)

const preferenceCurrentScreen = "currentScreen"

var topWindow fyne.Window

func main() {
	a := app.NewWithID("r1.app")
	w := a.NewWindow("Main Window")
	makeTray(a, w)
	logLifecycle(a)
	topWindow = w

	content := container.NewMax()
	title := widget.NewLabel("Название страницы")
	intro := widget.NewLabel("Приветики")
	isConnected := widget.NewLabel("Нет связи с облаком...")
	intro.Wrapping = fyne.TextWrapWord

	setScreen := func(t screens.Screen) {
		title.SetText(t.Title)
		intro.SetText(t.Intro)
		content.Objects = []fyne.CanvasObject{t.View(w)}
		content.Refresh()
	}

	// if err := socket.CreateConnection(ctx); err != nil {
	// 	isConnected.SetText(fmt.Sprintf("Нет связи с сервером: %v", err))
	// }

	s3 := s3.NewS3Service()

	screenObj := screens.NewScreensConstructor(s3)

	cscreen := container.NewBorder(container.NewVBox(container.NewGridWithColumns(2, title, isConnected), widget.NewSeparator()), container.NewVBox(container.NewGridWithColumns(2, widget.NewLabel("."), widget.NewLabel("."))), widget.NewLabel("."), widget.NewLabel("."), content)

	split := container.NewHSplit(makeNav(setScreen, screenObj), cscreen)
	split.Offset = 0.2
	w.SetContent(split)

	w.SetCloseIntercept(func() {
		w.Hide()
	})
	w.Resize(fyne.NewSize(640, 460))
	w.ShowAndRun()
}

func logLifecycle(a fyne.App) {
	a.Lifecycle().SetOnStarted(func() {
		log.Println("Lifecycle: Started")
	})
	a.Lifecycle().SetOnStopped(func() {
		log.Println("Lifecycle: Stopped")
	})
	a.Lifecycle().SetOnEnteredForeground(func() {
		log.Println("Lifecycle: Entered Foreground")
	})
	a.Lifecycle().SetOnExitedForeground(func() {
		log.Println("Lifecycle: Exited Foreground")
	})
}

func makeNav(setScreens func(screens screens.Screen), sobj *screens.Screens) fyne.CanvasObject {
	a := fyne.CurrentApp()

	tree := &widget.Tree{
		ChildUIDs: func(uid string) []string {
			return sobj.ScreenIndex[uid]
		},
		IsBranch: func(uid string) bool {
			children, ok := sobj.ScreenIndex[uid]
			return ok && len(children) > 0
		},
		CreateNode: func(branch bool) fyne.CanvasObject {
			return widget.NewLabel("Collection Widgets")
		},
		UpdateNode: func(uid string, branch bool, obj fyne.CanvasObject) {
			t, ok := sobj.Screens[uid]
			if !ok {
				fyne.LogError("Не нашли страницу?: "+uid, nil)
				return
			}
			obj.(*widget.Label).SetText(t.Title)
			obj.(*widget.Label).TextStyle = fyne.TextStyle{}
		},
		OnSelected: func(uid string) {
			if t, ok := sobj.Screens[uid]; ok {
				a.Preferences().SetString(preferenceCurrentScreen, uid)
				setScreens(t)
			}
		},
	}

	currentPref := a.Preferences().StringWithFallback(preferenceCurrentScreen, "hello")
	tree.Select(currentPref)

	themes := container.NewGridWithColumns(2,
		widget.NewButton("Dark", func() {
			a.Settings().SetTheme(theme.DarkTheme())
		}),
		widget.NewButton("Light", func() {
			a.Settings().SetTheme(theme.LightTheme())
		}),
	)

	return container.NewBorder(nil, themes, nil, nil, tree)
}

func makeTray(a fyne.App, w fyne.Window) {
	if desk, ok := a.(desktop.App); ok {
		h := fyne.NewMenuItem("Hello", func() {})
		h.Icon = theme.HomeIcon()
		menu := fyne.NewMenu("Hello World", h)
		h.Action = func() {
			h.Label = "Open Window"
			w.Show()
		}
		desk.SetSystemTrayMenu(menu)
	}
}
