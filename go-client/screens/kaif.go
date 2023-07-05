package screens

import (
	"fmt"

	"fyne.io/fyne/v2"
	"fyne.io/fyne/v2/container"
	"fyne.io/fyne/v2/widget"
)

func (s *Screens) kaifScreen(_ fyne.Window) fyne.CanvasObject {

	label := widget.NewLabelWithStyle("такс такс такс", fyne.TextAlignCenter, fyne.TextStyle{Bold: true})

	b, err := s.S3Service.GetAllBucket()
	if err != nil {
		label.SetText(fmt.Sprintf("не получилось кайфануть!\nПочему?:%v", err))
	}

	filepath := widget.NewEntry()
	form := &widget.Form{
		SubmitText: "Загрузить на s3 кайф",
		Items: []*widget.FormItem{
			{
				Text:     "",
				Widget:   filepath,
				HintText: "путь",
			}},
		OnSubmit: func() {
			if err := s.S3Service.UploadFileMinio("кайф.jpg", "C:\\Users\\'...'\\Desktop\\photo_2022-08-22_13-00-42.jpg"); err != nil {
				label.SetText(fmt.Sprintf("не получилось кайфануть!\nПочему?:%v", err))
				return
			}
		},
	}

	bpx := container.NewVBox(
		label,
		widget.NewList(
			func() int { return len(b) },
			func() fyne.CanvasObject { return widget.NewLabel("asdsad") },
			func(lii widget.ListItemID, co fyne.CanvasObject) { co.(*widget.Label).SetText(b[lii]) },
		),
		form,
		widget.NewButton("Кайфануть", func() {
			_, err := s.S3Service.DownloadFileMinio()
			if err != nil {
				label.SetText(fmt.Sprintf("не получилось кайфануть!\nПочему?:%v", err))
				return
			}
			label.SetText("еее")
		}),
	)

	return container.NewCenter(bpx)
}
