using System;
using System.Collections.Generic;
using System.IO;
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

namespace ink_recognizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();

                line.Stroke = SystemColors.WindowFrameBrush;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;

                currentPoint = e.GetPosition(this);
                this.canvas.Children.Add(line);
            }
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                currentPoint = e.GetPosition(this);
        }

        private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.canvas.Children.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                theInkCanvas.Strokes.Save(ms);
                var myInkCollector = new InkCollector();
                var ink = new Ink();
                ink.Load(ms.ToArray());

                using (RecognizerContext context = new RecognizerContext())
                {
                    if (ink.Strokes.Count > 0)
                    {
                        context.Strokes = ink.Strokes;
                        RecognitionStatus status;

                        var result = context.Recognize(out status);

                        if (status == RecognitionStatus.NoError)
                            textBox1.Text = result.TopString;
                        else
                            MessageBox.Show("Recognition failed");

                    }
                    else
                        MessageBox.Show("No stroke detected");
                }
            }

            InkRecognizerManager manager = new InkRecognizerManager();
            string result = manager.Request(GetRequest()).GetAwaiter().GetResult();
        }

        private InkRecognizerRequest GetRequest()
        {
            InkRecognizerRequest r = new InkRecognizerRequest();
            r.language = "fr-FR";
            r.version = 1;
            int id = 1;

            foreach(var c in this.canvas.Children)
            {
                if(c is Line)
                {
                    Line line = (Line)c;
                    
                }
            }




            return r;
        }
    }
}
