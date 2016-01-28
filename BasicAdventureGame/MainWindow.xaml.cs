/*
    Il presente progetto non contiene nessuno degli obiettivi primari (lettura/scrittura file di testo, OOP) dei lavori che andrete a svolgere.
    Mostra solo un esempio di possibile interfaccia e alcune modalità per gestire menu, suoni, grafica ecc.

    Nei vostri progetti potrete utilizzare questi spezzoni di codice d'esempio, ma modificandoli ed adattandoli agli scopi 
    più volte menzionati.
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

        // Campi di classe per l'animazione di Han Solo.
        /*BitmapImage[] hanSolo =
        {
            CaricaImmagine("Immagini/Solo 1.png"),
            CaricaImmagine("Immagini/Solo 2.png"),
            CaricaImmagine("Immagini/Solo 3.png"),
            CaricaImmagine("Immagini/Solo 4.png"),
            CaricaImmagine("Immagini/Solo 5.png"),
            CaricaImmagine("Immagini/Solo 6.png"),
            CaricaImmagine("Immagini/Solo 7.png")
        };*/
        int hanSoloIndex = 0;
        Image han = new Image();
        DispatcherTimer dt = new DispatcherTimer();

        // Utilizzo di una DLL di Windows per poter eseguire i file MP3.
        // In alternativa provare "Windows Media Player control":
        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd562692(v=vs.85).aspx.
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);

        // Oggetto per eseguire suoni WAV qualora si voglia mantenerne un riferimento.
        //SoundPlayer suono;

        // Array con i riferimenti ai pulsanti di spostamento.
        //Button[] comandiSpostamento = new Button[4];  // Esempio indipendente.
		GestoreMappa mappaPrincipale;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Inizializzazione dell'esempio indipendente.
            //comandiSpostamento[0] = btnVaVersoNord;
            //comandiSpostamento[1] = btnVaVersoEst;
            //comandiSpostamento[2] = btnVaVersoSud;
            //comandiSpostamento[3] = btnVaVersoOvest;

            // *****************************************************************
            // Creazione della mappa, DOVETE idearne una vostra ben più ricca. *
            // *****************************************************************
            /*
			mappaPrincipale = new GestoreMappa(
                        new Ambiente[]
                        {
						    new Ambiente("Villaggio",
                                    new Passaggio(" ci sono dei Campi coltivati", true, 1),
                                    new Passaggio(" si vede la Strada Principale", true, 2),
                                    null,
                                    new Passaggio(" si vedono i Magazzini", false, 3)),
						    new Ambiente("Campi coltivati",
                                    new Passaggio(" si nota da lontano un Lago", true, 5),
                                    null,
                                    new Passaggio(" c'è il Villaggio", true, 0),
                                    null),
							new Ambiente("Strada principale",
                                    null,
                                    null,
                                    new Passaggio(" cammini fino al 2° miglio della Strada Principale", true, 4),
                                    new Passaggio(" c'è il Villaggio", true, 0)),
							new Ambiente("Magazzini",
                                    null,
                                    new Passaggio(" c'è il Villaggio", true, 0),
                                    null,
                                    null),
							new Ambiente("Strada principale, 2° miglio",
                                    new Passaggio(" si torna indietro sulla Strada Principale", true, 2),
                                    null,
                                    null,
                                    null),
							new Ambiente("Lago",
                                    null,
                                    null,
                                    new Passaggio(" ci sono dei Campi Coltivati", true, 1),
                                    null),
                        },
                        new Button[] { btnVaVersoNord , btnVaVersoEst, btnVaVersoSud, btnVaVersoOvest }
                );*/

			//Creazione gestore mappa con i bottoni che gestiranno le direzioni
			mappaPrincipale = new GestoreMappa(null, new Button[] { btnVaVersoNord, btnVaVersoEst, btnVaVersoSud, btnVaVersoOvest }, null);

			//Caricamento mappa da file
			mappaPrincipale.CaricaMappa("MappeGioco\\Mappa1.txt");

            // ******************************
            // * Avvia la prima stanza.     *
            // ******************************
            //CambiaAmbiente(0);
            txtEsito.Text = mappaPrincipale.CambiaAmbiente(Direzioni.Avvio);
			/*
            // *****************************************
            // * Esempio su come eseguire un file MP3. *
            // *****************************************
            mciSendString("open \"Audio\\Battles In Space.mp3\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile REPEAT", null, 0, IntPtr.Zero);

            // Per fermarlo
            //mciSendString("close MediaFile", null, 0, IntPtr.Zero);

            // ******************************************************
            // * Esempio su come eseguire un file WAV (2 modalità). *
            // ******************************************************
            //suono = new SoundPlayer();
            //suono.SoundLocation = "Audio\\swvader02.wav";
            //suono.Load();
            //suono.Play();
            //suono.Stop();     // ... da eseguire eventualmente in seguito.
            new SoundPlayer("Audio\\swvader02.wav").Play();

            // **************************************************
            // * Come caricare e posizionare le immagini da C#. *
            // **************************************************

            // R2D2.
            Image r2d2 = new Image();
            r2d2.Source = CaricaImmagine("Immagini/r2d2.png");
            PosizionaScalaZIndex(r2d2, 209, 230, 0.5, 0);

            // Nave nell'hangar.
            Image nave = new Image();
            nave.Source = CaricaImmagine("Immagini/ship.png");
            PosizionaScalaZIndex(nave, 0, 200, 0.3, -1);

            // Han Solo con animazione tramite timer.
            han.Source = hanSolo[0];
            PosizionaScalaZIndex(han, 165, 150, 0.7, -1);
            dt.Interval = TimeSpan.FromMilliseconds(200);
            dt.Tick += Dt_Tick;
            dt.Start();

            // Aggiunge le immagini al Canvas.
            // Vedi anche i metodi Remove e RemoveAt.
            cnvVista.Children.Add(r2d2);
            cnvVista.Children.Add(nave);
            cnvVista.Children.Add(han);*/
        }
		/*
        private void Dt_Tick(object sender, EventArgs e)
        {
            // Aggiorna indice dell'animazione ma "giocando" con l'operatore ternario.
            hanSoloIndex = ++hanSoloIndex == hanSolo.Length ? 0 : hanSoloIndex;

            // Impostare Source a null per non visualizzare nulla, oppure impostare
            // han.Visibility = Visibility.Collapsed; (oppure .Hidden)
            han.Source = hanSolo[hanSoloIndex];
        }
		*/
        /// <summary>
        /// Carica un'immagine dalle risorse dell'applicazione o dal file system.
        /// Perché un'immagine sia memorizzate nelle risorse impostare 
        /// Azione di compilazione = Resource
        /// Copia nella directoy di outpuy = <vuoto>.
        /// Per il file system
        /// Azione di compilazione = Contenuto
        /// Copia nella directoy di outpuy = Copia se più recente.
        /// </summary>
        /// <param name="nomeFile">Percorso fino alla risorsa.</param>
        /// <returns>Bitmap dell'immagine caricata.</returns>
        private static BitmapImage CaricaImmagine(string nomeFile)
        {
            return new BitmapImage(new Uri(nomeFile, UriKind.Relative));
        }

        /// <summary>
        /// Imposta la posizione, il fattore di scala uniforme e, opzionalmente, lo ZIndex
        /// che stabilisce l'ordine di rendering per gestire in modo coerente le sovrapposizioni
        /// tra le immagini.
        /// </summary>
        /// <param name="img">Immagine.</param>
        /// <param name="x">Coordinata x rispetto al Canvas, parte da 0 e cresce verso destra.</param>
        /// <param name="y">Coordinata y rispetto al Canvas, parte da 0 e cresce verso il basso.</param>
        /// <param name="scalaUniforme">Fattore di scala uniforme (da applicare sia in senso X che Y), 
        /// 1 non modifica la scala, -1 inverte.</param>
        /// <param name="zi">ZIndex, default = 0.</param>
        private static void PosizionaScalaZIndex(Image img, double x, double y, double scalaUniforme, int zi)
        {
            // Imposta la posizione.
            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, y);

            // La traformazione di scala ha come origine il pixel (0, 0) dell'immagine, con altri
            // due argomenti (misure in pixel e relativi al punto in alto a sinistra dell'immagine)
            // è possibile posizionare altrove tale origine.
            // Vedere anche TranslateTransform, RotateTransform e come combinarle in un'unica trasformazione
            // con TransformGroup (e la proprietà Children con il metodo Add): appunti facoltativi di terza.
            img.RenderTransform = new ScaleTransform(scalaUniforme, scalaUniforme);

            // Specifica l'ordine di rendering (coordinata Z uscente dallo schermo).
            // A parità di ZIndex i controlli sono renderizzati nell'ordine in cui sono aggiunti
            // al canvas (nell'XAML e poi da codice).
            // Il valore di default è 0, ad esempio per le immagini inserite direttamente nell'XAML.
            Canvas.SetZIndex(img, zi);
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
            //if (ambienti[indiceStanza].Passaggi[(int)Direzioni.Nord].Aperto)
            //    CambiaAmbiente(ambienti[indiceStanza].Passaggi[(int)Direzioni.Nord].IndiceAmbienteDestinazione);
            txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Nord);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }

        private void btnVaVersoEst_Click(object sender, RoutedEventArgs e)
        {
            //if (ambienti[indiceStanza].Passaggi[(int)Direzioni.Est].Aperto)
            //    CambiaAmbiente(ambienti[indiceStanza].Passaggi[(int)Direzioni.Est].IndiceAmbienteDestinazione);
			txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Est);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }

        private void btnVaVersoSud_Click(object sender, RoutedEventArgs e)
        {
            //if (ambienti[indiceStanza].Passaggi[(int)Direzioni.Sud].Aperto)
            //    CambiaAmbiente(ambienti[indiceStanza].Passaggi[(int)Direzioni.Sud].IndiceAmbienteDestinazione);
			txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Sud);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }

        private void btnVaVersoOvest_Click(object sender, RoutedEventArgs e)
        {
            //if (ambienti[indiceStanza].Passaggi[(int)Direzioni.Ovest].Aperto)
            //    CambiaAmbiente(ambienti[indiceStanza].Passaggi[(int)Direzioni.Ovest].IndiceAmbienteDestinazione);
			txtEsito.Text += "\n" + mappaPrincipale.CambiaAmbiente(Direzioni.Ovest);
			txtEsito.Focus();
			txtEsito.CaretIndex = txtEsito.Text.Length;
        }
    }
}
