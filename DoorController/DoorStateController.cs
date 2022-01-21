using Shared;
using System;

namespace DoorManager
{
    public class DoorStateController : IDisposable, IDoorManagable
    {
        private Door Door;
        public DoorStateController(Door door)
        {
            if (door == null)
            {
                throw new DoorException("No door selected.");
            }
            Door = door;
        }

        public void Open()
        {
            if (Door.IsLocked)
            {
                throw new DoorException("Can not open locked door. Please Unlock it first.");
            }

            if (!Door.IsClosed)
            {
                throw new DoorException("Invalid operation. Door is already opened.");
            }

            Door.IsClosed = false;
        }

        public void Close()
        {
            if (!Door.IsClosed)
            {
                Door.IsClosed = true;
            }
            else
            {
                throw new DoorException("Invalid operation. Door is already Closed");
            }
        }

        public void Lock()
        {
            if (!Door.IsClosed)
            {
                throw new DoorException("Can not lock opened door. Please close it first.");
            }

            if (Door.IsLocked)
            {
                throw new DoorException("Invalid operation. Door is already closed.");
            }

            Door.IsLocked = true;
        }

        public void UnLock()
        {
            if (Door.IsLocked)
            {
                Door.IsLocked = false;
            }
            else
            {
                throw new DoorException("Invalid operation. Door is already Unlocked");
            }
        }

        public void Dispose()
        {
        }
    }
}
