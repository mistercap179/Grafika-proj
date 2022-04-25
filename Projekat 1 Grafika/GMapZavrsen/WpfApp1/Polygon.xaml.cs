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
    /// Interaction logic for Polygon.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {
        List<System.Windows.Point> p = new List<Point>();
        public PolygonWindow(List<System.Windows.Point> points)
        {
            InitializeComponent();

            p = points;

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
                ThicknessList.Items.Add(i);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Polygon polygon = new Polygon();
            
            for (int i = 0; i < p.Count(); i++)
            {
                polygon.Points.Add(p[i]);
            }

            string thickness = ThicknessList.SelectedItem.ToString();

            polygon.StrokeThickness = Double.Parse(thickness);

            string color = ColorList.SelectedItem.ToString();

            var colorL = (Color)ColorConverter.ConvertFromString(color);

            polygon.Stroke = new SolidColorBrush(colorL);


            ((MainWindow)Application.Current.MainWindow).Mapa.Children.Add(polygon);

            if (TextCheck.IsChecked == true)
            {

                string color2 = ColorList2.SelectedItem.ToString();

                var colorLA = (Color)ColorConverter.ConvertFromString(color2);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = TextBox.Text;
                double _textSize = textBlock.FontSize;

                textBlock.Foreground = new SolidColorBrush(colorLA);
                int _avgX = (Int32)p.Sum(o => o.X) / p.Count;
                int _avgY = (Int32)p.Sum(o => o.Y) / p.Count;

                Canvas.SetLeft(textBlock, _avgX + (p[0].X / 2));
                Canvas.SetTop(textBlock, _avgY + (p[0].Y / 2));

                ((MainWindow)Application.Current.MainWindow).Mapa.Children.Add(textBlock);

            }

            ((MainWindow)Application.Current.MainWindow).points = new List<System.Windows.Point>();

            this.Close();
        }

        private void TextCheck_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox rb = sender as CheckBox;

            if (rb != null)
            {
                if (rb.IsChecked == true)
                {
                    TextBox.Visibility = System.Windows.Visibility.Visible;
                    ColorList2.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
    }
}
