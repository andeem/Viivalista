using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viivalista.lib;

namespace Viivalista.Models
{
    public class Vuoro
    {
        public int Id { get; set; }
        public DateTime Alku { get; set; }
        public DateTime Loppu { get; set; }
        public int Tyontekija { get; set; }
        public int Tyopiste { get; set; }
        public String TyopisteNimi { get; set; }

        public Vuoro() { }

        public static IEnumerable<Vuoro> all()
        {
            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM Vuoro v INNER JOIN tyopiste t on v.tyopiste_id = t.id", conn))
                {
                    var reader = command.ExecuteReader();
                    List<Vuoro> vuorot = new List<Vuoro>();
                    while (reader.Read())
                    {
                        vuorot.Add(new Vuoro() { Id = (int)reader[0],
                                                    Alku = (DateTime)reader[1] + (TimeSpan)reader[2],
                                                 Loppu = (DateTime)reader[1] + (TimeSpan)reader[3],
                                                 Tyontekija = (int)reader[4],
                                                 Tyopiste = (int)reader[5],
                                                 TyopisteNimi = (string)reader[7]
                                                 
                        });
                    }
                    return vuorot;
                }
            }
        }
        public void save()
        {
            if (Tyontekija != 0 && Tyopiste != 0)
            {


                using (var conn = Database.connection())
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand("INSERT INTO Vuoro (pvm, alkuaika, loppuaika, tyopiste_id, tyontekija_id) VALUES (@pvm, @alku, @loppu, @tt, @tp)", conn))
                    {
                        command.Parameters.AddWithValue("pvm", this.Alku.Date);
                        command.Parameters.AddWithValue("alku", this.Alku.TimeOfDay);
                        command.Parameters.AddWithValue("loppu", this.Loppu.TimeOfDay);
                        command.Parameters.AddWithValue("tt", this.Tyontekija);
                        command.Parameters.AddWithValue("tp", this.Tyopiste);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }


    }
}
