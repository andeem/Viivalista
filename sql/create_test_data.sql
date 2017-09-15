INSERT INTO Tyontekija (id, nimi, tyontekijaryhma) VALUES (1, 'Matti Meik‰l‰inen', 'Tuotanto');
INSERT INTO Tyontekija (id, nimi, tyontekijaryhma) VALUES (2, 'Maija Meik‰l‰inen', 'Tuotanto');
INSERT INTO Tyopiste VALUES (1, 'Kone 1');
INSERT INTO Luvat VALUES (1, 1);
INSERT INTO Huomio (id, nimi, kuvaus, Pvm, Tyontekija_id) VALUES (1, 'L‰‰k‰ri', 'K‰ynti urologilla', '2017-10-01', 1);
INSERT INTO Vuoro(id, pvm, alkuaika, loppuaika, tyontekija_id, tyopiste_id) VALUES (1, '2017-10-01', '08:00:00', '12:00:00', 1, 1);