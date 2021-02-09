using System;
using System.Windows;

namespace CodeLock.State
{
    public class CloseDoorState : IDoorState
    {
        public void CloseDoor(Door door)
        {
            MessageBox.Show("Door is Lock");
        }

        public void OpenDoor(Door door)
        {
            door.currentState =  new OpenDoorState();
        }
    }
}
