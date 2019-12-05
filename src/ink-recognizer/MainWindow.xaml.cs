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



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InkRecognizerManager manager = new InkRecognizerManager();
            string result = manager.Request(GetRequest());
            this.result.Text = result;
        }

        private InkRecognizerRequest GetRequest()
        {
            InkRecognizerRequest r = new InkRecognizerRequest();
            r.language = "fr-FR";
            r.version = 1;
            int id = 1;


            foreach (var c in this.canvas.Strokes)
                r.AddStroke(id++, c);

            return r;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            canvas.Strokes.Clear();
            this.result.Text = "";
        }
    }
}
