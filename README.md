# tomek-updater
c# updater programme 

# wymagania:

visual studio 2019

nuget:
DotNetZip/ionic
costura.fody

1x serwer
na ktorym jest
version.txt (wersja do sprawdzania z lokalna)
package.zip (jesli jest nowa wersja updater pobiera i wypakowywuje)

# struktura pliku package.zip, plik version.txt

package.zip
- app.zip
- gfx.zip
- sfx.zip

w app.zip znajduje sie plik version.txt ktory musi byc taki sam jak version.txt na serwerze

format pliku package.zip mozna zmienic w UpdateGame(); ale wazne zeby zachowac plik version.txt
