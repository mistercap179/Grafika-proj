using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
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
using System.Xml;
using WpfApp1.Model;
using Brushes = System.Drawing.Brushes;
using Pen = System.Drawing.Pen;
using Point = WpfApp1.Model.Point;
using Size = System.Drawing.Size;
using WpfApp1.Logic;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public double noviX, noviY;
        public int minMatricaX, minMatricaY, maxMatricaX, maxMatricaY;
        public int N = 250;
        public MjestoMatrica[,] matrica = new MjestoMatrica[250, 250];
        public string radioButton;
        public UIElement poslednji;
        public List<UIElement> svi = new List<UIElement>();
        public List<System.Windows.Point> points = new List<System.Windows.Point>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Mapa_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;


            if (radioButton == "Ellipse")
            {
                WpfApp1.EllipseWindow ellipseWindow = new EllipseWindow(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
                ellipseWindow.Show();
                UndoButton.Visibility = Visibility.Visible;
                ClearButton.Visibility = Visibility.Visible;
            }
            else if (radioButton == "Polygon")
            {
                WpfApp1.PolygonWindow polygonWindow = new PolygonWindow(points);
                polygonWindow.Show();
                UndoButton.Visibility = Visibility.Visible;
                ClearButton.Visibility = Visibility.Visible;
            }
            else
            {
                WpfApp1.TextWindow textWindow = new TextWindow(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
                textWindow.Show();
                UndoButton.Visibility = Visibility.Visible;
                ClearButton.Visibility = Visibility.Visible;
            }
        }

        private void Mapa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            if (radioButton == "Polygon")
            {
                points.Add(e.GetPosition(canvas));
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(svi.Count);
            if(ClearButton.Visibility == Visibility.Hidden)
            {
                for (int i = 0; i < svi.Count; i++)
                {
                    Mapa.Children.Add(svi[i]);
                }
            }
            else
            {
                poslednji = Mapa.Children[Mapa.Children.Count - 1];

                Mapa.Children.RemoveAt(Mapa.Children.Count - 1);

                UndoButton.Visibility = Visibility.Hidden;
                RedoButton.Visibility = Visibility.Visible;
            }
            

        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            Mapa.Children.Add(poslednji);

            RedoButton.Visibility = Visibility.Hidden;
            UndoButton.Visibility = Visibility.Visible;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < Mapa.Children.Count; i++)
            {
                svi.Add(Mapa.Children[i]);
            }

            Mapa.Children.Clear();

            if(Mapa.Children.Count != 0)
            {
                ClearButton.Visibility = Visibility.Visible;
            }
            else
            {
                ClearButton.Visibility = Visibility.Hidden;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                if (rb.IsChecked == true)
                {
                    radioButton = rb.Name;
                }

            }
        }

        //public Dictionary<int,>
        public Dictionary<long, ObjekatEES> objekti = new Dictionary<long,ObjekatEES>();

        public void line_MouseDown(object sender, MouseButtonEventArgs e,long firstEnd,long secondEnd)
        {
            for(int i = 0; i< Mapa.Children.Count; i++)
            {
                if (Mapa.Children[i].Uid == firstEnd.ToString())
                {
                    if (Mapa.Children[i] is System.Windows.Shapes.Ellipse)
                    {
                        (Mapa.Children[i] as System.Windows.Shapes.Ellipse).Fill = new SolidColorBrush(Colors.BlueViolet);
                    }
                }
                else if (Mapa.Children[i].Uid == secondEnd.ToString())
                {
                    if (Mapa.Children[i] is System.Windows.Shapes.Ellipse)
                    {
                        (Mapa.Children[i] as System.Windows.Shapes.Ellipse).Fill = new SolidColorBrush(Colors.BlueViolet);
                    }
                }
            }

            Mapa.UpdateLayout();
        }


        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    matrica[i, j] = new MjestoMatrica()
                    {
                        Polje = Mjesto.Slobodno,
                        PathId = -1
                    };
                }
            }

            ////////////////////////////////////
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("Geographic.xml");
            XmlNodeList nodeList;
            double newX, newY;

//////////////////////////////////////////........ substations..........//////////////////////////////////////////////////////

            SubstationEntity sub = new SubstationEntity();
            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Substations/SubstationEntity");

            List<double> NiznoviX = new List<double>();
            List<double> NiznoviY = new List<double>();
            double minX,minY, maxX, maxY;   

            foreach(XmlNode novi in nodeList)
            {
                double konvertovaniX  = double.Parse(novi.SelectSingleNode("X").InnerText);
                double konvertovaniY = double.Parse(novi.SelectSingleNode("Y").InnerText);

                Conversions.ToLatLon(konvertovaniX, konvertovaniY, 34, out newY, out newX);

                NiznoviX.Add(newX);
                NiznoviY.Add(newY);
            }

            minX = NiznoviX.Min();
            minY = NiznoviY.Min();
            maxX = NiznoviX.Max();
            maxY = NiznoviY.Max();

            foreach (XmlNode node in nodeList)
            {
                sub.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                sub.Name = node.SelectSingleNode("Name").InnerText;
                sub.X = double.Parse(node.SelectSingleNode("X").InnerText);
                sub.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                Conversions.ToLatLon(sub.X, sub.Y, 34, out noviX, out noviY);

                double proportionX = Conversions.ProportionX(minX, maxX, noviY, Mapa.Width);
                double proportionY = Conversions.ProportionY(minY, maxY, noviX, Mapa.Height);

                System.Windows.Shapes.Ellipse marker = new System.Windows.Shapes.Ellipse();
                ToolTip toolTip = new ToolTip();

                marker.Width = 2;
                marker.Height = 2;
                marker.Fill = System.Windows.Media.Brushes.White;

                toolTip.Content = "Substation\nID: " + sub.Id + "  Name: " + sub.Name;
                toolTip.Foreground = System.Windows.Media.Brushes.Black;
                toolTip.Background = System.Windows.Media.Brushes.White;


                List<int> koordinate = MatrixPlacement.pronadjiMjesto(proportionX / 2, proportionY / 2, matrica);
                
                if (koordinate.Count != 0)
                {
                    ObjekatEES objekat = new ObjekatEES();

                    objekat.id = sub.Id;
                    objekat.x = koordinate[0] ;
                    objekat.y = koordinate[1] ;

                    objekti.Add(objekat.id, objekat);

                    marker.Margin = new Thickness(koordinate[0] * 2, koordinate[1] * 2, 0, 0);
                    marker.ToolTip = toolTip;
                    marker.Uid = (sub.Id).ToString();
                    Mapa.Children.Add(marker);      // mapa -> canvas
                    
                }
            }



//////////////////////////////////////////........ nodes..........//////////////////////////////////////////////////////////
            
            
            
            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Nodes/NodeEntity");

            List<double> NodoviX = new List<double>();
            List<double> NodoviY = new List<double>();
            double minNodeX, minNodeY, maxNodeX, maxNodeY;

            foreach (XmlNode novi in nodeList)
            {
                double konvertovaniX = double.Parse(novi.SelectSingleNode("X").InnerText);
                double konvertovaniY = double.Parse(novi.SelectSingleNode("Y").InnerText);

                Conversions.ToLatLon(konvertovaniX, konvertovaniY, 34, out newY, out newX);

                NodoviX.Add(newX);
                NodoviY.Add(newY);
            }

            minNodeX = NodoviX.Min();
            minNodeY = NodoviY.Min();
            maxNodeX = NodoviX.Max();
            maxNodeY = NodoviY.Max();


            NodeEntity nodeobj = new NodeEntity();

            foreach (XmlNode node in nodeList)
            {
                nodeobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                nodeobj.Name = node.SelectSingleNode("Name").InnerText;
                nodeobj.X = double.Parse(node.SelectSingleNode("X").InnerText);
                nodeobj.Y = double.Parse(node.SelectSingleNode("Y").InnerText);

                Conversions.ToLatLon(nodeobj.X, nodeobj.Y, 34, out noviX, out noviY);

                double proportionX = Conversions.ProportionX(minNodeX, maxNodeX, noviY, Mapa.Width);
                double proportionY = Conversions.ProportionY(minNodeY, maxNodeY, noviX, Mapa.Height);

                System.Windows.Shapes.Ellipse marker = new System.Windows.Shapes.Ellipse();
                ToolTip toolTip = new ToolTip();

                marker.Width = 2;
                marker.Height = 2;
                marker.Fill = System.Windows.Media.Brushes.Green;

                toolTip.Content = "Node\nID: " + nodeobj.Id + "  Name: " + nodeobj.Name;
                toolTip.Foreground = System.Windows.Media.Brushes.Green;
                toolTip.Background = System.Windows.Media.Brushes.White;

                List<int> koordinate = new List<int>();
                koordinate = MatrixPlacement.pronadjiMjesto(proportionX/2, proportionY/2, matrica);
                if (koordinate.Count != 0)
                {
                    ObjekatEES objekat = new ObjekatEES();

                    objekat.id = nodeobj.Id;
                    objekat.x = koordinate[0] ;
                    objekat.y = koordinate[1] ;

                    objekti.Add(objekat.id, objekat);

                    marker.Margin = new Thickness(koordinate[0] * 2, koordinate[1] * 2, 0, 0);
                    marker.ToolTip = toolTip;
                    //marker.Name = nodeobj.Name; 
                    marker.Uid = (nodeobj.Id).ToString();
                    Mapa.Children.Add(marker);
                }// mapa -> canvas
            }


//////////////////////////////////////////........ switchers..........//////////////////////////////////////////////////////            


            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Switches/SwitchEntity");

            List<double> SwitcheviX = new List<double>();
            List<double> SwitcheviY = new List<double>();
            double minSwitchX, minSwitchY, maxSwitchX, maxSwitchY;

            foreach (XmlNode novi in nodeList)
            {
                double konvertovaniX = double.Parse(novi.SelectSingleNode("X").InnerText);
                double konvertovaniY = double.Parse(novi.SelectSingleNode("Y").InnerText);

                Conversions.ToLatLon(konvertovaniX, konvertovaniY, 34, out newY, out newX);

                SwitcheviX.Add(newX);
                SwitcheviY.Add(newY);
            }

            minSwitchX = SwitcheviX.Min();
            minSwitchY = SwitcheviY.Min();
            maxSwitchX = SwitcheviX.Max();
            maxSwitchY = SwitcheviY.Max();

            SwitchEntity switchobj = new SwitchEntity();

            foreach (XmlNode node in nodeList)
            {
                switchobj.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                switchobj.Name = node.SelectSingleNode("Name").InnerText;
                switchobj.X = double.Parse(node.SelectSingleNode("X").InnerText);
                switchobj.Y = double.Parse(node.SelectSingleNode("Y").InnerText);
                switchobj.Status = node.SelectSingleNode("Status").InnerText;

                Conversions.ToLatLon(switchobj.X, switchobj.Y, 34, out noviX, out noviY);

                double proportionX = Conversions.ProportionX(minSwitchX, maxSwitchX, noviY, Mapa.Width);
                double proportionY = Conversions.ProportionY(minSwitchY, maxSwitchY, noviX, Mapa.Height);

                System.Windows.Shapes.Ellipse marker = new System.Windows.Shapes.Ellipse();
                ToolTip toolTip = new ToolTip();

                marker.Width = 2;
                marker.Height = 2;
                marker.Fill = System.Windows.Media.Brushes.DarkRed;

                toolTip.Content = "Swtich\nID: " + switchobj.Id + "  Name: " + switchobj.Name;
                toolTip.Foreground = System.Windows.Media.Brushes.IndianRed;
                toolTip.Background = System.Windows.Media.Brushes.White;

                List<int> koordinate = new List<int>();
                koordinate = MatrixPlacement.pronadjiMjesto(proportionX/2, proportionY/2, matrica);
                if (koordinate.Count != 0)
                {
                    ObjekatEES objekat = new ObjekatEES();

                    objekat.id = switchobj.Id;
                    objekat.x = koordinate[0] ;
                    objekat.y = koordinate[1] ;

                    objekti.Add(objekat.id, objekat);
                    
                    marker.Margin = new Thickness(koordinate[0] * 2, koordinate[1] * 2, 0, 0);
                    marker.ToolTip = toolTip;
                    marker.Uid = (switchobj.Id).ToString();
                    //marker.Name = switchobj.Name;
                    Mapa.Children.Add(marker);
                }// mapa -> canvas
            }


            ///////////////////////////////////lines/////////////////////////////////////////////////
            LineEntity l = new LineEntity();
            List<Polyline> linijeee = new List<Polyline>();
            List<Linija> lines = new List<Linija>();
            nodeList = xmlDoc.DocumentElement.SelectNodes("/NetworkModel/Lines/LineEntity");

            foreach (XmlNode node in nodeList)
            {
                l.Id = long.Parse(node.SelectSingleNode("Id").InnerText);
                l.Name = node.SelectSingleNode("Name").InnerText;
                if (node.SelectSingleNode("IsUnderground").InnerText.Equals("true"))
                {
                    l.IsUnderground = true;
                }
                else
                {
                    l.IsUnderground = false;
                }
                l.R = float.Parse(node.SelectSingleNode("R").InnerText);
                l.ConductorMaterial = node.SelectSingleNode("ConductorMaterial").InnerText;
                l.LineType = node.SelectSingleNode("LineType").InnerText;
                l.ThermalConstantHeat = long.Parse(node.SelectSingleNode("ThermalConstantHeat").InnerText);
                l.FirstEnd = long.Parse(node.SelectSingleNode("FirstEnd").InnerText);
                l.SecondEnd = long.Parse(node.SelectSingleNode("SecondEnd").InnerText);


                Linija jedna = new Linija()
                {
                    firstEnd = l.FirstEnd,
                    secondEnd = l.SecondEnd,
                    name = l.Name
                };

                lines.Add(jedna);

                System.Windows.Shapes.Polyline line = new System.Windows.Shapes.Polyline();

                line.Stroke = System.Windows.Media.Brushes.White;
                line.StrokeThickness = 0.5;
                line.Name = l.Name;

                ToolTip toolTip = new ToolTip();

                toolTip.Content = "Line\nID: " + l.Id + "  Name: " + l.Name;
                toolTip.Foreground = System.Windows.Media.Brushes.IndianRed;
                toolTip.Background = System.Windows.Media.Brushes.White;

                if (objekti.ContainsKey(l.FirstEnd) && objekti.ContainsKey(l.SecondEnd))
                {
                    List<QItem> q =
                        WpfApp1.Logic.BFS.minDistance(
                                ref matrica, N, objekti[l.FirstEnd].x, objekti[l.FirstEnd].y,
                                objekti[l.SecondEnd].x, objekti[l.SecondEnd].y
                            );

                    if (q.Count > 0)
                    {

                        for (int i = 0; i < q.Count; i++)
                        {
                            System.Windows.Point point = new System.Windows.Point();
                            QItem qItem = q[i];
                            point.X = qItem.x * 2 + 1;
                            point.Y = qItem.y * 2 + 1;
                            line.ToolTip = toolTip;
                            line.Points.Add(point);
                        }

                        Mapa.Children.Add(line);

                    }
                }

                line.MouseDown += delegate (object c, MouseButtonEventArgs a)
                {
                    //var linijes = Mapa.Children.OfType<Polyline>().ToList();
                    Linija lin = lines.Where(x => x.name == line.Name).FirstOrDefault();

                    line_MouseDown(c, a, lin.firstEnd, lin.secondEnd);
                    
                };

            }

            List <System.Windows.Shapes.Ellipse> preseci = new List<System.Windows.Shapes.Ellipse>();

            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    if (
                        i > 1 && i < matrica.GetLength(0) - 1 &&
                        j > 1 && j < matrica.GetLength(1) - 1
                        )
                    {
                        HashSet<int> pathIds = new HashSet<int>();
                        pathIds.Add(matrica[i, j].PathId);
                        pathIds.Add(matrica[i+1, j].PathId);
                        pathIds.Add(matrica[i-1, j].PathId);
                        pathIds.Add(matrica[i, j+1].PathId);
                        pathIds.Add(matrica[i, j-1].PathId);

                        if (
                            matrica[i, j].Polje == Mjesto.Vod &&
                            matrica[i+1,j].Polje == Mjesto.Vod &&
                            matrica[i-1,j].Polje == Mjesto.Vod &&
                            matrica[i,j+1].Polje == Mjesto.Vod &&
                            matrica[i,j-1].Polje == Mjesto.Vod &&
                            pathIds.Count == 2
                            )
                        {
                            System.Windows.Shapes.Ellipse ellipse = new System.Windows.Shapes.Ellipse();
                            ellipse.Stroke = System.Windows.Media.Brushes.Orange;
                            ellipse.Margin = new Thickness(i * 2, j * 2, 0, 0);
                            ellipse.Width = 2;
                            ellipse.Height = 2;
                            preseci.Add(ellipse);
                        }
                    }
                }
            }

            preseci.ForEach(x => Mapa.Children.Add(x));


        }



    }
}
