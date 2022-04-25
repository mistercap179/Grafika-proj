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
    /// Interaction logic for Text.xaml
    /// </summary>
    public partial class TextWindow : Window
    {
        public double xText,yText;

        public List<TextBlock> textovi = new List<TextBlock>();

        public TextWindow( double x,double y)
        {
            InitializeComponent();

            xText = x;
            yText = y;

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
            }


            for (int i = 0; i < 100; i++)
            {
                SizeList.Items.Add(i);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string size = SizeList.SelectedItem.ToString();

            TextBox.FontSize = Double.Parse(size);

            string color = ColorList.SelectedItem.ToString();

            var colorL = (Color)ColorConverter.ConvertFromString(color);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = TextBox.Text;
            textBlock.FontSize = TextBox.FontSize;
            textBlock.Foreground = new SolidColorBrush(colorL);


            Canvas.SetLeft(textBlock, xText);
            Canvas.SetTop(textBlock, yText);

            ((MainWindow)Application.Current.MainWindow).Mapa.Children.Add(textBlock);

            textovi.Add(textBlock);         

            this.Close();
        }
    }
}
