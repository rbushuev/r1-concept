package screens

import (
	"fmt"
	"os/exec"

	"fyne.io/fyne/v2"
	"fyne.io/fyne/v2/container"
	"fyne.io/fyne/v2/widget"
)

func revitScreen(_ fyne.Window) fyne.CanvasObject {
	revitlabel := widget.NewLabelWithStyle("Приложение для R1", fyne.TextAlignCenter, fyne.TextStyle{Bold: true})
	return container.NewCenter(container.NewVBox(
		revitlabel,
		widget.NewButton("Запустить Revit!", func() {
			revitlabel.SetText("запускаем Revit...")
			cmd := exec.Command("cmd.exe", "/C", "start", `C:\\Users\\...\\Desktop\\TestProject.rvt`)
			if err := cmd.Run(); err != nil {
				revitlabel.SetText(fmt.Sprintf("ошибочка запуска: %v", err))
				return
			}
			revitlabel.SetText("запустили!")
		}),
	))
}
