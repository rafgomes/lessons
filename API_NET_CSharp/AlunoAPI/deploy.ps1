iisreset /stop
dotnet build -c release -o C:\Temp\Build
dotnet publish -c Release -o C:\publish\output
try {
    New-WebSite -Name "MyApp" -PhysicalPath "C:\publish\output" -Port 80 -Force
}
catch {
    Write-Host "Webite Já Existe"
}

iisreset /start