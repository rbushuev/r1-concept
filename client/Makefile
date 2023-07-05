
workername?=.NET R1 Daemon

worker-run:
	dotnet run --project .\src\win.worker\win.worker.csproj

worker-publish:
	dotnet publish .\src\win.worker\win.worker.csproj --output "C:\r1"

worker-reload:
	echo "Останавлием службу: $(workername)"
	-Stop-Service -Name "${workername}"
	echo "Удаляем службу: $(workername)"
	-Remove-Service -Name "${workername}"
	echo "Билдим проект: $(workername)"
	dotnet publish .\src\win.worker\win.worker.csproj --output "C:\r1"
	echo "Создаём службу: $(workername)"
	New-Service -Name "${workername}" -BinaryPathName "C:\r1\win.worker.exe" -Description "Тестовая служба для R1 Revit" -DisplayName "${workername}" -StartupType Automatic
	echo "Запускаем службу: $(workername)"
	Start-Service -Name "${workername}"