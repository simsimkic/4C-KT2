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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        public OwnerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow(); 
            reviewsWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReschedulingRequests reschedulingRequests = new ReschedulingRequests();
            reschedulingRequests.Show();
        }
    }
}
