using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardsApp.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PickerCell : ViewCell
	{
		public PickerCell ()
		{
			InitializeComponent ();
		}

        public string Text
        {
            get { return TitleLabel.Text; }
            set { TitleLabel.Text = value; }
        }

        public IList<string> Items
        {
            get { return PackPicker.Items; }
            set { PackPicker.ItemsSource = (System.Collections.IList) value; }
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            PackPicker.Focus();
        }

        public string SelectedItem
        {
            get { return PackPicker.SelectedItem as string; }
            set { PackPicker.SelectedItem = value as object; }
        }
    }

}