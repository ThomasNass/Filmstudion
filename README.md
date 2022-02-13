# Filmstudion

F�r att starta applikationen beh�vs .Net 5.
Man kan starta servern via visual studio genom att trycka p� den gr�na pilen, eller genom terminalen via dotnet run.
Jag lyckas inte f� IIS-server att starta p� port 5001, s� jag v�ljer filmstudion.API ist�llet om jag vill k�ra via VS.
Klientgr�nssnittet ligger i wwwroot. P� min maskin s� startas mitt f�rra projekt (RoverAPI) av n�gon m�rklig anledning, men det h�nder f�rhoppningsvis inte p� n�gon annans maskin.
Det g�r fortfarande bra f�r mig att starta klientgr�nssnittet genom att bara �ppna browsern sj�lv och g� till https://localhost:5001/index.html, s� l�nge jag inte anv�nder min standardbrowser.
Det g�r �ven fint att starta klientgr�nssnittet via liveserver i VSC om man har startat servern via terminalen eller via VS.

Klientgr�nssnittet l�ter en icke inloggad anv�ndaren: Registrera sig, logga in, se tillg�ngliga filmer.
En inloggad anv�ndare kan: Se filmer som den inte redan hyr, hyra filmer, se filmer som den f�r n�rvarande hyr, l�mna tillbaka filmer och logga ut.
**Gl�mde bort att jag gjorde �tkomstpunkten rentals n�r jag satt och gjorde klientgr�nssnittet, s� jag h�mtar in filmerna p� ett mycket kr�ngligare s�tt �n n�dv�ndigt.
Rentals funkar finint**

Applikationen anv�nder sig av en inmemorydatabase, s� inget pill med migrations eller dylikt beh�vs. 
Det finns filmer f�rskapade i databasen, men anv�ndare finns ej utan m�ste registreras.
Har s�nkt kraven f�r vad ett l�senord m�ste vara s� att det ska g� smidigt att testa programmet. En siffra eller dylikt duger.

NEDAN F�LJER ANROPEN F�R ATT TESTA I POSTMAN. DET FINNS �VEN EN SWAGGER p� https://localhost:5001/swagger.

POST-ANROP:
---------------------------------
f�r att registrera en anv�ndare:
URL: https://localhost:5001/api/User/register
METOD: POST
body: {
  "userName": "Dadmin",
  "password": "1",
  "isAdmin": true
}
f�r att registerar en filmstudio:
URL: https://localhost:5001/api/FilmStudio/register
METOD: POST
body:{
  "FilmStudioName": "Dff",
  "FilmStudioCity": "Vetlanda",
  "password": "P@ssw0rd!"
}

F�r att authentisera en anv�ndare/filmstudio:(Detta ger tillbaka en token som skall anv�ndas f�r autentisering)
URL: https://localhost:5001/api/User/authenticate
METOD: POST
body:{
  "userName": "Dadmin",
  "password": "1"
}

F�r att hyra en film:
URL: https://localhost:5001/api/films/rent?id=1&studioid=Bff
METOD: POST
(Kr�ver autentisering, som admin eller korrekt filmstudio. Detta �r en query, s� id f�r film och studio anges i url:en. studioid �r samma som anv�ndarnamnet vid registrering)

F�r att l�mna tillbaka en film:
URL: https://localhost:5001/api/films/return?id=1&studioid=Bff
METOD: POST
(Samma som f�r att hyra)
---------------------------------------

GET-ANROP:
--------------------------------------
H�mta alla filmer:
URL: https://localhost:5001/api/Films
METOD: GET
(Om autentisering anv�nds s� h�mtas �ven medf�ljande filmcopies lista)
Det g�r att h�mta enbart en film om man l�gger till "/filmensID" p� slutet

H�mta alla filmstudios:
URL: https://localhost:5001/api/filmstudio/
METOD: GET
(Om autentisering anv�nds s� h�mtas �ven medf�ljande filmcopies-lista)
Det g�r att h�mta enbart en filmstudio om man l�gger till "/filmstudionsID" p� slutet

H�mta alla filmcopies:
URL:https://localhost:5001/api/films/filmcopies
METOD: GET
(Kr�ver autentisering)

H�mta en filmstudios hyrda filmer:
URL: https://localhost:5001/api/mystudio/rentals
METOD: GET
(Kr�ver autentisering, samt att det �r r�tt filmstudio som g�r anropet)
------------------------------------------

PUT-ANROP:
-------------------------------------------
F�r att skapa en film:
URL:https://localhost:5001/api/Films
body {
    "Name":"Filmen",
    "Country": "Sv�je",
    "Director": "Berra",
    "NumberOfCopies": 5
}
(Kr�ver admin och autentisering)

F�r att �ndra antalet kopior av en film:
URL: https://localhost:5001/api/films/1 (det sista �r filmens id)
body {
    "DesiredNumberOfCopies":5
}
(Kr�ver admin och autentisering)
------------------------------------------

PATCH-ANROP:
-----------------------------------------
F�r att �ndra n�got p� en film:
URL: https://localhost:5001/api/films/1 (det sista �r filmens id)
body: [
    {
        "value":"en film till",
        "path": "/name",
        "op": "replace"
    }
]
(Anv�nder mig av JsonPatchDocument f�r att patcha. F�ljde denna https://www.roundthecode.com/dotnet/asp-net-core-web-api/asp-net-core-api-how-to-perform-partial-update-using-http-patch.
op = operation, kan �ndras till olika saker beroende p� vad man vill g�ra.)
(Kr�ver admin och autentisering)
---------------------------------------