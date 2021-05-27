# tomek-updater
A C# app that automatically updates a game without user interaction.

# Requirements
- Visual Studio 2019 Community

Nuget:
- DotNetZip.Ionic
- Costura.Fody

1 HTTP server that has the following files:
- version.txt (this file will be used to compare the game version on disk and the server's version)
- package.zip (if the version.txt file on the server is different compared to the version on disk, the program automatically downloads and extracts this file)

# package.zip file structure

package.zip
- app.zip
- gfx.zip
- sfx.zip

The version.txt file lives in the app.zip file. The version.txt file in the zip has to be exactly the same as version.txt from the server, otherwise the updater will update the game every single time it is ran. This format can be customized to your liking and file structure in the UpdateGame() method - it's just a zip file.
