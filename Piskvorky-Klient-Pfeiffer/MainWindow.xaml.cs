using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Piskvorky_Klient_Pfeiffer
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] polePravidla = new int[11, 6];
        public Databaze db;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void pripojeniDatabaze()
        {
            db = new Databaze(tbDatabaze.Text);      //TODO: zatim nefunkcni ve tride DATABAZE!!!funguje jako localhost

            db.Pripojit();

            lInfo.Content = "Připojeno k databázi";
            tbDatabaze.IsEnabled = false;
            btnPripojit.Content = "Odpojit";
        }

        private void odpojeniDatabaze()
        {
            //Databaze db = new Databaze(tbDatabaze.Text);

            db.Odpojit();

            lInfo.Content = "Odpojeno od databáze";
            tbDatabaze.IsEnabled = true;
            btnPripojit.Content = "Připojit";
        }

        private void btnPripojit_Click(object sender, RoutedEventArgs e)
        {
            if (tbDatabaze.IsEnabled == true)
            {
                pripojeniDatabaze();
            }

            else
            {
                odpojeniDatabaze();
            }
            
        }
    }
}
