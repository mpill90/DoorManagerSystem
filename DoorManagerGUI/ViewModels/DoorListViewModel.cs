using Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using DoorManager;
using System.Threading;
using System.Linq;
using DoorManagerGUI.Windows;
using System.Threading.Tasks;
using Shared.Helpers;
using Shared.Config;

namespace DoorManagerGUI.ViewModels
{
    public class DoorListViewModel : BaseViewModel
    {
        public ObservableCollection<Door> Doors { get; set; }
        public Door SelectedDoor { get; set; }
        public Door EditedDoor { get; set; }
        
        private ClientConnection Client;
        private string IPAddress = Configuration.IPAddress;
        private readonly Timer Timer;

        //TODO dismantle this ViewModel into smaller chunks
        public DoorListViewModel()
        {
            try
            {
                Client = new ClientConnection(IPAddress);
                //TODO this polling is the cheap and dirty way, redo this as websocket based
                Timer = new Timer(RunPeriodicTask, null, 0, 250);
            }
            catch (Exception ex)
            {
                //TODO log exception
                MessageBox.Show(ex.Message);
            }
        }

        private void RunPeriodicTask(object state)
        {
            UpdateDoorsView();
        }

        internal async void HandleDoorOpenClose()
        {
            try
            {
                using (var doorControl = new DoorStateController(EditedDoor))
                {
                    if (EditedDoor.IsClosed)
                    {
                        doorControl.Open();
                    }
                    else
                    {
                        doorControl.Close();
                    }
                    await UpdateDoor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task UpdateDoor()
        {
            await Client.UpdateDoor(EditedDoor);
            var list = Client.GetDoors().Result;
            LoadDoors(list);
        }

        internal async void HandleDoorDeletion()
        {
            if (EditedDoor != null)
            {
                await Client.DeleteDoor(EditedDoor.Id);
            }
        }

        internal void UpdateSelection()
        {
            EditedDoor = SelectedDoor;
        }

        internal void HandleDoorCreation()
        {
            var createNewDoorWindow = new CreateNew(Client);
            createNewDoorWindow.Show();
        }

        internal async void HandleDoorLockUnlock()
        {
            try
            {
                using (var doorControl = new DoorStateController(EditedDoor))
                {
                    if (EditedDoor.IsLocked)
                    {
                        doorControl.UnLock();
                    }
                    else
                    {
                        doorControl.Lock();
                    }
                    await UpdateDoor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal async void SaveLabel()
        {
            try
            {
                if (EditedDoor != null)
                {
                    await UpdateDoor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDoorsView()
        {
            var refreshedDoors = Client.GetDoors().Result;

            if (Doors is null)
            {
                LoadDoors(refreshedDoors);
            }
            else
            {
                UpdateIfNeeded(refreshedDoors);
            }
        }

        private void UpdateIfNeeded(List<Door> refreshedDoors)
        {
            if (!Doors.SequenceEqual(refreshedDoors, new DoorEqualityComparer()))
            {
                LoadDoors(refreshedDoors);
            }
        }

        private void LoadDoors(List<Door> list)
        {
            Doors = new ObservableCollection<Door>(list);
        }
    }
}
