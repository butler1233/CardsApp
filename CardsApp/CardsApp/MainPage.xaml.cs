using CardsApp.screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CardsApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Main_Menu());
        }
    }
}
