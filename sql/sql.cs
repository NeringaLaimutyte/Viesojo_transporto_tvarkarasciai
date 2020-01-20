using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using SQLite;
using SQLitePCL;
using System.IO;
using sql.Views;
using sql.Models;

namespace sql
{
    public class App : Application
    {
        // The file name of database to be saved in the application writable directory.
        const string databaseFileName = "SQLite3.db3";
        // The file name of database to be saved in the application writable directory.
        static SQLiteConnection dbConnection;
        static string databasePath;

        // The file name of database to be saved in the application writable directory.
        const string databaseFileNameStoteles = "SQLite3Stoteles.db3";
        // The file name of database to be saved in the application writable directory.
        static SQLiteConnection dbConnectionStoteles;
        static string databasePathStoteles;

        // The file name of database to be saved in the application writable directory.
        const string databaseFileNameLaikai = "SQLite3Laikai.db3";
        // The file name of database to be saved in the application writable directory.
        static SQLiteConnection dbConnectionLaikai;
        static string databasePathLaikai;
        public static List<Transportas> GetAutobusai(string transportoPriemone)
        {
            var itemList = dbConnection.Table<Transportas>();
            List<Transportas> dbList = new List<Transportas>();
            // Add to itemList to be used as ItemsSource in a ListView.
            foreach (var item in itemList)
            {
                if (item.Priemone.Equals(transportoPriemone))
                {
                    dbList.Add(item);
                }
            }
            return dbList;
        }
        public App()
        {
            // Initiate database first.
            InitiateSQLite();
            MainPage = new NavigationPage(new Main());
            //MainPage = new NavigationPage(new Main()) { BarTextColor = Color.White, BackgroundImage = "Backgound.jpg", BarBackgroundColor = Color.Transparent, BackgroundColor = Color.Transparent };
        }
        public void InitiateSQLite()
        {
            bool needCreateTable = true;
            bool needCreateTableStoteles = false;
            bool needCreateTableLaikai = false;
            // Need to initialize SQLite
            raw.SetProvider(new SQLite3Provider_sqlite3());
            raw.FreezeProvider(true);
            // Get writable directory info for this app.
            string dataPath = global::Tizen.Applications.Application.Current.DirectoryInfo.Data;
            // Combine with database path and name
            databasePath = Path.Combine(dataPath, databaseFileName);
            databasePathStoteles = Path.Combine(dataPath, databaseFileNameStoteles);
            databasePathLaikai = Path.Combine(dataPath, databaseFileNameLaikai);
            // Check the database file to decide table creation.
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
                needCreateTableStoteles = true;
            }
            if (!File.Exists(databasePathStoteles))
            {
                needCreateTableStoteles = true;
            }
            if (!File.Exists(databasePathLaikai))
            {
                needCreateTableLaikai = true;
            }
            dbConnection = new SQLiteConnection(databasePath);
            dbConnectionStoteles = new SQLiteConnection(databasePathStoteles);
            dbConnectionLaikai = new SQLiteConnection(databasePathLaikai);
            if (needCreateTable)
            {
                dbConnection.CreateTable<Transportas>();
            }
            if (needCreateTableStoteles)
            {
                dbConnectionStoteles.CreateTable<Stotele>();
            }
            if (needCreateTableLaikai)
            {
                dbConnectionLaikai.CreateTable<Laikasssss>();
            }
            InsertTransportas();
        }
        /// <summary>
        /// "A" - Autobusai, "T" - Troleibusai, "M" - Maršrutiniai taksi, "R" - Tarpmiestiniai autobusai
        /// </summary>
        private void InsertTransportas()
        {
            Transportas t = new Transportas
            {
                Numeris = 3.ToString(),
                Stoteles = "Kauno klinikinė ligoninė;Naujakurių g.;Apuolės g.;Žiemgalių g.;Baltų pr.;Žemaičių pl.;Mosėdžio g.;9 - ojo Forto g.;Kuršių g.;Šarkuvos g.;Jotvingių g.;A.Strazdo g.;Kuršėnų g.;Varnių tiltas;Jonavos g.;Nuokalnės g.;V.Lašo g.;Klinikos;Akių klinika;Žeimenos g.;Topolis;Ukmergės g.;Ašigalio g.;S.Žukausko g.;Eiguliai;Čečėnijos aikštė;V.Landsbergio - Žemkalnio g.;Pakraščio g.;Turgavietė;Rėda;Draugystės parkas;Birželio 23 - iosios g.;Taikos pr.;Studentų g.;Zoologijos sodas;Gėlių rato g.;Sporto g.;Kęstučio g.;Griunvaldo g.;Autobusų stotis;Geležinkelio stotis;Kaunakiemio g.;Geležinkelio tiltas;Geležinkelio g.;Šančių poliklinika;Švč.Jėzaus Širdies bažnyčia;Gudų g.;Šančių kapinės;Tilto g.;Mažoji g.;A.Smetonos al.;Gailutės g.;J.Staugaičio g.;Kurtinių g.;Volungių g.;Onkologijos ligoninė",
                PritaikytaNeigaliesiems = false,
                LaikuSkirtumai = "00:01;00:02;00:02;00:01;00:01;00:02;00:01;00:01;00:01;00:01;00:02;00:01;00:01;00:02;00:02;00:02;00:01;00:02;00:01;00:01;00:02;00:01;00:01;00:01;00:03;00:01;00:01;00:02;00:01;00:01;00:01;00:03;00:01;00:01;00:01;00:02;00:03;00:01;00:01;00:01;00:02;00:02;00:02;00:02;00:01;00:01;00:01;00:02;00:02;00:01;00:01;00:01;00:03;00:01;00:01;",
                PradinesStotelesLaikai = "05:28;05:51;06:04;06:16;06:31;06:46;06:59;07:12;07:28;07:44;08:03;08:23;08:43;09:04;09:24;09:43;10:01;10:21;10:41;11:01;11:19;11:38;11:57;12:17;12:37;12:55;13:12;13:29;13:46;14:02;14:18;14:34;14:50;15:06;15:21;15:36;15:53;16:09;16:26;16:42;16:59;17:16;17:33;17:51;17:09;18:34;18:58;19:22;19:48;20:19;20:50;21:41",
                PradinesStotelesLaikaiSavaitgaliais = "06:25;07:04;07:28;07:50;08:10;08:35;08:58;09:23;09:54;10:16;10:45;11:17;11:44;12:03;12:23;12:44;13:08;13:34;13:53;14:14;14:37;15:00;15:19;15:39;16:02;16:25;16:51;17:19;18:09;18:46;19:11;19:37;20:16;20:43",
                SavaitesDienos = "P A T K P Š S",
                Priemone = "A"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);
            t = new Transportas
            {
                Numeris = 6.ToString(),
                Stoteles = "Technikos g.;Draugystės g.;Gamyklos;Kauno gelžbetonis;Urmas;Kauno kolegija;V. Krėvės pr.;Turgavietė;Kalniečių poliklinika;Baldų parduotuvė;Ortopedijos technika;Mituvos g.;Kauno aklųjų ir silpnaregių centras;K. Petrausko g.;Ledo arena;Sporto g.;Gedimino g.;S. Daukanto g.;Muzikinis teatras;Karaliaus Mindaugo pr.;Vytauto Didžiojo tiltas;Aleksoto poliklinika;S.Dariaus ir S.Girėno aerodromas;Europos pr.;Julijanavos g.;Lazdijų g.;2-asis fortas;Sodai;K. Dulksnio g.;Ramybės g.;Jonučiai;Vienybės g.;Tulpių g.;Bažnyčia;Biblioteka;A. Mitkaus pagrindinė mokykla;Rinkūnai",
                PritaikytaNeigaliesiems = false,
                LaikuSkirtumai = "0:01;0:01;0:02;0:02;0:02;0:02;0:02;0:02;0:01;0:01;0:02;0:02;0:03;0:01;0:02;0:03;0:01;0:01;0:03;0:02;0:02;0:02;0:01;0:01;0:02;0:02;0:01;0:01;0:01;0:02;0:01;0:02;0:02;0:01;0:01;0:01",
                PradinesStotelesLaikai = "05:23;06:04;06:33;06:45;07:00;07:14;07:30;07:42;07:54;08:06;08:20;08:39;08:55;09:25;09:39;10:14;10:53;11:20;11:51;12:13;12:34;13:02;13:30;13:48;14:02;14:15;14:29;14:43;14:57;15:12;15:32;15:53;16:14;16:31;16:56;17:12;17:31;18:09;18:32;19:02;19:25;19:49;20:27;20:56;21:47;22:46",
                PradinesStotelesLaikaiSavaitgaliais = "",
                SavaitesDienos = "P A T K P",
                Priemone = "A"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);
            t = new Transportas
            {
                Numeris = "6A",
                Stoteles = "Respublikinė Kauno ligoninė;Karių kapinės;Ašmenos 1-oji g.;Žarėnų g.;Prancūzų g.;Breslaujos g.;Dainų slėnis;Viadukas;Geležinkelio stotis;Geležinkelio stotis;Autobusų stotis;Griunvaldo g.;Gedimino g.;S. Daukanto g.;Muzikinis teatras;Karaliaus Mindaugo pr.;Vytauto Didžiojo tiltas;Aleksoto poliklinika;S.Dariaus ir S.Girėno aerodromas;Europos pr.;Julijanavos g.;Lazdijų g.;2-asis fortas;Sodai;K. Dulksnio g.;Ramybės g.;Jonučiai;Vienybės g.;Tulpių g.;Bažnyčia;Biblioteka;A. Mitkaus pagrindinė mokykla;Rinkūnai",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai = "0:01;0:01;0:01;0:02;0:02;0:03;0:01;0:01;0:01;0:01;0:01;0:02;0:01;0:01;0:03;0:02;0:02;0:02;0:01;0:01;0:02;0:02;0:01;0:01;0:01;0:02;0:01;0:01;0:02;0:01;0:01;0:01",
                PradinesStotelesLaikai = "",
                PradinesStotelesLaikaiSavaitgaliais = "06:31;07:23;07:51;08:25;09:02;09:30;09:53;10:30;11:00;11:35;12:09;12:28;12:51;13:14;13:49;14:24;14:56;15:28;15:48;16:15;16:43;17:11;17:38;17:50;18:27;19:06;19:40;20:15;20:47;21:17;21:47;22:08",
                SavaitesDienos = "Š S",
                Priemone = "A"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);




            //t = new Transportas
            //{
            //    Numeris = 19.ToString(),
            //    Stoteles = "Skubios pagalbos centras;Žeimenos g.;Topolis;Ukmergės g.;Ašigalio g.;K. Škirpos g.;P. Plechavičiaus g.;Martyno Mažvydo pagrindinė mokykla;V. Landsbergio-Žemkalnio g.;Pakraščio g.;Turgavietė;V. Krėvės pr.;Kauno saulėtekis;Technikos profesinio mokymo centras;Garažų g.;Regitra;Elektrinė;Kitron;Upytės g.;Ateities pl.;Naktigonės g.;Pervaža;Šilėnų g.;S. Nėries muziejus;Parko g.;Palemono keramika",
            //    PritaikytaNeigaliesiems = true,
            //    LaikuSkirtumai = "0:02;0:01;0:01;0:01;0:01;0:01;0:01;0:01;0:01;0:01;0:02;0:02;0:02;0:01;0:02;0:01;0:02;0:02;0:02;0:01;0:02;0:02;0:01;0:01;0:01",
            //    PradinesStotelesLaikai = "04:43;05:43;06:29;06:52;07:26;08:07;08:45;09:30;10:49;11:21;12:06;13:13;13:49;14:50;15:27;16:30;17:22;18:14;19:31;20:16;21:31",
            //    PradinesStotelesLaikaiSavaitgaliais = "",
            //    SavaitesDienos = "P A T K P",
            //    Priemone = "A"
            //};
            //t.PradineGalutineStoteleSet();
            //dbConnection.Insert(t);




            t = new Transportas
            {
                Numeris = 1.ToString(),
                Stoteles = "Islandijos pl.;S. Žukausko g.;Ašigalio g.;Ukmergės g.;Topolis;Žeimenos g.;Akių klinika;Eivenių g.;P. Dovydaičio g.;Utenos g.;Aukštaičių g.;Žemaičių g.;E. Ožeškienės g.;L. Sapiegos g.;Studentų skveras;Gedimino g.;Kęstučio g.;Griunvaldo g.;Autobusų stotis;Geležinkelio stotis;Kaunakiemio g.;Geležinkelio tiltas;Geležinkelio g.;Šančių poliklinika;Švč. Jėzaus Širdies bažnyčia;Gudų g.;Šančių kapinės;Tilto g.;Smėlio g.;KTU inžinerijos licėjus;Birutės g.;Vaidoto g.",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai= "0:02;0:01;0:01;0:02;0:01;0:01;0:01;0:01;0:01;0:02;0:01;0:03;0:01;0:02;0:01;0:02;0:01;0:01;0:01;0:02;0:01;0:02;0:02;0:01;0:01;0:01;0:02;0:01;0:01;0:01;0:01",
                PradinesStotelesLaikai= "04:13;04:30;04:47;04:58;05:14;05:30;05:45;06:02;06:13;06:24;06:35;06:45;06:55;07:04;07:13;07:21;07:31;07:41;07:52;08:04;08:16;08:27;08:39;08:51;09:02;09:14;09:26;09:38;09:49;10:00;10:10;10:20;10:30;10:41;10:52;11:03;11:14;11:26;11:38;11:50;12:01;12:12;12:22;12:31;12:42;12:52;13:02;13:10;13:19;13:29;13:38;13:51;14:01;14:13;14:23;14:33;14:42;14:50;14:59;15:07;15:16;15:29;15:40;15:51;16:02;16:14;16:25;16:36;16:49;17:02;17:19;17:33;17:46;17:59;18:14;18:28;18:44;18:58;19:11;19:26;19:40;19:52;20:07;20:21;20:37;20:53;21:09;21:28;21:47;22:06;22:23",
                PradinesStotelesLaikaiSavaitgaliais= "05:16;05:42;05:56;06:14;06:32;06:50;07:04;07:20;07:35;07:50;08:03;08:20;08:38;08:53;09:16;09:33;09:55;10:09;10:27;10:43;10:58;11:17;11:34;11:50;12:08;12:25;12:41;12:55;13:08;13:25;13:38;13:54;14:10;14:23;14:40;14:53;15:10;15:24;15:40;15:55;16:09;16:27;16:43;17:03;17:23;17:39;17:53;18:13;18:31;18:47;19:07;19:23;19:42;19:55;20:14;20:28;20:46;21:02;21:21;21:36;22:03;22:24",
                SavaitesDienos = "P A T K P Š S",
                Priemone = "T"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);
            t = new Transportas
            {
                Numeris = 2.ToString(),
                Stoteles = "Islandijos pl.;S. Žukausko g.;Ašigalio g.;Ukmergės g.;Topolis;Žeimenos g.;Akių klinika;Eivenių g.;P. Dovydaičio g.;K. Petrausko g.;Ledo arena;Sporto g.;Gedimino g.;S. Daukanto g.;Muzikinis teatras;Vilniaus g.;Kauno pilis",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai = "0:02;0:01;0:01;0:02;0:01;0:01;0:01;0:01;0:02;0:01;0:02;0:03;0:02;0:01;0:02;0:03;0:02;0:03;0:02;0:01;0:02;0:01;0:01;0:01;0:02;0:01;0:02;0:01;0:01",
                PradinesStotelesLaikai = "05:04;05:25;05:49;06:08;06:29;06:48;07:00;07:16;07:28;07:38;07:57;08:13;08:30;08:42;08:53;09:08;09:30;09:46;09:56;10:18;10:37;10:55;11:05;11:22;11:44;11:58;12:08;12:27;12:50;13:05;13:15;13:33;13:54;14:10;14:29;14:45;15:03;15:13;15:23;15:35;15:49;16:08;16:19;16:31;16:43;16:55;17:12;17:26;17:40;17:53;18:10;18:33;18:50;19:06;19:20;19:44;19:59;20:15;20:29;20:43;20:58;21:13;21:34;21:51;22:05;22:18;22:35;22:51",
                PradinesStotelesLaikaiSavaitgaliais = "",
                SavaitesDienos = "P A T K P",
                Priemone = "T"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);

            //taisyt
            t = new Transportas
            {
                Numeris = 4.ToString(),
                Stoteles = "Islandijos pl.;S. Žukausko g.;Ašigalio g.;Ukmergės g.;Topolis;Žeimenos g.;Akių klinika;Eivenių g.;P. Dovydaičio g.;K. Petrausko g.;Ledo arena;Sporto g.;Kęstučio g.;Griunvaldo g.;Autobusų stotis;Geležinkelio stotis;Kaunakiemio g.;Geležinkelio tiltas;Geležinkelio g.;Šančių poliklinika;Švč. Jėzaus Širdies bažnyčia;Gudų g.;Šančių kapinės;Tilto g.;Smėlio g.;KTU inžinerijos licėjus;Birutės g.;Vaidoto g.",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai = "0:02;0:01;0:01;0:02;0:01;0:01;0:01;0:01;0:02;0:01;0:02;0:03;0:01;0:01;0:01;0:02;0:01;0:02;0:02;0:01;0:01;0:01;0:03;0:01;0:01;0:01;0:01",
                PradinesStotelesLaikai = "04:08;04:39;05:07;05:41;05:57;06:21;06:38;06:50;07:07;07:25;07:45;08:09;08:32;08:57;09:19;09:43;10:04;10:24;10:47;11:08;11:32;11:54;12:16;12:36;12:56;13:25;13:43;14:04;14:18;14:35;14:54;15:12;15:26;15:54;16:05;16:28;16:52;17:14;17:39;18:03;18:19;18:40;19:03;19:23;19:36;20:00;20:24;20:47;21:03;21:20;21:43;22:01;22:26",
                PradinesStotelesLaikaiSavaitgaliais = "05:11;05:34;06:00;06:25;06:44;07:14;07:39;08:14;08:32;09:07;09:45;10:21;10:39;11:11;11:29;12:02;12:20;12:52;13:18;13:47;14:05;14:34;15:03;15:33;15:49;16:19;16:53;17:29;18:04;18:24;19:00;19:36;20:07;20:37;21:12;21:47;22:15",
                SavaitesDienos = "P A T K P Š S",
                Priemone = "T"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);
            t = new Transportas
            {
                Numeris = 5.ToString(),
                Stoteles = "Petrašiūnai;Elektros tinklai;Armatūrininkų g.;Kausta;Ketaus liejykla;Petrašiūnų mokymo centras;6-asis fortas;Pašilės g.;Breslaujos g.;Dainų slėnis;Viadukas;Geležinkelio stotis;Autobusų stotis;Griunvaldo g.;Gedimino g.;S. Daukanto g.;Muzikinis teatras;Vilniaus g.;Kauno pilis;A. Kriščiukaičio g.;Neries krantinė;K. Griniaus g.;Bijūnų g.;Vilijampolės turgavietė;Panerių g.;Varnių g.",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai = "0:01;0:01;0:02;0:02;0:01;0:02;0:01;0:01;0:03;0:01;0:02;0:01;0:01;0:02;0:02;0:01;0:02;0:03;0:02;0:01;0:01;0:01;0:03;0:02;0:02",
                PradinesStotelesLaikai = "04:50;05:09;05:28;05:43;06:05;06:17;06:29;06:41;06:54;07:07;07:18;07:28;07:42;07:55;08:08;08:21;08:38;08:55;09:10;09:27;09:39;09:53;10:09;10:17;10:31;10:48;11:05;11:21;11:32;11:43;12:00;12:09;12:22;12:36;12:52;13:07;13:23;13:35;13:52;14:04;14:17;14:28;14:39;14:51;15:05;15:17;15:29;15:46;15:53;16:06;16:17;16:35;16:48;17:05;17:19;17:43;18:08;18:27;18:48;19:06;19:24;19:41;19:55;;20:15;20:37;20:57;21:20;21:38;21:52;22:16;22:41",
                PradinesStotelesLaikaiSavaitgaliais = "05:48;06:16;06:40;07:04;07:21;07:37;07:54;08:10;08:27;08:47;09:07;09:27;09:48;10:03;10:21;10:43;11:02;11:16;11:30;11:44;12:02;12:20;12:40;12:56;13:17;13:33;13:47;14:01;14:18;14:35;14:55;15:13;15:25;15:37;15:54;16:11;16:28;16:47;17:08;17:33;17:59;18:20;18:43;18:58;19:17;19:40;20:03;20:23;20:43;21:04;21:25;21:42;22:04;22:22;22:41",
                SavaitesDienos = "P A T K P Š S",
                Priemone = "T"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);
            t = new Transportas
            {
                Numeris = 56.ToString(),
                Stoteles = "Tabariškiai;Ringaudai;Ringaudų pradinė mokykla;Gėlių g.;Varžupio g.;Akademijos miestelis;Žirgynas;Bublių g.;Požeminė perėja;Marvelė;Laiptai;Vytauto Didžiojo tiltas;Vytauto Didžiojo tiltas;Karaliaus Mindaugo pr.;Vilniaus g.;E. Ožeškienės g.;L. Sapiegos g.;Studentų skveras;Gedimino g.;Kęstučio g.;Griunvaldo g.;Autobusų stotis;Geležinkelio stotis;Viadukas;Dainų slėnis;Breslaujos g.;Pašilės g.;6-asis fortas;Petrašiūnų mokymo centras;Ketaus liejykla;Kausta;Kauno keliai;Amaliai;Sodai;Viržių g.;Naktigonės g.;Pervaža;Šilėnų g.;S. Nėries muziejus;Parko g.;Palemono keramika;Keramzito g.;Neveronių g.;Pervaža;Neveronys;Krašto g.;Neveronių žiedas",
                PritaikytaNeigaliesiems = false,
                LaikuSkirtumai = "0:02;0:01;0:01;0:02;0:01;0:03;0:01;0:01;0:01;0:01;0:01;0:01;0:01;0:02;0:01;0:01;0:01;0:01;0:01;0:01;0:00;0:01;0:02;0:01;0:02;0:01;0:01;0:02;0:01;0:02;0:01;0:02;0:01;0:00;0:01;0:01;0:01;0:01;0:00;0:01;0:01;0:00;0:03;0:01;0:06;0:01",
                PradinesStotelesLaikai = "05:50;06:25;07:00;07:45;08:15;08:50;09:25;10:10;10:40;11:15;12:30;13:00;13:50;14:25;14:55;15:45;16:10;16:45;17:15;18:20",
                PradinesStotelesLaikaiSavaitgaliais = "",
                SavaitesDienos = "P A T K P",
                Priemone = "M"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);


            //tais
            t = new Transportas
            {
                Numeris = 93.ToString(),
                Stoteles = "Tabariškiai;Ringaudai;Ringaudų pradinė mokykla;Gėlių g.;Varžupio g.;Akademijos miestelis;Žirgynas;Bublių g.;Požeminė perėja;Marvelė;Laiptai;Vytauto Didžiojo tiltas;Vytauto Didžiojo tiltas;Karaliaus Mindaugo pr.;Vilniaus g.;E. Ožeškienės g.;L. Sapiegos g.;Studentų skveras;Gedimino g.;Kęstučio g.;Griunvaldo g.;Autobusų stotis;Geležinkelio stotis;Viadukas;Dainų slėnis;Breslaujos g.;Pašilės g.;6-asis fortas;Petrašiūnų mokymo centras;Ketaus liejykla;Kausta;Kauno keliai;Amaliai;Sodai;Viržių g.;Naktigonės g.;Pervaža;Šilėnų g.;S. Nėries muziejus;Parko g.;Palemono keramika;Keramzito g.;Neveronių g.;Pervaža;Neveronys;Krašto g.;Neveronių žiedas",
                PritaikytaNeigaliesiems = false,
                LaikuSkirtumai = "0:02;0:01;0:01;0:02;0:01;0:03;0:01;0:01;0:01;0:01;0:01;0:01;0:01;0:02;0:01;0:01;0:01;0:01;0:01;0:01;0:00;0:01;0:02;0:01;0:02;0:01;0:01;0:02;0:01;0:02;0:01;0:02;0:01;0:00;0:01;0:01;0:01;0:01;0:00;0:01;0:01;0:00;0:03;0:01;0:06;0:01",
                PradinesStotelesLaikai = "05:50;06:25;07:00;07:45;08:15;08:50;09:25;10:10;10:40;11:15;12:30;13:00;13:50;14:25;14:55;15:45;16:10;16:45;17:15;18:20",
                PradinesStotelesLaikaiSavaitgaliais = "",
                SavaitesDienos = "P A T K P",
                Priemone = "M"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);
            t = new Transportas
            {
                Numeris = 88.ToString(),
                Stoteles = "Kauno autobusų stotis;Kovo 11-osios vidurinė mokykla;Kalniečių poliklinika;Biruliškės (kelias A1);Palemonas;Karčiupis;Jakštonys;Grabuciškės;Rumšiškių paviljonas;Pravieniškių geležinkelio stotis;Pravieniškių biblioteka;Pravieniškių sankryža;Pravieniškės;Pravieniškių žiedas",
                PritaikytaNeigaliesiems = false,
                LaikuSkirtumai = "0:06;0:09;0:03;0:04;0:02;0:02;0:03;0:06;0:05;0:03;0:03;0:01;0:03",
                PradinesStotelesLaikai = "09:30;13:30;16:00;18:30",
                PradinesStotelesLaikaiSavaitgaliais = "",
                SavaitesDienos = "P A T K P",
                Priemone = "M"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);
            t = new Transportas
            {
                Numeris = 189.ToString(),
                Stoteles = "Kauno autobusų stotis;Birštono g.;Kauno pilis;Ariogalos g.;Švyturio g.;Pikulo g.;Šiaurinio aplinkkelio tiltas;Panerys;Saliai;1-ieji Salių sodai;Radikių kapinaitės;Radikiai;Smiltynai;Smiltynų sodai;Lapių kryžkelė;Ginėnai;Masteikiai;Šančiai;Drąseikiai;Andruškoniai;Mažieji Žinėnai;Žinėnai;Batėgala;Batėgalos mokykla",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai = "0:03;0:02;0:02;0:02;0:02;0:03;0:01;0:01;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:03;0:02;0:02;0:01",
                PradinesStotelesLaikai = "06:05;08:10;13:30;15:35;18:05",
                PradinesStotelesLaikaiSavaitgaliais = "07:40;13:30;18:05",
                SavaitesDienos = "P A T K P Š S",
                Priemone = "R"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);


            //tais
            t = new Transportas
            {
                Numeris = 179.ToString(),
                Stoteles = "Kauno autobusų stotis;Švč. Jėzaus Širdies bažnyčia;Tilto g.;KTU inžinerijos licėjus;Rūko g.;Garšvės g.;Vaišvydava;Obelynas;19-tas kilometras;Elnių eiguva;Piliuonos kryžkelė;1-ieji sodai;S.b. „Technika“;2-ieji sodai;Tursonas;Viršužiglio reabilitacijos ligoninė;Arlaviškių kryžkelė;Kadagių slėnis;2-osios Arlaviškės;Arlaviškės",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai = "0:07;0:04;0:02;0:03;0:02;0:02;0:02;0:02;0:03;0:02;0:01;0:01;0:01;0:01;0:02;0:02;0:03;0:02;0:01;0:01;0:01",
                PradinesStotelesLaikai = "04:20;05:35;06:10;09:30;11:50;14:10;15:00;16:20;18:40;21:05",
                PradinesStotelesLaikaiSavaitgaliais = "06:10;09:30;11:50;16:20;18:40",
                SavaitesDienos = "P A T K P Š S",
                Priemone = "R"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);



            t = new Transportas
            {
                Numeris = 169.ToString(),
                Stoteles = "Kauno autobusų stotis;Birštono g.;Kauno pilis;Ariogalos g.;Švyturio g.;Pikulo g.;Šiaurinio aplinkkelio tiltas;Panerys;Saliai;1-ieji Salių sodai;Radikių kapinaitės;Radikiai;Smiltynai;Smiltynų sodai;Lapių kryžkelė;Ginėnai;Masteikiai;Šančiai;Drąseikiai;Andruškoniai;Mažieji Žinėnai;Žinėnai;Batėgala;Batėgalos mokykla",
                PritaikytaNeigaliesiems = true,
                LaikuSkirtumai = "0:03;0:02;0:02;0:02;0:02;0:03;0:01;0:01;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:02;0:03;0:02;0:02;0:01",
                PradinesStotelesLaikai = "06:05;08:10;13:30;15:35;18:05",
                PradinesStotelesLaikaiSavaitgaliais = "",
                SavaitesDienos = "P A T K P",
                Priemone = "R"
            };
            t.PradineGalutineStoteleSet();
            dbConnection.Insert(t);

            //t = new Transportas
            //{
            //    Numeris = 93.ToString(),
            //    Stoteles = "",
            //    PritaikytaNeigaliesiems = true,
            //    LaikuSkirtumai = "",
            //    PradinesStotelesLaikai = "",
            //    PradinesStotelesLaikaiSavaitgaliais = "",
            //    SavaitesDienos = "P A T K P Š S",
            //    Priemone = "M"
            //};
            //t.PradineGalutineStoteleSet();
            //dbConnection.Insert(t);




            //t = new Transportas
            //{
            //    Numeris = 93.ToString(),
            //    Stoteles = "",
            //    PritaikytaNeigaliesiems = true,
            //    LaikuSkirtumai = "",
            //    PradinesStotelesLaikai = "",
            //    PradinesStotelesLaikaiSavaitgaliais = "",
            //    SavaitesDienos = "P A T K P Š S",
            //    Priemone = "M"
            //};
            //t.PradineGalutineStoteleSet();
            //dbConnection.Insert(t);
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}