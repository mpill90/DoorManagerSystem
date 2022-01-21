using DoorManagerGUI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace DoorManagerGUI.Controls
{
    /// <summary>
    /// Interaction logic for DoorList.xaml
    /// </summary>
    public partial class DoorList : UserControl
    {
        internal DoorListViewModel ViewModel = new DoorListViewModel();
        public DoorList()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) => ViewModel.SaveLabel();

        private void OpenCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.HandleDoorOpenClose();
            UpdateOpenCloseBtn();
            UpdateLockUnlockBtn();
        }

        private void LockUnlockBtn_Click(object sender, RoutedEventArgs e) 
        {
            ViewModel.HandleDoorLockUnlock();
            UpdateOpenCloseBtn();
            UpdateLockUnlockBtn();
        }

        private void DoorsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.UpdateSelection();
            UpdateOpenCloseBtn();
            UpdateLockUnlockBtn();
        }

        private void UpdateOpenCloseBtn()
        {
            if (ViewModel.EditedDoor != null)
            {
                OpenCloseBtn.IsChecked = ViewModel.EditedDoor.IsClosed;
            }
        }

        private void UpdateLockUnlockBtn()
        {
            if (ViewModel.EditedDoor != null)
            {
                LockUnlockBtn.IsChecked = ViewModel.EditedDoor.IsLocked;
            }
        }

        private void CreateNewBtn_Click(object sender, RoutedEventArgs e) => ViewModel.HandleDoorCreation();

        private void DeleteBtn_Click(object sender, RoutedEventArgs e) => ViewModel.HandleDoorDeletion();      
    }
}
