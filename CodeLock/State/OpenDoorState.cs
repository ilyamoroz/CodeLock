using System;
using System.Windows;

namespace CodeLock.State
{
    public class OpenDoorState : IDoorState
    {
        Door door;
        public void CloseDoor()
        {
            door = new Door(new CloseDoorState());
        }

        public void OpenDoor()
        {
            MessageBox.Show("Door is Unlock");
        }
    }
}
