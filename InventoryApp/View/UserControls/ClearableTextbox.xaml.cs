using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InventoryApp.View.UserControls
{
	public partial class ClearableTextbox : UserControl, INotifyPropertyChanged
	{
		public ClearableTextbox()
		{
			InitializeComponent();
		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			txtInput.Clear();
			txtInput.Focus();
		}
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
		{
			Text = txtInput.Text;
			UpdatePlaceholderVisibility();
		}

		public static readonly DependencyProperty TextProperty =
						DependencyProperty.Register(
								nameof(Text),
								typeof(string),
								typeof(ClearableTextbox),
								new FrameworkPropertyMetadata(
										string.Empty,
										FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
										OnTextChanged));
		private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var control = (ClearableTextbox)d;
			control.txtInput.Text = (string)e.NewValue;
			control.UpdatePlaceholderVisibility();
		}

		private void UpdatePlaceholderVisibility()
		{
			txtPlaceholder.Visibility = string.IsNullOrEmpty(Text) ? Visibility.Visible : Visibility.Hidden;
		}

		private string placeholder;
		public string Placeholder
		{
			get { return placeholder; }
			set
			{
				placeholder = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			txtPlaceholder.Text = Placeholder;
		}
	}
}
