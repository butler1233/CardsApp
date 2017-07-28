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
	public partial class RecentGames : ContentPage
	{
		public RecentGames ()
		{
			InitializeComponent ();

            Task.Run(async () =>
            {
                games = await BasicRequest<List<GameInfo>>("GamesPlayed/Last/30", "POST");
                games.Sort((x, y) => y.GameUniqueId.CompareTo(x.GameUniqueId));
                Device.BeginInvokeOnMainThread(() =>
                {
                    foreach (GameInfo game in games)
                    {
                        var newcell = new TextCell() { Text = game.GameType.Name + " for £" + game.Entry.ToString("F2") , Detail = game.Players.Count.ToString() + " Players on " + game.GameCompleted.ToString() };
                        gamesdict.Add(newcell, game);
                        newcell.Tapped += gamepressed;
                        gamebuttons.Add(newcell);
                    }
                });
            });
            
		}
        List<GameInfo> games;
        Dictionary<TextCell, GameInfo> gamesdict = new Dictionary<TextCell, GameInfo>();

        public void gamepressed(object sender, EventArgs e)
        {
            var player = gamesdict[sender as TextCell];
            Navigation.PushAsync(new GameInfoScreen(player));
        }
	}
}