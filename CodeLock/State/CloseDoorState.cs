using System;
using System.Windows;

namespace CodeLock.State
{
    public class CloseDoorState : IDoorState
    {
        Door door;
        public void CloseDoor()
        {
            MessageBox.Show("Door is Lock");
        }

        public void OpenDoor()
        {
            door = new Door(new OpenDoorState());
        }
    }
}
