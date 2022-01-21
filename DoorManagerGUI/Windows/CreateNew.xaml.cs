using Client;
using DoorManagerGUI.ViewModels;
using System.Windows;

namespace DoorManagerGUI.Windows
{
    /// <summary>
    /// Interaction logic for CreateNew.xaml
    /// </summary>
    public partial class CreateNew : Window
    {
        CreateNewDoorWindowViewModel ViewModel;
        public CreateNew(ClientConnection client)
        {
            ViewModel = new CreateNewDoorWindowViewModel(client);
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => ViewModel.CreateNewDoor();
    }
}
