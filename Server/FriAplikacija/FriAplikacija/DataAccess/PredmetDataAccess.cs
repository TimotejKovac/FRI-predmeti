﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FriAplikacija.Table;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FriAplikacija.DataAccess {
    public class PredmetDataAccess {
        private static String SOURCE = "Server=tcp:friaplikacija.database.windows.net,1433;Initial Catalog=friAplikacija;Persist Security Info=False;User ID=user;Password=friAplikacija1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static Predmet getPredmet(int predmetID) {
            DataTable data = new DataTable("Predmet");
            using (SqlConnection connection = new SqlConnection(SOURCE)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Predmet Where predmetID = @predmetID", connection)) {
                    command.Parameters.Add(new SqlParameter("predmetID", predmetID));
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                        da.Fill(data);
                }
                connection.Close();
            }
            if (data.Rows.Count == 1) {
                Predmet predmet = rowToPredmet(data.Rows[0]);
                return predmet;
            } else {
                return null;
            }
        }

        public static PredmetWithOznaka getPredmetWithOznaka(int predmetID) {
            DataTable data = new DataTable("Predmet");
            using (SqlConnection connection = new SqlConnection(SOURCE)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Predmet Where predmetID = @predmetID", connection)) {
                    command.Parameters.Add(new SqlParameter("predmetID", predmetID));
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                        da.Fill(data);
                }
                connection.Close();
            }
            if (data.Rows.Count == 1) {
                PredmetWithOznaka predmet = rowToPredmetWithOznaka(data.Rows[0]);
                predmet.oznaka = OznakaDataAccess.getOznakeForPredmet(predmetID);
                return predmet;
            } else {
                return null;
            }
        }

        internal static List<Predmet> getAllPredmeti() {
            DataTable data = new DataTable("Predmet");
            using(SqlConnection connection = new SqlConnection(SOURCE)) {
                connection.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM Predmet", connection)) {
                    using(SqlDataAdapter da = new SqlDataAdapter(command))
                        da.Fill(data);
                }
                connection.Close();
            }
            if(data.Rows.Count >= 1) {
                return rowsToPredmeti(data);
            }
            return null;
        }

        internal static List<Predmet> getPredmetiForIzvajalec(int izvajalecID) {
            DataTable data = new DataTable("Predmet");
            using(SqlConnection connection = new SqlConnection(SOURCE)) {
                connection.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM Predmet p JOIN Izvaja i ON p.predmetID = i.predmetID JOIN Izvajalec iz ON iz.izvajalecID = i.izvajalecID WHERE i.izvajalecID = @izvajalecID", connection)) {
                    command.Parameters.Add(new SqlParameter("izvajalecID", izvajalecID));
                    using(SqlDataAdapter da = new SqlDataAdapter(command))
                        da.Fill(data);
                }
                connection.Close();
            }
            if(data.Rows.Count >= 1) {
                return rowsToPredmeti(data);
            }
            return null;
        }

        internal static List<Predmet> getPredmetiForPodrocje(int podrocjeID) {
            DataTable data = new DataTable("Predmet");
            using(SqlConnection connection = new SqlConnection(SOURCE)) {
                connection.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM Predmet p JOIN JeIzPodrocja j ON p.predmetID = j.predmetID JOIN Podrocje po ON j.podrocjeID = po.podrocjeID WHERE po.podrocjeID = @podrocjeID", connection)) {
                    command.Parameters.Add(new SqlParameter("podrocjeID", podrocjeID));
                    using(SqlDataAdapter da = new SqlDataAdapter(command))
                        da.Fill(data);
                }
                connection.Close();
            }
            if(data.Rows.Count >= 1) {
                return rowsToPredmeti(data);
            }
            return null;
        }

        internal static List<Predmet> getPredmetiForOznaka(int oznakaID) {
            DataTable data = new DataTable("Predmet");
            using(SqlConnection connection = new SqlConnection(SOURCE)) {
                connection.Open();
                using(SqlCommand command = new SqlCommand("SELECT * FROM Predmet p JOIN OznacujePredmet o ON p.predmetID = o.predmetID JOIN Oznaka oz ON o.oznakaID = oz.oznakaID WHERE o.oznakaID = @oznakaID", connection)) {
                    command.Parameters.Add(new SqlParameter("oznakaID", oznakaID));
                    using(SqlDataAdapter da = new SqlDataAdapter(command))
                        da.Fill(data);
                }
                connection.Close();
            }
            if(data.Rows.Count >= 1) {
                return rowsToPredmeti(data);
            }
            return null;
        }

        private static List<Predmet> rowsToPredmeti(DataTable data) {
            List<Predmet> predmeti = new List<Predmet>();
            foreach(DataRow row in data.Rows) {
                predmeti.Add(rowToPredmet(row));
            }
            return predmeti;
        }

        private static Predmet rowToPredmet(DataRow row) {
            Predmet predmet = new Predmet();
            predmet.predmetID = Int32.Parse(row["predmetID"].ToString());
            predmet.ime = row["ime"].ToString();
            predmet.splosnaOcena = Decimal.Parse(row["splosnaOcena"].ToString());
            predmet.tezavnostOcena = Decimal.Parse(row["tezavnostOcena"].ToString());
            predmet.zanimivostOcena = Decimal.Parse(row["zanimivostOcena"].ToString());
            predmet.uporabnostOcena = Decimal.Parse(row["uporabnostOcena"].ToString());
            predmet.ocena = row["opis"].ToString();
            return predmet;
        }

        private static PredmetWithOznaka rowToPredmetWithOznaka(DataRow row) {
            PredmetWithOznaka predmet = new PredmetWithOznaka();
            predmet.predmetID = Int32.Parse(row["predmetID"].ToString());
            predmet.ime = row["ime"].ToString();
            predmet.splosnaOcena = Decimal.Parse(row["splosnaOcena"].ToString());
            predmet.tezavnostOcena = Decimal.Parse(row["tezavnostOcena"].ToString());
            predmet.zanimivostOcena = Decimal.Parse(row["zanimivostOcena"].ToString());
            predmet.uporabnostOcena = Decimal.Parse(row["uporabnostOcena"].ToString());
            predmet.ocena = row["opis"].ToString();
            return predmet;
        }
    }
}