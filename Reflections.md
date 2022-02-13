REST
---------------
I programmet finns resurserna(klasserna): Film, FilmStudio, FilmCopy och User. Var och en av dem har har ett interface som dikterar 
vilka properties de ska ha.

User och FilmStudio �r sammankopplade genom att en user kan ha en FilmStudio, samt ett FilmStudioId. Detta beslutas n�r en anv�ndare
skapas. Om anv�ndaren skapas genom filmstudio-controllern s� skapas �ven en filmstudio, men om anv�ndaren skapas via user-controllern
s� skapas ist�llet en admin. Anledningen till att jag valt att ha det upplagt p� det s�ttet �r f�r jag anv�nder IdentityUser och 
d� �r l�ttare att bara ha en anv�ndarklass som sedan kan delas in i roller, vilket jag ocks� har gjort. Rollerna underl�ttar f�r validering
vilket jag �terkommer till under s�kerhet.

FilmCopy �r som en l�nk mellan FilmStudio och Film d� de b�da har en lista f�r FilmCopies som property. FilmCopy har ett Id f�r filmcopy,
ett f�r Film och ett f�r FilmStudio som i sin tur anv�nds f�r att kunna l�nka dem samman samt skilja dem �t. 

Programmet anv�nder sig av ett repository-lager f�r att f� tillg�ng till och arbeta med databasen, samt ett service-lager d�r metoderna
finns f�r att via repository-lagret manipulera datan. 

Alla �tkomstpunker och anrops-metoder g�r att finna i readme.md och �r implementerade i enlighet med kravspecifikationen.
Motivering till att de ser ut som de g�r och anv�nder de anrops-metoder �r f�r att det �r vad kravspecifikationen kr�vde.
Jag anv�nder PUT n�r en film ska skapas d� PUT skapar/uppdaterar hela resursen den jobbar emot. Hade nog anv�nt POST om det inte var f�r
Kravspecifikationen dock, i och med att POST inte anv�nds till n�got annat p� api/films.
Jag anv�nder Patch f�r att �ndra enstaka properties p� en film d� det �r precis det Patch �r bra f�r.
Det finns 4 controllers; en f�r Films, en f�r FilmStudio, en f�r User och en f�r MyStudio. 
------------------------

IMPLEMENTATION
---------------------
F�rst�r inte riktigt vad jag ska svara h�r. Jag antar att de interna modellerna �r mina DTO:er och att de synliga resurserna �r det en klient
kan se och g�ra anrop emot. Men de synliga resurserna levererar ju tillbaka det jag v�ljer att de ska baserat p� mina DTO:er, s� jag har lite
sv�rt f�r att besvara fr�gan.


S�KERHET
--------------------------
Applikationen anv�nder sig av JWT-autentisering. N�r en anv�ndare har registrerat sig s� kan den autentisera sig, varp� den f�r en token
tillbaka i bodyn p� anropet. Denna token kan anv�ndaren sedan anv�nda om och om igen, s� l�ngen tokenen �r giltig. P� s� s�tt uppn�s en
form av statelessness i och med att servern inte m�ste h�lla st�ndig koll p� om en anv�ndare �r inloggad. Alla controller anv�nder data-
annoteringen [Authorize], vilket g�r att anv�ndaren m�ste vara inloggad med en JWT, s� vida [AllowAnonymous] inte �r specifiserat p� 
anropet anv�ndaren f�rs�ker g�ra.I programmets olika controllers finns olika validering beroende p� vilket anrop anv�ndaren g�r. Det finns
get-anrop, som GetFilms, som till�ter vem som helst att kalla p� resurserna. De f�r dock begr�nsad information genom anv�ndningen av
automapping till ett data transfer object som styr s� att enbart r�tt information ges ut till r�tt anv�ndare. 
En annan v�ldigt f�rekommande valideringsmetod i programmet �r att kolla vilken roll anv�ndaren har, vilket kan vara admin eller filmstudio.
Den sista f�rekommande valideringen av anv�ndaren �r en kontroll av anv�ndarens namn fr�n usermanger mot id:n p� en filmstudio.
Detta g�rs f�r att kontrollera att anv�ndaren ifr�ga verkligen �r den samma som filmstudion som anv�ndaren f�rs�ker p�verka via ett anrop.
Ut�ver det s� anv�nds kontroll av input f�r att f�rs�kra att anv�ndaren inte f�rs�ker komma �t en resurs som inte finns, samt en data-annotation
vid namn [JsonIgnore], f�r att kunna blockera k�nslig information vid mappning fr�n en resurs till en model via dto. Detta begr�nsar d�
allts� vilken data som anroparen f�r tillg�ng till.

Inloggningen i klientgr�nssnittet g�r till p� f�ljande s�tt att anv�ndaren registrerar sig via en post-metod p� registreringssidan,
varp� den leds till inloggningssidan. P� inloggningssidan f�r anv�ndaren ange anv�ndarnamn och l�senord i en post-metod, varp� den f�r 
tillbaka en token. Tokenen sparas d� i sessionstorage via Json.Stringify och h�mtas sedan in via Json.Parse p� de sidor d�r den beh�vs.
Den skickas sedan med som header i fetchanropen. N�r anv�ndaren vill logga ut klickar den p� logga ut knappen, varp� anv�ndaren tas bort
fr�n sessionstorage.
