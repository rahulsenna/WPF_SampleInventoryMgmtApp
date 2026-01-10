using System.Windows;

namespace InventoryApp.View
{
	public partial class ProductModal : Window
	{
		public ProductModal()
		{
			InitializeComponent();
		}


		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}
	}
}
