CREATE TABLE Tyontekija (
	id SERIAL NOT NULL,
	Nimi VARCHAR(30),
	Tyontekijaryhma VARCHAR(30),
	PRIMARY KEY (id)
);

CREATE TABLE Tyopiste(
	id SERIAL NOT NULL,
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
	id SERIAL,
	Nimi VARCHAR(30),
	Kuvaus VARCHAR(200),
	Aika TIMESTAMP,
	Tyontekija_id INTEGER,
	PRIMARY KEY (id),
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija
);
CREATE TABLE Vuoro(
	id SERIAL,
	Pvm DATE,
	Alkuaika TIME,
	Loppuaika TIME,
	Tyontekija_id INTEGER,
	Tyopiste_id INTEGER,
	PRIMARY KEY (ID),
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija,
	FOREIGN KEY (Tyontekija_id) REFERENCES Tyontekija
);
CREATE TABLE Kayttaja(
	id SERIAL PRIMARY KEY,
	kayttajatunnus VARCHAR,
	salasana VARCHAR,
	tyontekija INT,
	esimies BOOLEAN,
	FOREIGN KEY (tyontekija) REFERENCES Tyontekija
	);
