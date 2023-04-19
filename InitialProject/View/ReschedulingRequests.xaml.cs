using InitialProject.Model;
using InitialProject.Service;
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
    /// Interaction logic for ReschedulingRequests.xaml
    /// </summary>
    public partial class ReschedulingRequests : Window
    {

        private ReservationReschedulingRequestService reservationReschedulingRequestService = new ReservationReschedulingRequestService();

        
        public ReschedulingRequests()
        {
            InitializeComponent();
            InitializeRequests();
        }

        public void InitializeRequests()
        {
            reschedulingRequsts.ItemsSource = reservationReschedulingRequestService.GetAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationReschedulingRequest selectedRequest = reschedulingRequsts.SelectedItem as ReservationReschedulingRequest;

            if(PostponmentWarning(selectedRequest))
            {
                reservationReschedulingRequestService.DeclareRespond(comment.Text, RequestState.Approved, selectedRequest.Id);
            }

            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReservationReschedulingRequest selectedRequest = reschedulingRequsts.SelectedItem as ReservationReschedulingRequest;

            if (PostponmentWarning(selectedRequest))
            {
                reservationReschedulingRequestService.DeclareRespond(comment.Text, RequestState.Rejected, selectedRequest.Id);
            }

        }

        private bool PostponmentWarning(ReservationReschedulingRequest request)
        {
            bool continuePostponement;

            if (reservationReschedulingRequestService.CanBePostponed(request))
            {

                MessageBox.Show("Pomeranje rezervacije se moze izvrsiti");
                continuePostponement = true;
            }
            else
            {
                

                if (MessageBox.Show("Datumi su vec rezervisani.", "Upozorenje o pomeranju rezervacije", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    continuePostponement= true;
                } else
                {
                    continuePostponement = false;
                }
                
            }

            return continuePostponement;
            
        }
    }
}
