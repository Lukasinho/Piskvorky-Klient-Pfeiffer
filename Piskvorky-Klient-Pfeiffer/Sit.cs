using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Piskvorky_Klient_Pfeiffer
{
    class Sit
    {
        string ipadresa;
        int cisloPortu;
        public Sit(string ip, int port)
        {
            ipadresa = ip;
            cisloPortu = port;
        }

        public void Poslouchej()
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(ipadresa), cisloPortu);
            TcpClient client = null;
            try
            {
                while (true)
                {

                    listener.Start();
                    client = listener.AcceptTcpClient();
                    NetworkStream clientStream = client.GetStream();
                    StreamReader reader = new StreamReader(clientStream);
                    string obsahZpravy = reader.ReadToEnd();
                    MessageBox.Show("Klient prijal: " + obsahZpravy);


                    //ROZKODOVAT SI TYP ZPRAVY
                    //Prichozi: Hrajes , Kolecko, Krizek
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Nastala chyba v poslechu " + e);
            }
            finally
            {
                if (client != null)
                {
                    client.Close();
                }
                listener.Stop();
            }

        }

        public void Odesli(string data)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(ipadresa, 5000);      //adresa serveru
                Stream strm = client.GetStream();
                StreamWriter writer = new StreamWriter(strm);
                writer.Write(data);
                writer.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Nastala chyba v odesilani " + e);

            }
            finally
            {
                if (client != null)
                    client.Close();

            }
        }


    }
}
