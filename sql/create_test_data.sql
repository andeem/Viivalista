INSERT INTO Tyontekija (nimi, tyontekijaryhma) VALUES ('Matti Meik‰l‰inen', 'Tuotanto');
INSERT INTO Tyontekija (nimi, tyontekijaryhma) VALUES ('Maija Meik‰l‰inen', 'Tuotanto');
INSERT INTO Tyopiste (nimi) VALUES ('Kone');
INSERT INTO Tyopiste (nimi) VALUES ('Tarjotin');
INSERT INTO Tyopiste (nimi) VALUES ('Tarkastus');
INSERT INTO Luvat VALUES (1, 1);
INSERT INTO Luvat VALUES (1, 2);
INSERT INTO Luvat VALUES (1, 3);
INSERT INTO Luvat VALUES (2, 1);
INSERT INTO Luvat VALUES (2, 2);
INSERT INTO Huomio (nimi, kuvaus, Aika, Tyontekija_id) VALUES ('L‰‰k‰ri', 'K‰ynti urologilla', '2017-10-01 10:00:00', 1);
INSERT INTO Vuoro(pvm, alkuaika, loppuaika, tyontekija_id, tyopiste_id) VALUES ('2017-10-01', '11:00:00', '12:00:00', 1, 1);
INSERT INTO Kayttaja(kayttajatunnus, tyontekija, esimies, salasana) VALUES ('matmei', 1, TRUE, '7446eeeef5c2d0b985b20c24f04c64ed7572aef33cef2eb8b7b25489a7438ac5');
INSERT INTO Kayttaja(kayttajatunnus, tyontekija, esimies, salasana) VALUES ('maimei', 2, FALSE, '7446eeeef5c2d0b985b20c24f04c64ed7572aef33cef2eb8b7b25489a7438ac5')