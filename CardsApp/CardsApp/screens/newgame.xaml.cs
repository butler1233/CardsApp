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
	public partial class newgame : ContentPage
	{
        Dictionary<SwitchCell, Player> PlayerCells = new Dictionary<SwitchCell, Player>();

        public newgame ()
		{
            

			InitializeComponent ();
            var GameList = new Dictionary<string, int>();
            foreach (Game asd in Settings.Games)
            {
                GameList.Add(asd.Name, asd.GameId);
            }
            GamePicker.Items = GameList.Keys.ToList();

            foreach (Player pls in Settings.Players)
            {
                if (pls.PlayerId == 0)
                {
                    pls.Active = false;
                }
                else
                {
                    var sc = new SwitchCell()
                    {
                        BindingContext = pls,
                        On = pls.Active,
                        Text = pls.Name
                    };
                    PlayerCells.Add(sc, pls);
                    sc.OnChanged += PlayerToggled;
                    PlayerSection.Add(sc);
                }
                
            }
            
        }

        async void PlayerToggled(object sender, EventArgs e)
        {
            var sc = sender as SwitchCell;
            var player = PlayerCells[sc];
            player.Active = sc.On;

        }

        async void CreateButton_Clicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert("Create Game?", "Are you sure you want to create tjos game?", "Yes", "No"))
            {
                //GOGOOG
                var playingplayers = new List<int>();

                foreach (Player pls in Settings.Players)
                {
                    if (pls.Active) { playingplayers.Add(pls.PlayerId); };
                }
                var selectedGameId = Settings.Games.Single((game) => game.Name == GamePicker.SelectedItem).GameId;
                var entryCost = Convert.ToInt32(Price.Text);
                ReturnObject<GameInfo> response;
                try
                {
                    response = await BasicRequest<ReturnObject<GameInfo>>($"NewGame/Register?gameNameId={selectedGameId.ToString()}&entryCost={entryCost.ToString()}", "POST", $"[{String.Join(", ", playingplayers)}]");
                    if (response.Success)
                    {
                        await DisplayAlert("Game Registration", $"Game Registered - ID {response.ReturnData.GameUniqueId} - for £{response.ReturnData.Entry.ToString("F2")} entry with {response.ReturnData.Players.Count} players.", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Game Registration", $"Exception: {response.ExceptionMessage.ToString()}", "OK");
                    }                    
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Game Registration", $"Error: {ex.ToString()}", "OK");
                }
                

                

                

                //Done, go bak.
                Navigation.PopAsync();
            }
        }
    }
}