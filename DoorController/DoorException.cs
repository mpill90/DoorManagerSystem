using System;
using System.Collections.Generic;
using System.Text;

namespace DoorManager
{
    class DoorException : Exception
    {
        public DoorException(string message) : base(message)
        {

        }
    }
}
