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

		private Giocatore _giocatore;

		private ListBox _invGiocatore;

		private ListBox _invAmbiente;

        private ComboBox _interlocutore;

		private ComboBox _oggettiCoinvolti;

        private ComboBox _frase;

        private Button _parla;

		private ListBox _armi;

		private ListBox _indumenti;

        private int _profonditàScelta;

		private bool _inCombattimento = false;

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
		public GestoreMappa(Giocatore g, Ambiente[] m, Button[] cs, Azione[] az, ComboBox i, ComboBox f, Button p, ListBox a, ComboBox oggs, ListBox ig, ListBox ar, ListBox ind)
		{
			if (m != null)
				Mappa = (Ambiente[])m.Clone();
			else
				Mappa = null;
			_pulsantiSpostamento = (Button[])cs.Clone();

			_giocatore = g;
            _interlocutore = i;
            _frase = f;
            _parla = p;
			_invAmbiente = a;
			_invGiocatore = ig;
			_armi = ar;
			_indumenti = ind;
			_oggettiCoinvolti = oggs;
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
                    for (int j = 0; j < Mappa[i].Azioni.Count; j++)
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
            if(Mappa[IndiceStanza].Cose.Count != 0)
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

			_invAmbiente.Items.Clear();
			_oggettiCoinvolti.Items.Clear();

			if (Mappa[IndiceStanza].Inv.Oggetti.Count != 0)
			{
				st += CaricaInventarioAmbiente();
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

						//Controlla se sono presenti cose nell'ambiente
						if (st.Length > 2)
						{
							int nCose = int.Parse(st[2]);
                            Mappa[int.Parse(st[0].Trim())].Azioni = new List<Azione>();
                            for (int i = 0; i < nCose; i++)
							{
								string[] act = sr.ReadLine().Split(':');
                                string nomeCosa = act[0];
                                string effettoCosa = act[1];

                                switch (nomeCosa)
                                {
                                    case "Open":
                                        string indicePartenza = effettoCosa.Split('-')[0];
                                        string indiceArrivo = effettoCosa.Split('-')[1];
                                        Mappa[int.Parse(st[0].Trim())].Azioni.Add(new ApriPassaggio(int.Parse(indicePartenza), int.Parse(indiceArrivo)));
                                        break;
                                    case "Persona":
                                        Dialogo dial = new Dialogo();
                                        string[] infos = effettoCosa.Split(',');
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
                                    case "Oggetto":
										string numOgg = effettoCosa;
										try
										{
											using (StreamReader sr2 = new StreamReader("Oggetti\\Oggetti.txt"))
											{
												string s = sr2.ReadLine();
												while (s.Split(',')[0] != numOgg)
												{
													s = sr2.ReadLine();
												}
												string[] infoOggetto = s.Split(',');
												if(infoOggetto.Length == 3)
													Mappa[int.Parse(st[0].Trim())].Inv.Aggiungi(new Oggetto(infoOggetto[1].Trim(), infoOggetto[2].Trim()));
												else if (infoOggetto.Length > 3)
												{
													if(infoOggetto[3].Trim() == "arma")
													{
														Impugnature imp;
														if(infoOggetto[5].Trim() == "unamano")
															imp = Impugnature.UnaMano;
														else if(infoOggetto[5].Trim() == "duemani")
															imp = Impugnature.DueMani;
														else
															imp = Impugnature.Nessuna;
														Mappa[int.Parse(st[0].Trim())].Inv.Aggiungi(new Arma(infoOggetto[1].Trim(), infoOggetto[2].Trim(), int.Parse(infoOggetto[4].Trim()), imp));
													}
													else if (infoOggetto[3].Trim() == "indumento")
													{
														TipoIndumento t;
														int bonusDef = int.Parse(infoOggetto[4].Trim());
														int bonusStam = int.Parse(infoOggetto[5].Trim());
														switch (infoOggetto[6].Trim())
														{
															case "elmo": t = TipoIndumento.Elmo; break;
															case "armatura": t = TipoIndumento.Armatura; break;
															case "falda": t = TipoIndumento.Falda; break;
															case "bracciere": t = TipoIndumento.Bracciere; break;
															case "scarpe": t = TipoIndumento.Scarpe; break;
															default: t = TipoIndumento.Generico; break;
														}
														Mappa[int.Parse(st[0].Trim())].Inv.Aggiungi(new Indumento(infoOggetto[1].Trim(), infoOggetto[2].Trim(), bonusDef, bonusStam, t));
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
                            _interlocutoreAttuale = "";
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
            if(_interlocutoreAttuale == n)
            {

                if (Mappa[IndiceStanza].Cose.Count != 0)
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

		private string CaricaInventarioAmbiente()
		{
			_invAmbiente.Items.Clear();
			_oggettiCoinvolti.Items.Clear();
			string s = "Per terra trovi : \n";
			foreach (Oggetto obj in Mappa[IndiceStanza].Inv.Oggetti)
			{
				s += "\t" + obj.Nome + "\n";
				_invAmbiente.Items.Add(obj);
				_oggettiCoinvolti.Items.Add(obj);
			}
			return s;
		}

		private void CaricaInventarioGiocatore()
		{
			_invGiocatore.Items.Clear();
			Inventario i = _giocatore.RitornaInventario();
			foreach (Oggetto obj in i.Oggetti)
			{
				_invGiocatore.Items.Add(obj);
			}
		}

		public string PrendiOggetto(Oggetto obj)
		{
			if (_oggettiCoinvolti.SelectedIndex != -1)
			{
				string s = _giocatore.Inv.Prendi(obj, Mappa[IndiceStanza].Inv);
				CaricaInventarioGiocatore();
				CaricaInventarioAmbiente();
				return s;
			}
			else
				return "Nessun oggetto selezionato!\n";
		}

		public string LasciaOggetto(Oggetto obj)
		{
			if (_invGiocatore.SelectedIndex != -1)
			{
				string s = _giocatore.Inv.Lascia(obj, Mappa[IndiceStanza].Inv);
				CaricaInventarioGiocatore();
				CaricaInventarioAmbiente();
				return s;
			}
			else
				return "Nessun oggetto selezionato!\n";
		}

		public string EliminaOggetto(Oggetto obj)
		{
			if (_invGiocatore.SelectedIndex != -1)
			{
				string s = _giocatore.Inv.Elimina(obj);
				CaricaInventarioGiocatore();
				CaricaInventarioAmbiente();
				return s;
			}
			else
				return "Nessun oggetto selezionato!\n";
		}

        public string Equipaggia(Oggetto obj)
        {
			string s = "";
			if (obj is Arma)
			{
				Arma a = (Arma)obj;
				s = _giocatore.EquipaggiaArma(a);
				CaricaArmi();
				CaricaInventarioGiocatore();
			}
			else if(obj is Indumento)
			{
				Indumento i = (Indumento)obj;
				s = _giocatore.EquipaggiaIndumento(i);
				CaricaIndumenti();
				CaricaInventarioGiocatore();
			}
			else
			{
				if (obj != null)
					return "Oggetto selezionato non equipaggiabile!\n";
				else
					return "Nessun oggetto selezionato!\n";
			}
			return s;
        }

		private void CaricaArmi()
		{
			_armi.Items.Clear();
			foreach (Arma a in _giocatore.ArmiEquipaggiate)
			{
				_armi.Items.Add(a);
			}
		}

		private void CaricaIndumenti()
		{
			_indumenti.Items.Clear();
			foreach (Indumento a in _giocatore.IndumentiEquipaggiati)
			{
				_indumenti.Items.Add(a);
			}
		}

		public string RiponiArma(Arma a)
		{
			if (a != null)
			{
				string s = _giocatore.DisequipaggiaArma(a);
				CaricaArmi();
				CaricaInventarioGiocatore();
				return s;
			}
			else
				return "Nessuna arma selezionata!\n";
		}

		public string RiponiIndumento(Indumento i)
		{
			if (i != null)
			{
				string s = _giocatore.DisequipaggiaIndumento(i);
				CaricaIndumenti();
				CaricaInventarioGiocatore();
				return s;
			}
			else
				return "Nessun indumento selezionato!\n";
		}

		public string Combattimento(Combattente c)
		{
			int res;
			string s = _giocatore.Combatti(c, out res);
			if (res == 1)
				_inCombattimento = true;
			else if (res == 0)
				_inCombattimento = false;
			else
				FineAvventura();
			return s;
		}

		private void FineAvventura()
		{
			MessageBox.Show("La tua avventura finisce qui!");
		}
	}
}
