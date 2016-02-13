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

        private ComboBox _interlocutore;

        private ComboBox _frase;

        private Button _parla;

        private int _profonditàScelta;

        private string _interlocutoreAttuale = "";

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
		public GestoreMappa(Ambiente[] m, Button[] cs, Azione[] az, ComboBox i, ComboBox f, Button p)
		{
			if (m != null)
				Mappa = (Ambiente[])m.Clone();
			else
				Mappa = null;
			_pulsantiSpostamento = (Button[])cs.Clone();

            _interlocutore = i;
            _frase = f;
            _parla = p;
			_profonditàScelta = -1;
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
				return s + "\n";
			}

			//Per tutti gli altri comandi che non sono Direzione.Avvio viene eseguita questa parte di codice

			IndiceStanza = Mappa[IndiceStanza].Passaggi[(int)comando].IndiceAmbienteDestinazione;	//Trova l'indice della stanza di arrivo
			string st = "Sei andato in " + Mappa[IndiceStanza].Descrizione + ".\n";					//Comincia a riempire la stringa st che conterrà tutte le informazioni sullo spostamento

			//Controllo delle eventuali azioni da applicare
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
            
            //Controllo delle eventuali entità presenti nell'Ambiente di arrivo
            if(Mappa[IndiceStanza].Cose != null)
            {
                _interlocutore.Items.Clear();
                bool checkDialogs = false;
                foreach (Entità ent in Mappa[IndiceStanza].Cose)
                {
                    if (ent is Persona)
                    {
                        Persona p = (Persona)ent;
                        st += p.Descrizione + "\n";
                        if (p.Dial != null)
                        {
                            _frase.Items.Clear();
                            _interlocutore.Items.Add(p.Nome);
                            checkDialogs = true;
                        }
                    }
                }
                _parla.IsEnabled = checkDialogs;
            }
            else
            {
				_interlocutore.Items.Clear();
				_frase.Items.Clear();
                _parla.IsEnabled = false;
            }

            _interlocutoreAttuale = "";
            _profonditàScelta = -1;

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
			return st + "\n";
			
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
					//Lettura numero degli ambienti
					int n = int.Parse(sr.ReadLine());
					Mappa = new Ambiente[n];
					int count = 0;						//Variabile per il conteggio dell'ambiente

					//Lettura file completo
					while (sr.Peek() != -1)
					{
						//Creazione ambiente
						Mappa[count] = new Ambiente("", null, null, null, null);
						string[] st = sr.ReadLine().Split(',');

						//Aggiunta della descrizione dell'ambiente
						Mappa[count].Descrizione = st[1].Trim().Replace("\\n", "\n");

						//Controlla se sono presenti azioni nell'ambiente
						if (st.Length > 2)
						{
							//Se sono presenti, le crea e le inserisce nel vettore Azioni della classe Ambiente
							int nAzioni = int.Parse(st[2]);
                            Mappa[int.Parse(st[0].Trim())].Azioni = new Azione[nAzioni];
                            Mappa[int.Parse(st[0].Trim())].Cose = new List<Entità>();
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
                                    case "Persona":
                                        Dialogo dial = new Dialogo();
                                        string[] infos = effettoAzione.Split(',');
                                        string nome = infos[0];
                                        string descrizione = infos[1];
                                        int vita = int.Parse(infos[2]);
                                        if(infos[3] != "null")
                                        {
                                            try
                                            {
                                                using (StreamReader sr2 = new StreamReader("Dialoghi\\Dialoghi.txt"))
                                                {
													string[] ss = sr2.ReadLine().Split(',');
													string nomeAttuale;
													if (ss.Length > 1)
														nomeAttuale = ss[1].Trim();
													else
														nomeAttuale = "";
													bool arrivato = nomeAttuale == nome;
													while (!arrivato)
													{
														ss = sr2.ReadLine().Split(',');
														if (ss.Length > 1)
															nomeAttuale = ss[1].Trim();
														else
															nomeAttuale = "";
														arrivato = nomeAttuale == nome;
													}
                                                    string[] inf = ss;
                                                    if (infos[3] == inf[0])
                                                    {
                                                        dial.Scelte = new List<Scelta>();
                                                        for (int k = 0; k < int.Parse(inf[2]); k++)
                                                        {
                                                            string[] dialogo = sr2.ReadLine().Split(';');
                                                            List<Tuple<string,int>> scelte = new List<Tuple<string,int>>();
                                                            for(int l = 1; l < dialogo.Length; l++)
                                                                scelte.Add(new Tuple<string,int>(dialogo[l].Split(':')[0], dialogo[l].Split(':').Length > 1 ? int.Parse(dialogo[l].Split(':')[1]) : -1));
                                                            dial.Scelte.Add(new Scelta(dialogo[0], scelte));
                                                        }
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
                                        else
                                        {
                                            dial = null;
                                        }
                                        Mappa[int.Parse(st[0].Trim())].Cose.Add(new Persona(nome, descrizione, vita, dial));
                                        break;
                                    default:
                                        throw new NotImplementedException();   
                                }
							}
						}

						//Creazione passaggi
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

        public string Parla()
        {
			int lastProfondità = _profonditàScelta;
            string nome = (string)_interlocutore.SelectedItem;
            if(nome == null)
            {
                return "Scegli con chi vuoi parlare\n";
            }

            if (_interlocutoreAttuale == "" || _interlocutoreAttuale == nome)
            {
                _interlocutoreAttuale = nome;
                string opzione = (string)_frase.SelectedItem;
                string risposta = "";
				bool check = false;
                foreach (Entità ent in Mappa[IndiceStanza].Cose)
                {
                    if (ent.Nome == nome && ent is Persona)
                    {
                        Persona p = (Persona)ent;

						if (_profonditàScelta == -1)
						{
							_profonditàScelta++;
							CaricaOpzioni(nome);
							return nome + ": " + p.Dial.Scelte[_profonditàScelta].Entrata + "\n";
						}

                        int opz = 0;
                        for (int i = 0; i < p.Dial.Scelte[_profonditàScelta].Opzioni.Count; i++)
                        {
                            if (p.Dial.Scelte[_profonditàScelta].Opzioni[i].Item1 == opzione)
                            {
								if (p.Dial.Scelte[_profonditàScelta].Opzioni[i].Item2 >= 1)
								{
                                    opz = p.Dial.Scelte[_profonditàScelta].Opzioni[i].Item2;
									check = true;
								}
                                break;
                            }
                        }

						if (check)
						{
							risposta = p.Dial.Scelte[opz].Entrata;
							_profonditàScelta = opz;
							risposta = "Tu: " + opzione + "\n" + nome + ": " + risposta + "\n";
						}
						else
						{
							risposta = "Tu: " + opzione + "\n" + "Ma non risponde...\n";
                            _profonditàScelta = -1;
						}

						CaricaOpzioni(nome);

						return risposta;
                    }
                }
                return "";
            }
            else
                return "Stavi parlando con " + _interlocutoreAttuale + "!";
        }

		public void CaricaOpzioni(string n)
		{
            _frase.Items.Clear();
            if (Mappa[IndiceStanza].Cose != null)
			{
				foreach (Entità ent in Mappa[IndiceStanza].Cose)
				{
					if (ent.Nome == n && ent is Persona)
					{
						Persona p = (Persona)ent;
						if (_profonditàScelta != -1)
						{
							if (p.Dial.Scelte[_profonditàScelta].Opzioni.Count == 0)
							{
								_profonditàScelta = -1;
                                _interlocutoreAttuale = "";
								_frase.Items.Clear();
								return;
							}
							foreach (Tuple<string, int> t in p.Dial.Scelte[_profonditàScelta].Opzioni)
							{
								_frase.Items.Add(t.Item1);
							}
						}
					}
				}
			}
		}
	}
}
