# Filmstudion

För att starta applikationen behövs .Net 5.
Man kan starta servern via visual studio genom att trycka på den gröna pilen, eller genom terminalen via dotnet run.
Jag lyckas inte få IIS-server att starta på port 5001, så jag väljer filmstudion.API istället om jag vill köra via VS.
Klientgränssnittet ligger i wwwroot. På min maskin så startas mitt förra projekt (RoverAPI) av någon märklig anledning, men det händer förhoppningsvis inte på någon annans maskin.
Det går fortfarande bra för mig att starta klientgränssnittet genom att bara öppna browsern själv och gå till https://localhost:5001/index.html, så länge jag inte använder min standardbrowser.
Det går även fint att starta klientgränssnittet via liveserver i VSC om man har startat servern via terminalen eller via VS.

Klientgränssnittet låter en icke inloggad användaren: Registrera sig, logga in, se tillgängliga filmer.
En inloggad användare kan: Se filmer som den inte redan hyr, hyra filmer, se filmer som den för närvarande hyr, lämna tillbaka filmer och logga ut.
**Glömde bort att jag gjorde åtkomstpunkten rentals när jag satt och gjorde klientgränssnittet, så jag hämtar in filmerna på ett mycket krångligare sätt än nödvändigt.
Rentals funkar finint**

Applikationen använder sig av en inmemorydatabase, så inget pill med migrations eller dylikt behövs. 
Det finns filmer förskapade i databasen, men användare finns ej utan måste registreras.
Har sänkt kraven för vad ett lösenord måste vara så att det ska gå smidigt att testa programmet. En siffra eller dylikt duger.

NEDAN FÖLJER ANROPEN FÖR ATT TESTA I POSTMAN. DET FINNS ÄVEN EN SWAGGER på https://localhost:5001/swagger.

POST-ANROP:
---------------------------------
för att registrera en användare:
URL: https://localhost:5001/api/User/register
METOD: POST
body: {
  "userName": "Dadmin",
  "password": "1",
  "isAdmin": true
}
för att registerar en filmstudio:
URL: https://localhost:5001/api/FilmStudio/register
METOD: POST
body:{
  "FilmStudioName": "Dff",
  "FilmStudioCity": "Vetlanda",
  "password": "P@ssw0rd!"
}

För att authentisera en användare/filmstudio:(Detta ger tillbaka en token som skall användas för autentisering)
URL: https://localhost:5001/api/User/authenticate
METOD: POST
body:{
  "userName": "Dadmin",
  "password": "1"
}

För att hyra en film:
URL: https://localhost:5001/api/films/rent?id=1&studioid=Bff
METOD: POST
(Kräver autentisering, som admin eller korrekt filmstudio. Detta är en query, så id för film och studio anges i url:en. studioid är samma som användarnamnet vid registrering)

För att lämna tillbaka en film:
URL: https://localhost:5001/api/films/return?id=1&studioid=Bff
METOD: POST
(Samma som för att hyra)
---------------------------------------

GET-ANROP:
--------------------------------------
Hämta alla filmer:
URL: https://localhost:5001/api/Films
METOD: GET
(Om autentisering används så hämtas även medföljande filmcopies lista)
Det går att hämta enbart en film om man lägger till "/filmensID" på slutet

Hämta alla filmstudios:
URL: https://localhost:5001/api/filmstudio/
METOD: GET
(Om autentisering används så hämtas även medföljande filmcopies-lista)
Det går att hämta enbart en filmstudio om man lägger till "/filmstudionsID" på slutet

Hämta alla filmcopies:
URL:https://localhost:5001/api/films/filmcopies
METOD: GET
(Kräver autentisering)

Hämta en filmstudios hyrda filmer:
URL: https://localhost:5001/api/mystudio/rentals
METOD: GET
(Kräver autentisering, samt att det är rätt filmstudio som gör anropet)
------------------------------------------

PUT-ANROP:
-------------------------------------------
För att skapa en film:
URL:https://localhost:5001/api/Films
body {
    "Name":"Filmen",
    "Country": "Sväje",
    "Director": "Berra",
    "NumberOfCopies": 5
}
(Kräver admin och autentisering)

För att ändra antalet kopior av en film:
URL: https://localhost:5001/api/films/1 (det sista är filmens id)
body {
    "DesiredNumberOfCopies":5
}
(Kräver admin och autentisering)
------------------------------------------

PATCH-ANROP:
-----------------------------------------
För att ändra något på en film:
URL: https://localhost:5001/api/films/1 (det sista är filmens id)
body: [
    {
        "value":"en film till",
        "path": "/name",
        "op": "replace"
    }
]
(Använder mig av JsonPatchDocument för att patcha. Följde denna https://www.roundthecode.com/dotnet/asp-net-core-web-api/asp-net-core-api-how-to-perform-partial-update-using-http-patch.
op = operation, kan ändras till olika saker beroende på vad man vill göra.)
(Kräver admin och autentisering)
---------------------------------------