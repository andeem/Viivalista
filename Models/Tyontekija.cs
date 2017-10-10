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
        [Required(ErrorMessage = "Nimi pitää antaa")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Nimen pituus väliltä 3-30 merkkiä")]
        public String Nimi { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Ryhmän pituus väliltä 3-30 merkkiä")]
        public String Tyontekijaryhma { get; set; }
        public IEnumerable<Tyopiste> luvat { get; set; }


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

        public static IEnumerable<Tyontekija> getAllWithPermissions()
        {
            using (var conn = Database.connection())
            {
                conn.Open();
                string sql = @"SELECT t.id, t.nimi, tyontekijaryhma, tyopiste_id, tp.nimi AS tyopiste FROM tyontekija t
                        INNER JOIN luvat l ON t.id = l.tyontekija_id INNER JOIN tyopiste tp ON l.tyopiste_id = tp.id";
                using (var command = new NpgsqlCommand(sql, conn))
                {


                    NpgsqlDataReader reader = command.ExecuteReader();
                    Tyontekija t = null;
                    List<Tyontekija> tyontekijat = new List<Tyontekija>();
                    List<Tyopiste> tyopisteet = new List<Tyopiste>();

                    if (t == null)
                    {
                        reader.Read();
                        t = new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]);
                        tyopisteet.Add(new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] });
                    }
                    while (reader.Read())
                    {
                        if (t.Id == (int)reader[0])
                        {
                            tyopisteet.Add(new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] });
                        }
                        else
                        {
                            t.luvat = tyopisteet;
                            tyopisteet = new List<Tyopiste>();
                            tyontekijat.Add(t);
                            t = new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]);
                            tyopisteet.Add(new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] });
                        }

                    }
                    t.luvat = tyopisteet;
                    tyontekijat.Add(t);




                    return tyontekijat;
                }


            }
        }
    }
}
