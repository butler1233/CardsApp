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
            try
            {
                RefreshBoard();
            }
            catch (Exception ex)
            {
                //uhj
                Console.WriteLine(ex.ToString());
            }
            
        }

        

        private void RefreshBoardButton_Clicked(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        private async void RefreshBoard()
        {

            //Refresh the board
            BoardSection.Clear();
            BoardSection.Add(new ViewCell() { View = common.Common.GetLoadingGridView("Loading The Board™") });
            Settings.Players = await BasicRequest<List<Player>>("Players/List", "POST");

            var boardStatus = await BasicRequest<Dictionary<string, decimal>>("Status/All", "POST");
            Settings.Players.Sort((x, y) => y.CurrentMargin.CompareTo(x.CurrentMargin));
            BoardSection.Clear();
            foreach (Player Play in Settings.Players)
            {
                var LabelColor = Color.Red;
                if (Play.Active)
                {
                    LabelColor = Color.White;
                }
                var Cell = new EntryCell() { IsEnabled = false, Text = boardStatus[Play.Name].ToString("F2"), Label = Play.Name, LabelColor = LabelColor };
                BoardSection.Add(Cell);
            }

            //refresh the games
            CurrentGames.Clear();
            CurrentGames.Add(new ViewCell() { View = common.Common.GetLoadingGridView("Loading Current Games") });
            var Games = await BasicRequest<List<GameInfo>>("GamesPlayed/Unfinished", "POST");
            CurrentGameEntries = new Dictionary<TextCell, GameInfo>();
            Games.Sort((x, y) => y.GameUniqueId.CompareTo(x.GameUniqueId));
            CurrentGames.Clear();
            foreach (GameInfo game in Games)
            {
                var playernames = new List<string>();
                foreach (Player player in game.Players)
                {
                    playernames.Add(player.Name);
                }
                var Cell = new TextCell(){ Text = game.GameType.Name + " - £" + game.Entry.ToString("F2"), Detail = $"ID: {game.GameUniqueId} Players: {string.Join(", ",playernames)}" };
                Cell.Tapped += CurrentGamePress;
                CurrentGameEntries.Add(Cell, game);
                CurrentGames.Add(Cell);
            }
        }

        private async void CurrentGamePress(object Sender, EventArgs e)
        {
            try
            {
                var GameInf = CurrentGameEntries[Sender as TextCell];
                await Navigation.PushAsync(new GameInfoScreen(GameInf));
            }
            catch(Exception ex)
            {

            }
            
        }


        Dictionary<TextCell, GameInfo> CurrentGameEntries;

        private void TextCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RecentGames());
        }
    }
}