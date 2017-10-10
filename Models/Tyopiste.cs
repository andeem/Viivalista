using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viivalista.lib;

namespace Viivalista.Models
{
    public class Tyopiste
    {
        public int Id { get; set; }
        public String Nimi { get; set; }
        
        public Tyopiste() { }

        public static IEnumerable<Tyopiste> all()
        {
            var tyopisteet = new List<Tyopiste>();

            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM tyopiste", conn))
                {

                    

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tyopisteet.Add(new Tyopiste { Id = (int)reader[0], Nimi = (String)reader[1]});
                    }
                    return tyopisteet;
                }


            }
        }
        
        }
    }

