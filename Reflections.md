REST
---------------
I programmet finns resurserna(klasserna): Film, FilmStudio, FilmCopy och User. Var och en av dem har har ett interface som dikterar 
vilka properties de ska ha.

User och FilmStudio är sammankopplade genom att en user kan ha en FilmStudio, samt ett FilmStudioId. Detta beslutas när en användare
skapas. Om användaren skapas genom filmstudio-controllern så skapas även en filmstudio, men om användaren skapas via user-controllern
så skapas istället en admin. Anledningen till att jag valt att ha det upplagt på det sättet är för jag använder IdentityUser och 
då är lättare att bara ha en användarklass som sedan kan delas in i roller, vilket jag också har gjort. Rollerna underlättar för validering
vilket jag återkommer till under säkerhet.

FilmCopy är som en länk mellan FilmStudio och Film då de båda har en lista för FilmCopies som property. FilmCopy har ett Id för filmcopy,
ett för Film och ett för FilmStudio som i sin tur används för att kunna länka dem samman samt skilja dem åt. 

Programmet använder sig av ett repository-lager för att få tillgång till och arbeta med databasen, samt ett service-lager där metoderna
finns för att via repository-lagret manipulera datan. 

Alla åtkomstpunker och anrops-metoder går att finna i readme.md och är implementerade i enlighet med kravspecifikationen.
Motivering till att de ser ut som de gör och använder de anrops-metoder är för att det är vad kravspecifikationen krävde.
Jag använder PUT när en film ska skapas då PUT skapar/uppdaterar hela resursen den jobbar emot. Hade nog använt POST om det inte var för
Kravspecifikationen dock, i och med att POST inte används till något annat på api/films.
Jag använder Patch för att ändra enstaka properties på en film då det är precis det Patch är bra för.
Det finns 4 controllers; en för Films, en för FilmStudio, en för User och en för MyStudio. 
------------------------

IMPLEMENTATION
---------------------
Förstår inte riktigt vad jag ska svara här. Jag antar att de interna modellerna är mina DTO:er och att de synliga resurserna är det en klient
kan se och göra anrop emot. Men de synliga resurserna levererar ju tillbaka det jag väljer att de ska baserat på mina DTO:er, så jag har lite
svårt för att besvara frågan.


SÄKERHET
--------------------------
Applikationen använder sig av JWT-autentisering. När en användare har registrerat sig så kan den autentisera sig, varpå den får en token
tillbaka i bodyn på anropet. Denna token kan användaren sedan använda om och om igen, så längen tokenen är giltig. På så sätt uppnås en
form av statelessness i och med att servern inte måste hålla ständig koll på om en användare är inloggad. Alla controller använder data-
annoteringen [Authorize], vilket gör att användaren måste vara inloggad med en JWT, så vida [AllowAnonymous] inte är specifiserat på 
anropet användaren försöker göra.I programmets olika controllers finns olika validering beroende på vilket anrop användaren gör. Det finns
get-anrop, som GetFilms, som tillåter vem som helst att kalla på resurserna. De får dock begränsad information genom användningen av
automapping till ett data transfer object som styr så att enbart rätt information ges ut till rätt användare. 
En annan väldigt förekommande valideringsmetod i programmet är att kolla vilken roll användaren har, vilket kan vara admin eller filmstudio.
Den sista förekommande valideringen av användaren är en kontroll av användarens namn från usermanger mot id:n på en filmstudio.
Detta görs för att kontrollera att användaren ifråga verkligen är den samma som filmstudion som användaren försöker påverka via ett anrop.
Utöver det så används kontroll av input för att försäkra att användaren inte försöker komma åt en resurs som inte finns, samt en data-annotation
vid namn [JsonIgnore], för att kunna blockera känslig information vid mappning från en resurs till en model via dto. Detta begränsar då
alltså vilken data som anroparen får tillgång till.

Inloggningen i klientgränssnittet går till på följande sätt att användaren registrerar sig via en post-metod på registreringssidan,
varpå den leds till inloggningssidan. På inloggningssidan får användaren ange användarnamn och lösenord i en post-metod, varpå den får 
tillbaka en token. Tokenen sparas då i sessionstorage via Json.Stringify och hämtas sedan in via Json.Parse på de sidor där den behövs.
Den skickas sedan med som header i fetchanropen. När användaren vill logga ut klickar den på logga ut knappen, varpå användaren tas bort
från sessionstorage.
