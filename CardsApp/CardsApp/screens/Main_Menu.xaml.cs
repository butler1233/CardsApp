using CardsApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CardsApp.JSONInteracts;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardsApp.screens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Main_Menu : ContentPage
	{
		public Main_Menu ()
		{
			InitializeComponent ();
		}

        private void NewGameButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new newgame());
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        

        private void RefreshBoardButton_Clicked(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        private async void RefreshBoard()
        {
            BoardSection.Clear();
            BoardSection.Add(new ViewCell() { View = common.Common.GetLoadingGridView("Loading The Board™") });
            Settings.Players = await BasicRequest<List<Player>>("Players/List", "POST");
            Settings.Players.Sort((x, y) => y.CurrentMargin.CompareTo(x.CurrentMargin));
            BoardSection.Clear();
            foreach (Player Play in Settings.Players)
            {
                var LabelColor = Color.Red;
                if (Play.Active)
                {
                    LabelColor = Color.White;
                }
                var Cell = new EntryCell() { IsEnabled = false, Text = Play.CurrentMargin.ToString(), Label = Play.Name, LabelColor = LabelColor };
                BoardSection.Add(Cell);
            }
        }


    }
}