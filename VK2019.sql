CREATE DATABASE VideoKlub2019
COLLATE Serbian_Latin_100_CI_AI
GO

USE VideoKlub2019
GO

CREATE TABLE Zanr
(
ZanrId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
NazivZanra nvarchar(50)  NOT NULL
);

INSERT INTO Zanr VALUES('Akcija');
INSERT INTO Zanr VALUES('Avantura');
INSERT INTO Zanr VALUES('Drama');
INSERT INTO Zanr VALUES('Istorijski');
INSERT INTO Zanr VALUES('Komedija');
INSERT INTO Zanr VALUES('Ratni');
INSERT INTO Zanr VALUES('Triler');
INSERT INTO Zanr VALUES('Animirani');
INSERT INTO Zanr VALUES('Fantastika');

SELECT * FROM Zanr;


CREATE TABLE Film(
	FilmId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Naziv nvarchar(100)  NOT NULL,	
	ZanrId int  NOT NULL FOREIGN KEY REFERENCES Zanr(ZanrId),
	Reziser nvarchar(100) NULL,
	Godina int NOT NULL
 );

 INSERT INTO  FILM VALUES
('Pisma sa Iwo Jime', 6, 'Klint Istvud', 2006)

INSERT INTO  FILM VALUES
('Zastave nasih oceva', 6, 'Klint Istvud', 2006)

INSERT INTO  FILM VALUES
( 'Gladijator', 4, 'Ridli Skot', 2000)

INSERT INTO  FILM VALUES
('Titanik', 3, 'Džejms Kamerun', 1998)

INSERT INTO  FILM VALUES
('Beogradski fantom', 3, 'Jovan Todorovic', 2009)

INSERT INTO  FILM VALUES
( 'Wicker park', 7, 'Paul McGuigan', 2004)

INSERT INTO  FILM VALUES
( 'Troja', 1, 'Paul McGuigan', 2004)

INSERT INTO  FILM VALUES
('Forest Gamp', 5, 'Robert Zemeckis', 1994)

INSERT INTO  FILM VALUES
( '10000 BC', 2, 'Roland Emerih', 2008)

 INSERT INTO Film VALUES
 ('The Hangover Part III',5,'Todd Phillips',2013);

INSERT INTO Film VALUES
('Mission Impossible: Ghost Protocol',7,' Brad Bird',2011);

 INSERT INTO Film VALUES
 ('Lepa sela lepo gore',6,' Srdjan Dragojevic',1996);

 INSERT INTO Film VALUES('Madagascar 3',8,'Tom McGrath',2012);

 INSERT INTO Film VALUES('Svet iz doba Jure',9,' Colin Trevorrow',2015);


 INSERT INTO Film VALUES('Falsifikator',5,' Goran Markovic',2013);

 SELECT * FROM Film


CREATE TABLE Clan(
	ClanId int IDENTITY(1,1) NOT NULL PRIMARY KEY,	
	Ime nvarchar(50)  NOT NULL,
	Prezime nvarchar(50)  NOT NULL,	
	LicnaKarta char(9) NOT NULL,
    UlicaBroj nvarchar(30) NOT NULL,
	Mesto nvarchar(50) NOT NULL DEFAULT 'Beograd'
);

INSERT INTO Clan VALUES
( 'Nebojsa','Nikolic', '003322211', 'Novogradska 12', DEFAULT)

INSERT INTO Clan VALUES
('Milena','Maric', '007229213', 'Cara Dusana 22', DEFAULT)

INSERT INTO Clan VALUES
('Petar', 'Peric', '002234323', 'Prvomajska 7', DEFAULT)

SELECT * FROM Clan

CREATE TABLE Iznajmljivanje(
	IznajmljivanjeId int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FilmId int NOT NULL FOREIGN KEY REFERENCES Film(FilmId),
	ClanId int NOT NULL FOREIGN KEY REFERENCES Clan(ClanId),
	DatumIznajmljivanja date NOT NULL,
	DatumVracanja date NULL,
	CenaPoDanu  decimal(6, 2) NULL
	)
 
 GO

 INSERT INTO Iznajmljivanje VALUES
 (1, 1, '2019-01-20', '2019-01-21', 100)

INSERT INTO Iznajmljivanje VALUES

(2, 1, '2019-01-20', '2019-01-22', 100)


INSERT INTO Iznajmljivanje VALUES
(3, 2, '2018-05-14', NULL, NULL)


SELECT * FROM Iznajmljivanje

GO
CREATE VIEW View_Iznajmljivanja
AS
SELECT iz.IznajmljivanjeId, f.Naziv, c.Ime, c.Prezime, iz.DatumIznajmljivanja, iz.DatumVracanja, iz.CenaPoDanu
FROM  Clan as c INNER JOIN  Iznajmljivanje as iz
ON c.ClanId = iz.ClanId
INNER JOIN  Film as f 
ON iz.FilmId = f.FilmId

GO
SELECT * FROM View_Iznajmljivanja
                        

