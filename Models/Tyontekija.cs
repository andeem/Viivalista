using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Viivalista.lib;

namespace Viivalista.Models
{
    public class Tyontekija
    {
        public int Id { get; }
        [Required(ErrorMessage="Nimi pitää antaa")]
        [StringLength(30, MinimumLength = 3, ErrorMessage ="Nimen pituus väliltä 3-30 merkkiä")]
        public String Nimi { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Ryhmän pituus väliltä 3-30 merkkiä")]
        public String Tyontekijaryhma { get; set; }

        public Tyontekija()
        {

        }


        public Tyontekija(int id, String nimi, String tryhma)
        {
            this.Id = id;
            this.Nimi = nimi;
            this.Tyontekijaryhma = tryhma;
        }

        public static Tyontekija find(int id)
        {

            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM tyontekija WHERE id = @id", conn))
                {
                    command.Parameters.AddWithValue("id", id);

                    NpgsqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    return new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]);
                }


            }
        }
        public static List<Tyontekija> all()
        {

            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM tyontekija", conn))
                {

                    List<Tyontekija> l = new List<Tyontekija>();

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        l.Add(new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]));
                    }
                    return l;
                }


            }
        }

        public void save()
        {
            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("INSERT INTO tyontekija (nimi, tyontekijaryhma) VALUES (@nimi, @tyontekijaryhma)", conn))
                {
                    command.Parameters.AddWithValue("nimi", this.Nimi);
                    command.Parameters.AddWithValue("tyontekijaryhma", this.Tyontekijaryhma);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void update()
        {
            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("UPDATE Tyontekija SET nimi = @nimi, tyontekijaryhma = @tryhma WHERE id = @id", conn))
                {
                    command.Parameters.AddWithValue("nimi", this.Nimi);
                    command.Parameters.AddWithValue("tryhma", this.Tyontekijaryhma);
                    command.Parameters.AddWithValue("id", this.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void delete()
        {
            using (var conn = Database.connection())
            {
                conn.Open();
                using (var command = new NpgsqlCommand("DELETE FROM Tyontekija WHERE id = @id", conn))
                {
                    command.Parameters.AddWithValue("id", this.Id);

                    command.ExecuteNonQuery();
                } 
            }
        }
    }
}
