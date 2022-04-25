using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Ellipse.xaml
    /// </summary>
    public partial class EllipseWindow : Window
    {
        double xEll, yEll;
        public EllipseWindow(double x,double y)
        {
            xEll = x;
            yEll = y;


            InitializeComponent();


            List<string> boje = new List<string>()
            {
                "#000000",
                "#0000FF",
                "#808080",
                "#008000",
                "#800080",
                "#FF0000",
                "#FFFFFF"
            };

            for (int i = 0; i < boje.Count(); i++)
            {
                ColorList.Items.Add(boje[i]);
                ColorList2.Items.Add(boje[i]);
            }


            for (int i = 0; i < 100; i++)
            {
                RadiusList.Items.Add(i);
            }


            for (int i = 0; i < 100; i++)
            {
                ThicknessList.Items.Add(i);
            }
        }

        private void AddText_Checked(object sender, RoutedEventArgs e)
        {

            CheckBox rb = sender as CheckBox;

            if (rb != null)
            {
                if (rb.IsChecked == true)
                {
                    Text.Visibility = System.Windows.Visibility.Visible;
                    ColorList2.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ellipse ellipse = new Ellipse();

            string text = RadiusList.SelectedItem.ToString();
            ellipse.Width = Double.Parse(text);
            
            ellipse.Height = Double.Parse(text);

            string thickness = ThicknessList.SelectedItem.ToString();

            ellipse.StrokeThickness = Double.Parse(thickness);

            string color = ColorList.SelectedItem.ToString();

            var colorL = (Color)ColorConverter.ConvertFromString(color);

            ellipse.Stroke = new SolidColorBrush(colorL);

            Canvas.SetLeft(ellipse, xEll);
            Canvas.SetTop(ellipse, yEll);


            ((MainWindow)Application.Current.MainWindow).Mapa.Children.Add(ellipse);

            if (AddText.IsChecked == true)
            {

                string color2 = ColorList2.SelectedItem.ToString();

                var colorLA = (Color)ColorConverter.ConvertFromString(color2);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = Text.Text;

                textBlock.Foreground = new SolidColorBrush(colorLA);

                Canvas.SetLeft(textBlock, xEll);
                Canvas.SetTop(textBlock, yEll + ellipse.Height / 2);

                ((MainWindow)Application.Current.MainWindow).Mapa.Children.Add(textBlock);

            }

            this.Close();
        }
    }
}
