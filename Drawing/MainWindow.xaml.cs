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

namespace Drawing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDrawing;
        private List<Line> lines = new List<Line>();
        private Point startPoint;


        public MainWindow()
        {
            InitializeComponent();
            canvas.MouseLeave += Сanvas_MouseLeave;
        }

        private Polyline currentPolyline;

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Point currentPoint = e.GetPosition(canvas);
                currentPolyline.Points.Add(currentPoint);
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            Point startPoint = e.GetPosition(canvas);
            currentPolyline = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Points = new PointCollection { startPoint }
            };
            canvas.Children.Add(currentPolyline);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
        }




        private void Сanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            // Iterate over all the Polyline segments
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Polyline polyline)
                {
                    // Check if the Polyline forms an inverted eight
                    if (polyline.Points.Count == 3 &&
                        polyline.Points[0].Y < polyline.Points[1].Y &&
                        polyline.Points[1].X < polyline.Points[2].X &&
                        polyline.Points[0].X - polyline.Points[1].X < 30 &&  
                        polyline.Points[1].Y - polyline.Points[0].Y > 100 && 
                        polyline.Points[2].Y - polyline.Points[1].Y < 30 && 
                        polyline.Points[2].X - polyline.Points[0].X < 30 && 
                        Math.Abs(polyline.Points[1].X - polyline.Points[0].X - polyline.Points[2].X + polyline.Points[1].X) < 30) 
                    {
                        MessageBox.Show("Inverted eight detected!");
                        return;
                    }



                }
            }

            MessageBox.Show("No inverted eight detected.");
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

    }
}
