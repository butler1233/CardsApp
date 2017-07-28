using CardsApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardsApp.screens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameInfoScreen : ContentPage
	{

        GameInfo gamedata;
        /// <summary>
        /// Paramless no use pls
        /// </summary>
        public GameInfoScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Use me isntead
        /// </summary>
        /// <param name="data"></param>
        internal GameInfoScreen(GameInfo data)
        {
            gamedata = data;
            InitializeComponent();

            //Gogogo
            GameSection.Title = data.GameType.Name;
            this.Title = data.GameType.Name + $" ({data.GameUniqueId.ToString()})";
            GameIdCell.Detail = data.GameUniqueId.ToString();
            EntryCell.Detail = "£" + data.Entry.ToString("F2");
            PotCell.Detail = "£" + (data.Entry * data.Players.Count).ToString("F2");
            Timestamp.Detail = data.GameCompleted.ToString();

            //Finished Status
            if (data.Finished)
            {
                CompletedCell.Detail = "Yes";
                FinishButton.IsVisible = false;
            }
            else {
                CompletedCell.Detail = "No (press below to finish)";
                FinishButton.IsVisible = true;
            }

            //Players
            foreach(Player player in data.Players)
            {
                var newcell = new TextCell() { Text = player.Name, Detail="Currently " + player.CurrentMargin.ToString()};
                PlayerList.Add(newcell);
            }
        }

        private async void FinishButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameFinishScreen(gamedata));
        }
    }
}