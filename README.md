# NTR21Z-Okrutny-Aleksandra

Projekt realizuje system raportowania czasu pracy. 
Projekt umożliwia raportowanie czasu własnej pracy oraz nadzorowanie i zatwierdzanie pracy użytkowników, którzy dodawali aktywność w projektach należacyhc do użytkownika. 

**Zakres**:
- Części 1-3 zrealizowana w ASP.NET MVC. 
	Część 1 odczytuje dane z plików .json. 
	Część 2+3 odczytuje dane z bazy i implementuje równoległość.

- Część 4 zrealizowana w React.js - hooks i ASP.NET (serwer) - część zawiera wszystkie funkcjonalności realizowane w częsciach 1-3. 

**Uruchomienie**:
- Części 1-3 można uruchomić przy pomocy polecenia "dotnet run" uruchomionego z głównego katalogu projektu. 
- Część 4 - serwer uruchamiamy przy pomocy polecenia "dotnet run", a frontend kolejno przy użyciu poleceń "npm install" i "npm start" - aplikacja uruchomiona jest na porcie 3000.

**Uruchomienie w trybie produkcyjnym**:
- Serwer dotnet - z głównego katalogu należy wykonać polecenie "dotnet publish", następnie przejść do katalogu publish (w katalogu bin) i uruchomić komendę "./ntr-mysqlDatabase "
- Frontend - z głównego katalogu wykonujemy kolejno "npm run build", "npm install -g serve" i "serve -s build". - aplikacja zostaje uruchomiona na porcie 3000. 

Części korzystające z bazy danych skonfigurowane są z bazą MySql w wersji 8.0.27, serwer c# wykorzystuje ASP.NET Core 5.0.404, a frontend został zbudowany w node w wersji 14.18.3.

