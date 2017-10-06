using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viivalista.lib;

namespace Viivalista.Models
{
    public class Huomio
    {

        public int Id;
        public String Nimi;
        public String Kuvaus;
        public DateTime Aika;


        public Huomio() { }

        public Huomio(int id, string nimi, string kuvaus, DateTime pvm)
        {
            Id = id;
            Nimi = nimi;
            Kuvaus = kuvaus;
            Aika = pvm;
        }

        public static IEnumerable<Huomio> HaeTyontekijalla(Kayttaja kayttaja)
        {
            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM Huomio WHERE tyontekija_id = @id", conn))
                {
                    command.Parameters.AddWithValue("id", kayttaja.Tyontekija);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    List<Huomio> huomiot = new List<Huomio>();
                    while (reader.Read())
                    {
                        
                        huomiot.Add(new Huomio((int)reader[0], (String)reader[1], (String)reader[2], (DateTime)reader[3]));
                    }
                    return huomiot;
                }


            }
            ;
        }
    }
}
