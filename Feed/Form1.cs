using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using MongoDB.Driver;
using System.Net;
using MongoDB.Bson;
using System.Threading;

namespace Feed
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LogWpis(string text)
        {
            richTextBox1.BeginInvoke(new Action(() =>
            {
                richTextBox1.Text += text.ToString() + Environment.NewLine;
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
                richTextBox1.ScrollToCaret();
            }));
        }

        public static MongoClientSettings mongoClientSettings = new MongoClientSettings
        {
            //serwer adress
            Server = new MongoServerAddress("127.0.0.1", 27017),
            MaxConnectionPoolSize = 100000 // Maksymalny rozmiar puli połączeń
        };

        public static MongoClient dbClient = new MongoClient(mongoClientSettings);
        
        //dabaase to save
        public static string BazaDanych_Feed_ALL = "Feeddb";

        static bool IsUdpPortAvailable(int port)
        {
            try
            {
                using (var udpClient = new UdpClient(port))
                {
                    return true;
                }
            }
            catch (SocketException)
            {
                return false;
            }
        }

        static async Task WyslijPolecenieDoAPI(string Polecenie)
        {
            using (var client = new WebClient())
            {
                await client.DownloadStringTaskAsync(Polecenie);
                Thread.Sleep(2);
            }
        }

        private void button_Otworz_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox_Sciezka.Text = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    LogWpis(ex.ToString());
                }
            }
        }

        private void Rejestruj()
        {
            if (textBox_Sciezka.Text != string.Empty || textBox_IleSymboliNaPort.Text != string.Empty)
            {
                if (backgroundWorker_rejestruj.IsBusy == false)
                {
                    backgroundWorker_rejestruj.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show("Nie podano wszystkich parametrów.");
            }
        }

        private void buttonRejestracja_Click(object sender, EventArgs e)
        {
            Rejestruj();
        }

        private async void backgroundWorker_rejestruj_DoWork(object sender, DoWorkEventArgs e)
        {
            //Zablokowanie okienek
            groupBox1.BeginInvoke(new Action(() =>
            {
                groupBox1.Enabled = false;
            }));

            int basePort = 3000;
            int LicznikSymboli = 1;
            List<int> listaPortow = new List<int>();
            int licznik = 1;
            int Port = 0;
            int IleSymboliNaPort = Convert.ToInt32(textBox_IleSymboliNaPort.Text);

            foreach (var Symbol in File.ReadLines(textBox_Sciezka.Text))
            {
                try
                {
                    if (Port != basePort)
                    {
                        listaPortow.Add(basePort);
                    }

                    Port = basePort;

                    while (!IsUdpPortAvailable(Port))
                    {
                        Port++;
                    }

                    //-------------------------------------------------------
                    LogWpis(licznik.ToString() + ": Rejestruje TOS, L1, L2 dla " + Symbol + " na porcie " + Port.ToString());

                    //Rejestracja do TOS
                    string rejestracjaTOS = "http://localhost:8081/Register?symbol=" + Symbol + "&feedtype=TOS";

                    await WyslijPolecenieDoAPI(rejestracjaTOS);

                    //Tworzenie komendy skierowana danych z TOS na port
                    string TOS_ON = "http://localhost:8081/SetOutput?symbol=" + Symbol + "&feedtype=TOS&output=" + Port + "&status=on";

                    await WyslijPolecenieDoAPI(TOS_ON);

                    //-------------------------------------------------------
                    //Rejestracja do L1
                    string rejestracjaL1 = "http://localhost:8081/Register?symbol=" + Symbol + "&feedtype=L1";

                    await WyslijPolecenieDoAPI(rejestracjaL1);

                    //Tworzenie komendy skierowana danych z L1 na port
                    string L1_ON = "http://localhost:8081/SetOutput?symbol=" + Symbol + "&feedtype=L1&output=" + Port + "&status=on";

                    await WyslijPolecenieDoAPI(L1_ON);

                    //-------------------------------------------------------
                    //Rejestracja L2
                    string rejestracjaL2 = "http://localhost:8081/Register?symbol=" + Symbol + "&feedtype=L2";

                    await WyslijPolecenieDoAPI(rejestracjaL2);

                    //Tworzenie komendy skierowana danych z TOS na port
                    string L2_ON = "http://localhost:8081/SetOutput?symbol=" + Symbol + "&feedtype=L2&output=" + Port + "&status=on";
                    await WyslijPolecenieDoAPI(L2_ON);

                    licznik++;
                    LicznikSymboli++;

                    if (LicznikSymboli % IleSymboliNaPort == 1)
                    {
                        basePort = Port + 1;
                    }

                }
                catch (Exception ex2)
                {
                    LogWpis(ex2.ToString());
                    MessageBox.Show(ex2.ToString());
                    continue;
                }
            }

            foreach (var item in listaPortow)
            {
                try
                {
                    ThreadPool.QueueUserWorkItem(async a => await ListenToPortAsync(item));
                    LogWpis("Uruchomiono nasłuch na porcie: " + item);
                }
                catch (Exception ex3)
                {
                    MessageBox.Show(ex3.ToString());
                    MessageBox.Show("Port zajęty. Prawdopodobnie w czasie procesu rejestracji inna aplikacja uruchomiła nasłuch. Zrestartuj Ppro8 i zarejestruj symbole jeszcze raz.");
                    LogWpis(ex3.ToString());
                    continue;
                }
            }

            Port++;

            while (!IsUdpPortAvailable(Port))
            {
                Port++;
            }

            groupBox1.BeginInvoke(new Action(() =>
            {
                groupBox1.Enabled = true;
            }));
        }

        async Task ListenToPortAsync(int port)
        {
            using (var udpClient = new UdpClient(port))
            {
                while (true)
                {
                    try
                    {
                        // Odbieranie danych
                        var result = await udpClient.ReceiveAsync();
                        udpClient.Client.ReceiveBufferSize = 4194304;//1310720;
                        string data = Encoding.ASCII.GetString(result.Buffer);

                        // Przetwarzanie danych i zapis do bazy danych
                        await ProcessAndSaveDataAsync(data);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        LogWpis(ex.ToString());
                        continue;
                    }
                }
            }
        }

        async Task ProcessAndSaveDataAsync(string data)
        {
            try
            {
                Dictionary<string, string> parsedData = ParseData(data);
                var dbFeedAll = dbClient.GetDatabase(BazaDanych_Feed_ALL);

                string message;
                if (!parsedData.TryGetValue("Message", out message))
                {
                    return;
                }

                switch (message)
                {
                    case "TOS":
                        await ProcessTOS(parsedData, dbFeedAll);
                        break;
                    case "L1":
                        ProcessL1(parsedData, dbFeedAll);
                        break;
                    //send with L1 Data. Garbage
                    case "AuctionInfo":
                        ProcessAuctionInfo(parsedData, dbFeedAll);
                        break;
                    case "L2":
                        await ProcessL2(parsedData, dbFeedAll);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogWpis(ex.ToString());
            }
        }

        private Dictionary<string, string> ParseData(string data)
        {
            string[] splitData = data.Split(',', '=');
            Dictionary<string, string> parsedData = new Dictionary<string, string>();

            for (int i = 0; i < splitData.Length - 1; i += 2)
            {
                parsedData[splitData[i]] = splitData[i + 1];
            }

            return parsedData;
        }
      
        private BsonDateTime GetDateTime(Dictionary<string, string> data, string key)
        {
            if (data.TryGetValue(key, out string dateTimeString))
            {
                return DateTime.TryParse(dateTimeString, out DateTime dateTime)
                    ? dateTime
                    : DateTime.MinValue;
            }

            return DateTime.MinValue;
        }

        private double GetDouble(Dictionary<string, string> data, string key)
        {
            if (data.TryGetValue(key, out string valueString))
            {
                return double.TryParse(valueString.Replace('.', ','), out double value)
                    ? value
                    : 0;
            }

            return 0;
        }

        private string GetString(Dictionary<string, string> data, string key)
        {
            return data.TryGetValue(key, out string value) ? value : string.Empty;
        }

        private async Task ProcessTOS(Dictionary<string, string> data, IMongoDatabase db)
        {
            if (data["Type"] == "0")
            {
                var document = new Feed_TOS
                {
                    localTime = Convert.ToDateTime(data["LocalTime"]),
                    marketTime = Convert.ToDateTime(data["MarketTime"]),
                    message = data["Message"],
                    symbol = data["Symbol"],
                    type = data["Type"],
                    price = double.Parse(data["Price"].Replace('.', ',')),
                    size = Convert.ToInt32(data["Size"]),
                    source = data["Source"],
                    condition = data["Condition"],
                    tick = data["Tick"],
                    mmid = data["Mmid"],
                    sub_market_id = data["SubMarketId"]
                };

                var collection_TOS = db.GetCollection<Feed_TOS>(document.symbol);
                await collection_TOS.InsertOneAsync(document);
            }
        }

        private void ProcessL1(Dictionary<string, string> data, IMongoDatabase db)
        {
            var document = new Feed_L1
            {
                localTime = Convert.ToDateTime(data["LocalTime"]),
                marketTime = Convert.ToDateTime(data["MarketTime"]),
                message = data["Message"],
                symbol = data["Symbol"],
                bidPrice = Convert.ToDouble(data["BidPrice"].Replace('.', ',')),
                bidSize = Convert.ToInt32(data["BidSize"]),
                askPrice = Convert.ToDouble(data["AskPrice"].Replace('.', ',')),
                askSize = Convert.ToInt32(data["AskSize"]),
                tick = data["Tick"].Replace("\n", "")
            };

            var collection_L1 = db.GetCollection<Feed_L1>(document.symbol);
            collection_L1.InsertOne(document);
        }

        private void ProcessAuctionInfo(Dictionary<string, string> data, IMongoDatabase db)
        {
            var document = new Feed_AuctionInfo
            {
                localTime = Convert.ToDateTime(data["LocalTime"]).ToUniversalTime(),
                marketTime = data["MarketTime"],
                message = data["Message"],
                symbol = data["Symbol"],
                indicativePrice = Convert.ToDouble(data["IndicativePrice"].Replace('.', ',')),
                indicativeVolume = Convert.ToInt32(data["IndicativeVolume"]),
                auctionType = data["AuctionType"].Replace("\n", "")
            };

            var collection_AuctionInfo = db.GetCollection<Feed_AuctionInfo>(document.symbol);
            collection_AuctionInfo.InsertOne(document);
        }

        private async Task ProcessL2(Dictionary<string, string> data, IMongoDatabase dbFeedAll)
        {
            var document = new Feed_L2
            {
                localTime = Convert.ToDateTime(data["LocalTime"]),
                marketTime = Convert.ToDateTime(data["MarketTime"]),
                message = data["Message"],
                symbol = data["Symbol"],
                mmid = data["Mmid"],
                side = data["Side"],
                price = Convert.ToDouble(data["Price"].Replace('.', ',')),
                volume = Convert.ToInt32(data["Volume"]),
                depth = Convert.ToInt32(data["Depth"]),
                sequenceNumber = Convert.ToInt32(data["SequenceNumber"])
            };

            var collection_L2 = dbFeedAll.GetCollection<Feed_L2>(document.symbol);
            await collection_L2.InsertOneAsync(document);
        }

        private string GetMarket(string symbol)
        {
            string[] symbolParts = symbol.Split('.');
            return symbolParts[symbolParts.Length - 1];
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            //wczytanie ustawień
            textBox_Sciezka.Text = Properties.Settings.Default.Sciezka;
            textBox_IleSymboliNaPort.Text = Properties.Settings.Default.IleSymboliNaPort.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Sciezka = textBox_Sciezka.Text;
            Properties.Settings.Default.IleSymboliNaPort = Convert.ToInt32(textBox_IleSymboliNaPort.Text);

            Properties.Settings.Default.Save();
        }      
    }
}
