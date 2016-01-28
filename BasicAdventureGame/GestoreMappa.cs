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
using System.IO;

namespace BasicAdventureGame
{
	/// <summary>
	/// Classe che gestisce una mappa
	/// </summary>
	class GestoreMappa
	{
		/// <summary>
		/// Attributo vettore di Button che identifica i bottoni che gestiranno i movimenti nella mappa
		/// </summary>
		private Button[] _pulsantiSpostamento;

		/// <summary>
		/// Proprietà intera che descrive in quale "Stanza" (Ambiente) ci si trova
		/// </summary>
		public int IndiceStanza { get; set; }

		/// <summary>
		/// Proprietà vettore di Ambiente che deliena la mappa
		/// </summary>
		public Ambiente[] Mappa { get; set; }

		/// <summary>
		/// Costruttore principale
		/// </summary>
		/// <param name="m">Vettore di Ambiente</param>
		/// <param name="cs">Vettore di Button</param>
		public GestoreMappa(Ambiente[] m, Button[] cs, Azione[] az)
		{
			if (m != null)
				Mappa = (Ambiente[])m.Clone();
			else
				Mappa = null;
			_pulsantiSpostamento = (Button[])cs.Clone();
		}

		/// <summary>
		/// Metodo che permette di cambiare ambiente all'interno della mappa e ottenere i messaggi per il giocatore
		/// </summary>
		/// <param name="comando">Direzione da prendere</param>
		/// <returns>Stringa con i messaggi per il giocatore</returns>
		public string CambiaAmbiente(Direzioni comando)
		{
			//All'avvio viene inizializzata la mappa partendo dall'ambiente con indice 0
			//Il codice è simile a quello per le altre direzioni, quindi si consiglia di guardare subito sotto se si hanno dubbi
			if (comando == Direzioni.Avvio)
			{
				IndiceStanza = 0;
				string s = "Benvenuto nel " + Mappa[IndiceStanza].Descrizione + "\n";
				for (int i = 0; i < Mappa[IndiceStanza].Passaggi.Length; i++)
				{
					if (Mappa[IndiceStanza].Passaggi[i] != null)
					{
						string direzione;
						string chiuso = "";
						if (i == 0)
							direzione = "Nord";
						else if (i == 1)
							direzione = "Est";
						else if (i == 2)
							direzione = "Sud";
						else
							direzione = "Ovest";

						if (!Mappa[IndiceStanza].Passaggi[i].Aperto)
						{
							chiuso = "(Chiuso)";
							_pulsantiSpostamento[i].IsEnabled = false;
						}
						else
							_pulsantiSpostamento[i].IsEnabled = true;

						s += "A " + direzione  + " " + Mappa[IndiceStanza].Passaggi[i].Titolo + chiuso + ". ";
					}
					else
						_pulsantiSpostamento[i].IsEnabled = false;
				}
				return s;
			}

			//Per tutti gli altri comandi che non sono Direzione.Avvio viene eseguita questa parte di codice

            

			IndiceStanza = Mappa[IndiceStanza].Passaggi[(int)comando].IndiceAmbienteDestinazione;	//Trova l'indice della stanza di arrivo
			string st = "Sei andato in " + Mappa[IndiceStanza].Descrizione + ".\n";					//Comincia a riempire la stringa st che conterrà tutte le informazioni sullo spostamento

            for (int i = 0; i < Mappa.Length; i++)
            {
                if(Mappa[i].Azioni != null)
                {
                    for (int j = 0; j < Mappa[i].Azioni.Length; j++)
                    {
                        if (Mappa[i].Azioni[j] != null && Mappa[i].Azioni[j].GetType() == typeof(ApriPassaggio))
                        {
                            if (IndiceStanza == i)
                            {
                                st += Mappa[i].Azioni[j].Esegui(this);
                            }
                        }
                    }
                }
            }
            

            //Ciclo per finire il riempimento della stringa st con informazioni riguardanti gli ambienti visibili
            //Inoltre permette di disabilitare i pulsanti qualora mancassi il passaggio o fosse chiuso
            for (int i = 0; i < Mappa[IndiceStanza].Passaggi.Length; i++)
			{
				if (Mappa[IndiceStanza].Passaggi[i] != null)
				{
					string direzione;
					string chiuso = "";
					if (i == 0)
						direzione = "Nord";
					else if (i == 1)
						direzione = "Est";
					else if (i == 2)
						direzione = "Sud";
					else
						direzione = "Ovest";

					if (!Mappa[IndiceStanza].Passaggi[i].Aperto)
					{
						chiuso = "(Chiuso)";
						_pulsantiSpostamento[i].IsEnabled = false;
					}
					else
						_pulsantiSpostamento[i].IsEnabled = true;

					st += "A " + direzione + " " + Mappa[IndiceStanza].Passaggi[i].Titolo + chiuso + ". ";
				}
				else
					_pulsantiSpostamento[i].IsEnabled = false;
			}
			return st;
			
		}

		/// <summary>
		/// Metodo che permette di caricare una mappa da file
		/// </summary>
		/// <param name="percorsoFile">Percorso del file in cui è presente la mappa</param>
		public void CaricaMappa(string percorsoFile)
		{
			try
			{
				using (StreamReader sr = new StreamReader(percorsoFile))
				{
					int n = int.Parse(sr.ReadLine());
					Mappa = new Ambiente[n];
					int count = 0;
					while (sr.Peek() != -1)
					{
						Mappa[count] = new Ambiente("", null, null, null, null);
						string[] st = sr.ReadLine().Split(',');
						Mappa[count].Descrizione = st[1].Trim().Replace("\\n", "\n");
						if (st.Length > 2)
						{
							int nAzioni = int.Parse(st[2]);
                            Mappa[int.Parse(st[0].Trim())].Azioni = new Azione[nAzioni];
                            for (int i = 0; i < nAzioni; i++)
							{
								string[] act = sr.ReadLine().Split(':');
                                string nomeAzione = act[0];
                                string effettoAzione = act[1];

                                switch (nomeAzione)
                                {
                                    case "Open":
                                        string indicePartenza = effettoAzione.Split('-')[0];
                                        string indiceArrivo = effettoAzione.Split('-')[1];
                                        Mappa[int.Parse(st[0].Trim())].Azioni[i] = new ApriPassaggio(int.Parse(indicePartenza), int.Parse(indiceArrivo));
                                        break;
                                    default:
                                        throw new NotImplementedException();   
                                }
							}
						}
						for (int i = 0; i < 4; i++)
						{
							string s = sr.ReadLine().Trim();
							if (s != "null")
							{
								string[] ss = s.Trim().Split(',');
								Mappa[count].Passaggi[i] = new Passaggio(ss[0], bool.Parse(ss[1]), int.Parse(ss[2]));
							}
							else
								Mappa[count].Passaggi[i] = null;
						}
						count++;
					}
				}

			}
			catch (IOException ex)
			{
				MessageBox.Show("Eccezione in fase di carimento file : " + ex.Message);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Eccezione non gestita : " + ex.Message);
			}
		}
	}
}
