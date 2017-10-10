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
        public int Id { get; set; }
        [Required(ErrorMessage = "Nimi pitää antaa")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Nimen pituus väliltä 3-30 merkkiä")]
        public String Nimi { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Ryhmän pituus väliltä 3-30 merkkiä")]
        public String Tyontekijaryhma { get; set; }
        public List<Tyopiste> Tyopisteet { get; set; }


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
                    Tyontekija t = null;

                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        t = new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]);
                        t.Tyopisteet = new List<Tyopiste>(Tyopiste.all());
                        l.Add(t);
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
                    
                    foreach (var tp in Tyopisteet)
                    {
                        if (tp.Allowed)
                        {
                            try
                            {
                                command.CommandText = ("INSERT INTO Luvat (tyopiste_id, tyontekija_id) VALUES (@tp_id, @t_id)");
                                command.Parameters.AddWithValue("t_id", this.Id);
                                command.Parameters.AddWithValue("tp_id", tp.Id);
                                command.ExecuteNonQuery();
                                command.Parameters.Clear();
                            }
                            catch (Exception)
                            {
                                command.Parameters.Clear();
                            }

                        }
                        else
                        {
                            command.CommandText = ("DELETE FROM Luvat WHERE tyopiste_id=@tp_id AND tyontekija_id = @t_id");
                            command.Parameters.AddWithValue("t_id", this.Id);
                            command.Parameters.AddWithValue("tp_id", tp.Id);
                            command.ExecuteNonQuery();
                            command.Parameters.Clear();
                        }
                    }
                    
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
        public static Tyontekija findWithPermissions(int id)
        {
            using (var conn = Database.connection())
            {
                conn.Open();
                string sql = @"SELECT t.id, t.nimi, tyontekijaryhma, tyopiste_id, tp.nimi AS tyopiste FROM tyontekija t
                        INNER JOIN luvat l ON t.id = l.tyontekija_id INNER JOIN tyopiste tp ON l.tyopiste_id = tp.id WHERE t.id = @id";

                using (var command = new NpgsqlCommand(sql, conn))
                {

                    command.Parameters.AddWithValue("id", id);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    Tyontekija t = null;
                    List<Tyopiste> tyopisteet = new List<Tyopiste>(Tyopiste.all());

                    if (!reader.Read())
                    {
                        t = find(id);
                    }
                    else
                    {
                        t = new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]);
                        tyopisteet.Find(x => x.Id == new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] }.Id).Allowed = true;
                        while (reader.Read())
                        {
                            tyopisteet.Find(x => x.Id == new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] }.Id).Allowed = true;
                        }
                    }
                    t.Tyopisteet = tyopisteet;
                    return t;
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
                    List<Tyopiste> tyopisteet = new List<Tyopiste>(Tyopiste.all());

                    if (t == null)
                    {
                        reader.Read();
                        t = new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]);
                        tyopisteet.Find(x => x.Id == new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] }.Id).Allowed = true;
                    }
                    while (reader.Read())
                    {
                        if (t.Id == (int)reader[0])
                        {

                            tyopisteet.Find(x => x.Id == new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] }.Id).Allowed = true;

                            {

                            };
                        }
                        else
                        {
                            t.Tyopisteet = tyopisteet;
                            tyopisteet = new List<Tyopiste>(Tyopiste.all());
                            tyontekijat.Add(t);
                            t = new Tyontekija((int)reader[0], (String)reader[1], (String)reader[2]);
                            tyopisteet.Find(x => x.Id == new Tyopiste { Id = (int)reader[3], Nimi = (string)reader[4] }.Id).Allowed = true;
                        }

                    }
                    t.Tyopisteet = tyopisteet;
                    tyontekijat.Add(t);




                    return tyontekijat;
                }


            }
        }
    }
}
