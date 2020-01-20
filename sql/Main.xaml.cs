using sql.Models;
using sql.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace sql
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main : ContentPage
	{
		public Main ()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Color.Transparent;
            BackgroundImage = "Backgound.jpg";

            InitializeComponent ();
		}

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Autobusai('A'));
            //await Navigation.PushAsync(new Page1('A'));
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Autobusai('T'));
            //await Navigation.PushAsync(new Page1('T'));
        }

        async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Autobusai('M'));
            //await Navigation.PushAsync(new Page1('M'));
        }

        async void Button_Clicked_3(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Autobusai('R'));
            //await Navigation.PushAsync(new Page1('R'));
        }
    }
}