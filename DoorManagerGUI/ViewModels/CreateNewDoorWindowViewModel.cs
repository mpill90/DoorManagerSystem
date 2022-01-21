using Client;
using Shared;
using System;
using System.Windows;

namespace DoorManagerGUI.ViewModels
{
    public class CreateNewDoorWindowViewModel : BaseViewModel
    {
        public string Label { get; set; }
        public bool IsClosed { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        private ClientConnection Client;
        public CreateNewDoorWindowViewModel(ClientConnection client)
        {
            Client = client;
        }

        internal void CreateNewDoor()
        {
            //TODO dedicated factory for door. The current way is not clean
            var door = new Door();
            door.Label = Label;
            door.IsClosed = IsClosed;
            door.IsLocked = IsLocked;

            try
            {
                Client.CreateDoor(door);
                MessageBox.Show("Door created");
            }
            catch (Exception ex)
            {
                //TODO log
                MessageBox.Show(ex.Message);
            }
        }
    }
}
