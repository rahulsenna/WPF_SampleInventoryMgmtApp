using System.Windows;

namespace InventoryApp.View
{
	public partial class MainWindow : Window
	{
		void SetDarkMode(bool enabled)
		{
			var dictionaries = Application.Current.Resources.MergedDictionaries;
			dictionaries.Clear();

			if (enabled)
			{
				dictionaries.Add(new ResourceDictionary
				{
					Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative)
				});
			}
			else
			{
				dictionaries.Add(new ResourceDictionary
				{
					Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative)
				});
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			SetDarkMode(false);
		}
	}
}