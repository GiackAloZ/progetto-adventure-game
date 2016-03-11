/*
    Progetto di Aloisi Giacomo 4a F
*/
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

// Aggiunti.
using System.Diagnostics;
using System.Windows.Threading; // DispatcherTimer.
using System.Runtime.InteropServices;
using System.Media;
using Microsoft.Win32;


namespace BasicAdventureGame
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Giocatore Player = new Giocatore("GiackAloZ", "Il giocatore corrente", 100, 20, 20, 100);

		GestoreMappa mappaPrincipale;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			//Creazione gestore mappa con i bottoni che gestiranno le direzioni
            btnParla.IsEnabled = false;
			mappaPrincipale = new GestoreMappa(Player, null, new Button[] { btnVaVersoNord, btnVaVersoEst, btnVaVersoSud, btnVaVersoOvest }, null, cmbInterlocutore, cmbFrase, btnParla, lstInventarioAmbiente, cmbOggettoCoinvolto, lstInventarioGiocatore, lstArmi, lstIndumenti);

			CheckStats();

			//Caricamento mappa da file
			mappaPrincipale.CaricaMappa("MappeGioco\\Mappa1.txt");

            // ******************************
            // * Avvia la prima stanza.     *
            // ******************************
            //CambiaAmbiente(0);
            txtEsito.Text = mappaPrincipale.CambiaAmbiente(Direzioni.Avvio);
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Apre un file HTML nel browser predefinito.
            Process.Start("Help\\Help.html");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Chiude l'applicazione.
            this.Close();
        }

        private void btnVaVersoNord_Click(object sender, RoutedEventArgs e)
        {
            txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Nord);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }

        private void btnVaVersoEst_Click(object sender, RoutedEventArgs e)
        {
			txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Est);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }

        private void btnVaVersoSud_Click(object sender, RoutedEventArgs e)
        {
			txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Sud);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }

        private void btnVaVersoOvest_Click(object sender, RoutedEventArgs e)
        {
			txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Ovest);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }

        private void btnParla_Click(object sender, RoutedEventArgs e)
        {
            txtEsito.Text += "\n" + mappaPrincipale.Parla();
            txtEsito.Focus();
            txtEsito.CaretIndex = txtEsito.Text.Length;
        }

		private void cmbInterlocutore_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			mappaPrincipale.CaricaOpzioni((string)cmbInterlocutore.SelectedItem);
		}

		private void btnPrende_Click(object sender, RoutedEventArgs e)
		{
			txtEsito.Text += "\n" + mappaPrincipale.PrendiOggetto((Oggetto)cmbOggettoCoinvolto.SelectedItem);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
		}

		private void btnLasciaOggetto_Click(object sender, RoutedEventArgs e)
		{
			txtEsito.Text += "\n" + mappaPrincipale.LasciaOggetto((Oggetto)lstInventarioGiocatore.SelectedItem);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
		}

		private void btnEliminaOggetto_Click(object sender, RoutedEventArgs e)
		{
			txtEsito.Text += "\n" + mappaPrincipale.EliminaOggetto((Oggetto)lstInventarioGiocatore.SelectedItem);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
		}

		private void btnEquipaggiaOggetto_Click(object sender, RoutedEventArgs e)
		{
			txtEsito.Text += "\n" + mappaPrincipale.Equipaggia((Oggetto)lstInventarioGiocatore.SelectedItem);
			CheckStats();
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
		}

		private void btnRiponiArma_Click(object sender, RoutedEventArgs e)
		{
			txtEsito.Text += "\n" + mappaPrincipale.RiponiArma((Arma)lstArmi.SelectedItem);
			CheckStats();
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
		}

		private void btnRiponiIndumento_Click(object sender, RoutedEventArgs e)
		{
			txtEsito.Text += "\n" + mappaPrincipale.RiponiIndumento((Indumento)lstIndumenti.SelectedItem);
			CheckStats();
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
		}

		private void CheckStats()
		{
			prgSalute.Maximum = Player.MaxSalute;
			prgSalute.Value = Player.Salute;
			prgDifesa.Maximum = Player.Difesa;
			prgDifesa.Value = Player.Difesa;
			prgAttacco.Maximum = Player.Attacco;
			prgAttacco.Value = Player.Attacco;
			prgStamina.Maximum = Player.MaxStamina;
			prgStamina.Value = Player.Stamina;
			prgPrecisione.Maximum = Player.MaxPrecisione;
			prgPrecisione.Value = Player.Precisione;
			prgLivello.Value = Player.Livello;
			prgEsperienza.Maximum = Player.EsperienzaPassaggioLivello();
			prgEsperienza.Value = Player.Esperienza;
		}
    }
}
