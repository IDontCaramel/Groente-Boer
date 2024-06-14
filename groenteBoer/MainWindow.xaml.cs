using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace groenteBoer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            generateGroente();
        }

        private void generateGroente()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            List<Product> products = dbHelper.GetProducts();

            foreach (Product product in products)
            {
                Button btn = new Button
                {
                    Tag = product.ID,
                    Margin = new Thickness(4),
                    Height = 250,
                    Width = 250,

                };
                if (product.plaatje != null)
                {
                    Uri uri = new Uri("pack://application:,,,/fruits/" + product.plaatje);

                    Grid grid = new Grid();

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = $"{product.groente}\n{product.prijs.ToString()} {product.prijsPer}";
                    textBlock.Foreground = Brushes.Black;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.FontSize = 30;
                    textBlock.FontFamily = new FontFamily("Century Gothic");



                    try
                    {
                        Image imageControl = new Image();
                        BitmapImage bitmapImage = new BitmapImage(uri);
                        imageControl.Source = bitmapImage;
                        imageControl.VerticalAlignment = VerticalAlignment.Center;
                        imageControl.HorizontalAlignment = HorizontalAlignment.Center;

                        textBlock.VerticalAlignment = VerticalAlignment.Bottom;
                        
                        grid.Children.Add(imageControl);
                    }
                    catch
                    {
                    }

                    grid.Children.Add(textBlock);

                    btn.Content = grid;
                }


                else
                {
                    StackPanel stackPanel = new StackPanel();

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = product.prijs.ToString(); // Assuming product.prijs is a numeric value
                    textBlock.Foreground = Brushes.Black; // Set text color
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center; // Center text horizontally
                    textBlock.VerticalAlignment = VerticalAlignment.Center; // Center text vertically

                    stackPanel.Children.Add(textBlock);
                    btn.Content = stackPanel;
                }

                // Subscribe to the Click event of the button
                btn.Click += (sender, args) =>
                {
                    Button clickedButton = sender as Button;
                    if (clickedButton != null)
                    {
                        MessageBox.Show("Button clicked! Tag: " + clickedButton.Tag.ToString());
                    }
                };

                // Add the button to your layout container
                wp.Children.Add(btn);
            };
        }
    }
}
