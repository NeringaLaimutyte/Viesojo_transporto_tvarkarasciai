using sql.Models;
using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace sql.Views
{
    public class Data
    {
        public int Id { get; set; }
        public string Primary { get; set; }
        public string Secondary { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Autobusai : ContentPage
    {
        private static string xmlFile = "DB.xml";
        List<Transportas> DbList = new List<Transportas>();
        public ObservableCollection<Data> Items { get; set; }
        private Color color;
        public Autobusai(char TransportoPriemone)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var TheHeader = "";
            string neigaliesiems = "\u267F";
            switch (TransportoPriemone)
            {
                case 'A':
                    DbList = App.GetAutobusai("A");
                    color = Color.Red;
                    TheHeader = "Autobusai";
                    break;
                case 'T':
                    DbList = App.GetAutobusai("T");
                    color = Color.Green;
                    TheHeader = "Troleibusai";
                    break;
                case 'M':
                    DbList = App.GetAutobusai("M");
                    color = Color.Goldenrod;
                    TheHeader = "Maršrutiniai taksi";
                    break;
                case 'R':
                    DbList = App.GetAutobusai("R");
                    color = Color.Blue;
                    TheHeader = "Tarpmiestiniai autobusai";
                    break;

            }
            Items = new ObservableCollection<Data>();
            foreach (var item in DbList)
            {
                if (item.PritaikytaNeigaliesiems)
                {
                    Items.Add(new Data() { Id = item.ID, Primary = item.Numeris + " | " + item.SavaitesDienos + " | "+neigaliesiems, Secondary = " " + item.PradineGalutineStotele });
                }
                else
                {
                    Items.Add(new Data() { Id = item.ID, Primary = item.Numeris + " | " + item.SavaitesDienos + " ", Secondary = " " + item.PradineGalutineStotele });
                }
            }
            MyListView.ItemsSource = Items;
            MyListView.BackgroundColor = color;
            BackgroundColor = color;
            MyListView.Header = TheHeader;
            MyListView.Footer = "";
            MyListView.SeparatorColor = color;
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            var text = e.Item as Data;

            foreach (var item in DbList)
            {
                if (item.ID.Equals(text.Id))
                {
                    await Navigation.PushAsync(new Stoteles(item, color, false, ""));
                }
            }
            //await Navigation.PushAsync(new Stoteles(DbList[text.Id], color, false, ""));
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
