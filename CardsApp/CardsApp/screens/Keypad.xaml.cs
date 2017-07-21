using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardsApp.screens
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Keypad : ContentPage
	{
		public Keypad ()
		{
			InitializeComponent ();
            
		}

	    internal bool Return = false;
	    internal int Value = 0;
	    internal bool StillOpen = true;

	    private void Key_OnClicked(object sender, EventArgs e)
	    {
	        if (KeypadValue.Text == "0" || KeypadValue.Text == "")
	        {
	            KeypadValue.Text = (sender as Button).Text;

	        }
	        else
	        {
	            KeypadValue.Text += (sender as Button).Text;
            }
	        
	    }

	    private void KeyCancel_OnClicked(object sender, EventArgs e)
	    {
	        Return = false;
	        Value = 0;
	        Navigation.PopModalAsync();
	        StillOpen = false;
	    }

	    private void KeyEnter_OnClicked(object sender, EventArgs e)
	    {
	        if (KeypadValue.Text == "")
	        {
	            Value = 0;

	        }
	        else
	        {
	            Value = Convert.ToInt32(KeypadValue.Text);
	        }
	        Return = true;
	        Navigation.PopModalAsync();
            StillOpen = false;
        }
	}
}