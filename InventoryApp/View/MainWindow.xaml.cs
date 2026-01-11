using System.Windows;
using Dark.Net;

namespace InventoryApp.View
{
	public partial class MainWindow : Window
	{
		void SetDarkMode(bool enabled)
		{
			var dictionaries = Application.Current.Resources.MergedDictionaries;
			dictionaries.Clear();

			DarkNet.Instance.SetWindowThemeWpf(this, enabled ? Theme.Dark : Theme.Light);

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
		bool isDarkMode;

		public MainWindow()
		{
			DarkNet.Instance.SetWindowThemeWpf(this, Theme.Dark);
			InitializeComponent();
			SetDarkMode(false);
		}

		private void ToggleDarkMode_Click(object sender, RoutedEventArgs e)
		{
			isDarkMode = !isDarkMode;
			SetDarkMode(isDarkMode);
		}
	}
}