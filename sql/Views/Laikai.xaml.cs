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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Laikai : ContentPage
    {
        public ObservableCollection<Data> Items { get; set; }
        private Data _Stotele;
        private Color _Color;
        private Transportas _Marsrutas;
        private string _TheHeader;
        private bool _ArDarboDiena;
        private List<Laikas> _Laikas = new List<Laikas>();
        public Laikai(Data stotele, Color color, Transportas Marsrutas, bool ArDarboDiena)
        {
            _Stotele = stotele;
            _Marsrutas = Marsrutas;
            _Color = color;
            if (Marsrutas.PradinesStotelesLaikai.Length<1)
            {
                ArDarboDiena = false;
            }
            _ArDarboDiena = ArDarboDiena;
            List<string> stoteles = ParseString(Marsrutas.Stoteles);
            List<Laikas> skirtumai = ParseStringLaikai(Marsrutas.LaikuSkirtumai);
            int stotelesIndex = 0;
            for (int i = 0; i < stoteles.Count; i++)
            {
                if (stoteles[i].Equals(stotele.Primary.Trim()))
                {
                    stotelesIndex = i;
                }
            }
            if (_ArDarboDiena)
            {
                _Laikas = ParseStringLaikai(Marsrutas.PradinesStotelesLaikai);
            }
            else
            {
                _Laikas = ParseStringLaikai(Marsrutas.PradinesStotelesLaikaiSavaitgaliais);
            }
            Laikas skirtumasNuoPirmos = new Laikas(0, 0);
            for (int i = 0; i < stotelesIndex; i++)
            {
                skirtumasNuoPirmos.Add(skirtumai[i]);
            }
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            string TheHeader = stotele.Primary;
            _TheHeader = TheHeader;
            Items = new ObservableCollection<Data>();
            Laikas current = new Laikas(0, 0);
            foreach (var item in _Laikas)
            {
                current = new Laikas(item.Valanda, item.Minute);
                current = current.Add(skirtumasNuoPirmos);
                Items.Add(new Data() { Primary = " " + current.ToString(), Secondary = item.ToString() });
            }
            MyListView.ItemsSource = Items;
            MyListView.BackgroundColor = color;
            BackgroundColor = color;
            Data header;
            if (_ArDarboDiena)
            {
                header = new Data() { Primary = "Darbo diena ", Secondary = TheHeader };
            }
            else
            {
                header = new Data() { Primary = "Savaitgalis ", Secondary = TheHeader };
            }
            if (Marsrutas.PradinesStotelesLaikai.Length < 2)
            {
                header = new Data() { Primary = "Savaitgalis ", Secondary = TheHeader };
            }
            MyListView.Header = header;
            MyListView.SeparatorColor = color;
            MyListView.Footer = "";
            if (Marsrutas.PradinesStotelesLaikaiSavaitgaliais.Length < 1 || Marsrutas.PradinesStotelesLaikai.Length < 1)
            {
                button1.IsEnabled = false;
                button1.IsVisible = false;
            }
            if (_ArDarboDiena)
            {
                button1.Text = "Savaitgalis";
            }
            else
            {
                button1.Text = "Darbo diena";
            }
            Color buttonColor = Color.Black;
            if (color == Color.Red)
            {
                buttonColor = Color.DarkRed;
            }
            if (color == Color.Green)
            {
                buttonColor = Color.DarkGreen;
            }
            if (color == Color.Goldenrod)
            {
                buttonColor = Color.DarkGoldenrod;
            }
            if (color == Color.Blue)
            {
                buttonColor = Color.DarkBlue;
            }
            button1.BackgroundColor = buttonColor;
        }
        private List<string> ParseString(string eilute)
        {
            char[] sep = { ';' };
            List<string> elementai = eilute.Split(sep).ToList();
            return elementai;
        }
        private List<Laikas> ParseStringLaikai(string eilute)
        {
            char[] sep = { ';' };
            List<string> elementaiS = eilute.Split(sep).ToList();
            List<Laikas> elementai = new List<Laikas>();
            foreach (var item in elementaiS)
            {
                elementai.Add(GetLaikas(item));
            }
            return elementai;
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
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var text = e.Item as Data;
            await Navigation.PushAsync(new Stoteles(_Marsrutas, _Color, true, text.Secondary));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        async void Button_Clicked(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Savaitgalis"))
            {
                button1.Text = "Darbo diena";
            }
            else
            {
                button1.Text = "Savaitgalis";
            }
            if (_Marsrutas.PradinesStotelesLaikaiSavaitgaliais.Length != 0)
            {
                await Navigation.PushAsync(new Laikai(_Stotele, _Color, _Marsrutas, !_ArDarboDiena));
                Navigation.RemovePage(this);
            }
        }
    }
}
