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
	public partial class GameFinishScreen : ContentPage
	{
        GameInfo game;
        Dictionary<TextCell, Player> players = new Dictionary<TextCell, Player>();

		public GameFinishScreen ()
		{
			InitializeComponent ();
		}
        internal GameFinishScreen(GameInfo data)
        {
            InitializeComponent();
            game = data;
            try
            {
                // fill the players
                //Players
                foreach (Player player in data.Players)
                {
                    var newcell = new TextCell() { Text = player.Name, Detail = "Currently " + player.CurrentMargin.ToString() };
                    newcell.Height = 50;
                    players.Add(newcell, player);
                    newcell.Tapped += FinishPls;
                    playerbuttons.Add(newcell);
                }
            }
            catch (Exception exce)
            {

            }
            
        }

        private async void FinishPls(Object Sender, EventArgs ea)
        {

            var player = players[Sender as TextCell];
            try
            {
                var response = await BasicRequest<int>($"Games/Finish?gamePlayedId={game.GameUniqueId}&gameWinner={player.PlayerId}", "POST");
                await Navigation.PopToRootAsync();
            }
            catch (Exception exception)
            {
                await DisplayAlert("Couldn't finish", exception.Message, "OK");
            }

            

            
        }

        private async void DrawButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var response = await BasicRequest<int>($"Games/Finish?gamePlayedId={game.GameUniqueId}&gameWinner=0&isDraw=True", "POST");
                await Navigation.PopToRootAsync();
            }
            catch (Exception exception)
            {
                await DisplayAlert("Couldn't finish", exception.Message, "OK");
            }
        }
    }
}