using sql.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace sql.Views
{
    public class Laikas
    {
        public int Valanda { get; set; }
        public int Minute { get; set; }
        public Laikas() { }
        public Laikas(int v, int m)
        {
            Valanda = v;
            Minute = m;
        }
        public Laikas Add(Laikas demuo1)
        {
            Valanda += demuo1.Valanda;
            Minute += demuo1.Minute;
            if (Minute >= 60)
            {
                Valanda++;
                Minute -= 60;
            }
            if (Valanda == 24)
            {
                Valanda = 00;
            }
            return this;
        }
        public override string ToString()
        {
            string h = "";
            string min = "";
            if (Valanda < 10)
            {
                h = "0" + Valanda.ToString();
            }
            else
            {
                h = Valanda.ToString();
            }
            if (Minute < 10)
            {
                min = "0" + Minute.ToString();
            }
            else
            {
                min = Minute.ToString();
            }
            return h + ":" + min;
        }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Stoteles : ContentPage
    {
        public ObservableCollection<Data> Items = new ObservableCollection<Data>();
        private Transportas _Marsrutas;
        private Color _Color;
        private bool _ArSuLaikais;
        public Stoteles(Transportas Marsrutas, Color color, bool ArSuLaikais, string pradinisLaikas)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            _ArSuLaikais = ArSuLaikais;
            _Color = color;
            _Marsrutas = Marsrutas;
            Laikas laikas = new Laikas();
            List<string> Stoteless = new List<string>();
            List<string> Laikai = new List<string>();
            Stoteless = ParseString(Marsrutas.Stoteles);
            if (ArSuLaikais)
            {
                laikas = GetLaikas(pradinisLaikas);
                Laikai = ParseString(Marsrutas.LaikuSkirtumai);
            }
            string TheHeader = Marsrutas.PradineGalutineStotele;

            for (int i = 0; i < Stoteless.Count; i++)
            {
                if (ArSuLaikais)
                {
                    if (i > 0)
                    {
                        laikas.Add(GetLaikas(Laikai[i - 1]));
                    }
                    Items.Add(new Data() { Id = i, Primary = Stoteless[i], Secondary = " " + laikas.ToString() });
                }
                else
                {
                    Items.Add(new Data() { Primary = " " + Stoteless[i], Secondary = "" });
                }
            }
            MyListView.ItemsSource = Items;
            MyListView.BackgroundColor = color;
            BackgroundColor = color;
            MyListView.Header = TheHeader;
            MyListView.SeparatorColor = color;
            MyListView.Footer = "";
        }
        private Laikas GetLaikas(string laikas)
        {
            char[] separator = { ':' };
            string[] pasirinktasLaikas = laikas.Split(separator);
            if (pasirinktasLaikas.Count() > 1)
            {
                return new Laikas(int.Parse(pasirinktasLaikas[0]), int.Parse(pasirinktasLaikas[1]));
            }
            return new Laikas();
        }
        private List<string> ParseString(string eilute)
        {
            char[] sep = { ';' };
            List<string> elementai = eilute.Split(sep).ToList();
            return elementai;
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!_ArSuLaikais)
            {
                if (e.Item == null)
                    return;
                Data text = e.Item as Data;
                await Navigation.PushAsync(new Laikai(text, _Color, _Marsrutas, true));
                //Deselect Item
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}