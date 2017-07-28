using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Content.Res;
using CardsApp.screens;
using Xamarin.Forms;
using static CardsApp.JSONInteracts;


namespace CardsApp.common
{
    internal static class Common
    {
        internal static Grid GetLoadingGridView(string LoadingText)
        {
            var LoaderGrid = new Grid();
            LoaderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
            LoaderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            LoaderGrid.Children.Add(new ActivityIndicator { IsRunning = true }, 0, 0);
            LoaderGrid.Children.Add(new Label { Text = LoadingText }, 1, 0);
            return LoaderGrid;
        }

        internal const string ApiUri = "http://cardsapi220170718100146.azurewebsites.net/";

        internal static void RipTitle(Page Context)
        {
            NavigationPage.SetHasNavigationBar(Context, false);
        }

        internal async static void LoadEverything()
        {
            try
            {
                Settings.Games = await BasicRequest<List<Classes.Game>>("Games/List");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }


        internal async static Task<int> RequestUserKeypad(Page context)
        {
            var Keypad = new Keypad();
            await context.Navigation.PushModalAsync(Keypad);
            await Task.Run(() =>
            {
                while (Keypad.StillOpen)
                {
                    Thread.Sleep(10);
                }
            });
            
            if (Keypad.Return)
            {
                return Keypad.Value;
            }
            else
            {
                throw new ArgumentNullException("No value was submitted.");
            }
            
        }
    }
}
