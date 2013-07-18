using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Piskvorky_Klient_Pfeiffer
{
    public class Databaze
    {
        private string ip;
        SqlConnection datovePripojeni = new SqlConnection();

        public Databaze(string ipadresa)
        {
            ip = ipadresa;
        }

        public void Pripojit()
        {
            try
            {
                SqlConnectionStringBuilder konfigurace = new SqlConnectionStringBuilder();
                konfigurace.DataSource = ".\\SQL2008R2";   //".\\SQLExpress";
                konfigurace.InitialCatalog = "Piskvorky";
                konfigurace.IntegratedSecurity = true;
                datovePripojeni.ConnectionString = konfigurace.ConnectionString;
                datovePripojeni.Open();
            }
            catch (SqlException e)
            {
                MessageBox.Show(String.Format("Chyba při vstupu do DB: {0}", e.Message));
            }
        }

        public void PridatHrace(string jmenoHrace)
        {

            try
            {
                
                SqlCommand datovyPrikaz = new SqlCommand();
                datovyPrikaz.Connection = datovePripojeni;
                datovyPrikaz.CommandType = CommandType.Text;
                datovyPrikaz.CommandText =
                    "Insert Into Hrac(pocet_bodu,jmeno) output inserted.id values (0,@JmenoHrace)";

                SqlParameter param = new SqlParameter("@JmenoHrace", SqlDbType.VarChar, 20);
                param.Value = jmenoHrace;
                datovyPrikaz.Parameters.Add(param);

              //  datovyPrikaz.ExecuteNonQuery();
                int id = (int) datovyPrikaz.ExecuteScalar();    //RIKA ID VLOZENEHO HRACE
            }
            catch (SqlException e)
            {
                MessageBox.Show("Chyba vložení do DB: "+e.Message);
           
            }
            finally
            {
                datovePripojeni.Close();
            }
            
           

        }
        public void Odpojit()
        {
            datovePripojeni.Close();
        }


        internal void PridatPravidlo(string p)
        {
            try
            {

                SqlCommand datovyPrikaz = new SqlCommand();
                datovyPrikaz.Connection = datovePripojeni;
                datovyPrikaz.CommandType = CommandType.Text;
                datovyPrikaz.CommandText =
                    "Insert Into Pravidlo(hrac_id,pravidlo) values (10,@p)";

                SqlParameter param = new SqlParameter("@p", SqlDbType.VarChar, 60);
                param.Value = p;
                datovyPrikaz.Parameters.Add(param);

                datovyPrikaz.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                MessageBox.Show(string.Format("Chyba vložení do DB: {0}", e.Message));

            }
            finally
            {
                datovePripojeni.Close();
            }
        }
    }
}
