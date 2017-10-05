using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Viivalista.lib;

namespace Viivalista.Models
{
    public class Kayttaja
    {
        public int Id { get; }
        public string Kayttajatunnus { get; set; }
        public string Salasana { get; set; }
        public int Tyontekija { get; set; }
        public bool Esimies { get; set; }

        public Kayttaja() { }

        public Kayttaja(int id, string ktunnus, string ssana, int tid, bool esimies)
        {
            this.Id = id;
            this.Kayttajatunnus = ktunnus;
            this.Salasana = ssana;
            this.Tyontekija = tid;
            this.Esimies = esimies;
        }

        public static Kayttaja get(string ktunnus, string ssana)
        {
            try
            {
                using (var conn = Database.connection())
                {
                    string salasana;
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT * FROM kayttaja WHERE kayttajatunnus = @ktunnus AND salasana = @ssana", conn))
                    {
                        using (var sha256 = SHA256.Create())
                        {
                            
                            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ssana));
                              
                            salasana = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                        }
                        command.Parameters.AddWithValue("ktunnus", ktunnus);

                        command.Parameters.AddWithValue("ssana", salasana);

                        NpgsqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        return new Kayttaja((int)reader[0], (String)reader[1], (String)reader[2], (int)reader[3], (bool)reader[4]);
                    }


                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static Kayttaja get(int id)
        {
            try
            {
                using (var conn = Database.connection())
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT * FROM kayttaja WHERE id = @id", conn))
                    {
                        command.Parameters.AddWithValue("id", id);

                        NpgsqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        return new Kayttaja((int)reader[0], (String)reader[1], (String)reader[2], (int)reader[3], (bool)reader[4]);
                    }


                }
            
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
