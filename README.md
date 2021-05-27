# tomek-updater
A C# app that automatically updates a game without user interaction.

# Requirements
Software / Build Tools
- Visual Studio 2019 Community
- .NET Framework 4.7.2

NuGet:
- DotNetZip.Ionic
- Costura.Fody

An HTTP server that has the following files:
- version.txt (this file will be used to compare the game version on disk and the server's version)
- package.zip (if the version.txt file on the server is different compared to the version on disk, the program automatically downloads and extracts this file)

# package.zip file structure
package.zip
- app.zip
- gfx.zip
- sfx.zip

The version.txt file lives in the app.zip file. The version.txt file in the zip has to be exactly the same as version.txt from the server, otherwise the updater will update the game every single time it is ran. This format can be customized to your liking and file structure in the UpdateGame() method - it's just a zip file.

# Other
The server that's currently specified in the code does not work anymore, so replace it with your own (you should've done that either way). I've provided a sample package.zip and version.txt file in the Examples folder in the root directory.

Please don't actually use this in production or anything - this is actually really horribly written. Everything's in a single file and it's really hard to manage. I hope I'll have some time soon to rewrite this.
