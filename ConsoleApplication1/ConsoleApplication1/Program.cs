using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApplication4
{
    class Program
    {
        static SqlConnection pripojeni;
        static SqlConnectionStringBuilder sestaveni;

        static void Main(string[] args)
        {
            sestaveni = new SqlConnectionStringBuilder();
            sestaveni.DataSource = "@C305-PC19\SQLEXPRESS";
            sestaveni.InitialCatalog = "Slovnicek";
            sestaveni.UserID = "sa";
            sestaveni.Password = "sssaaa";

            using(pripojeni = new SqlConnection(sestaveni.ConnectionString))
            {
                pripojeni.Open();

                string dotaz = "SELECT * FROM Slovo";

                SqlDataAdapter naplneni = new SqlDataAdapter(dotaz, pripojeni);
                DataSet vysledky = new DataSet();
                naplneni.Fill(vysledky, Slovo);

                for (int i = 0; i < vysledky.Tables[Slovo].Rows.Count; i++)
                {
                    DataRow radek = vysledky.Tables[Slovo].Rows[i];

                    Console.WriteLine("ID: " + radek[0] + ", cesky: " + radek["cesky"] + ", anglicky " + radek["anglicky"] + ", obtiznost " + radek["obtiznost"]);
                }

                DataRow[] nalezeneRadky = vysledky.Tables["Slovo"].Select("id=2");

                nalezeneRadky[0].BeginEdit();
                nalezeneRadky[0]["cesky"] = "kun";
                nalezeneRadky[0]["anglicky"] = "horse";
            }
            Console.ReadKey();
        }
    }
}