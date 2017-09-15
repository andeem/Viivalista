CREATE TABLE Tyontekija (
	id INTEGER NOT NULL,
	Nimi VARCHAR(30),
	Tyontekijaryhma VARCHAR(30),
	PRIMARY KEY (id)
);

CREATE TABLE Tyopiste(
	id INTEGER NOT NULL,
	Nimi VARCHAR(30),
	PRIMARY KEY (id)
);
CREATE TABLE Luvat(
	Tyontekija_id INTEGER,
	Tyopiste_id INTEGER,
	PRIMARY KEY(Tyontekija_id, Tyopiste_id),
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija,
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija
);
CREATE TABLE Huomio(
	id INTEGER,
	Nimi VARCHAR(30),
	Kuvaus VARCHAR(200),
	Pvm DATE,
	Aika DATE,
	Tyontekija_id INTEGER,
	PRIMARY KEY (id),
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija
);
CREATE TABLE Vuoro(
	id INTEGER,
	Pvm DATE,
	Alkuaika TIME,
	Loppuaika TIME,
	Tyontekija_id INTEGER,
	Tyopiste_id INTEGER,
	PRIMARY KEY (ID),
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija,
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija
);