using System;
using System.Windows;

namespace CodeLock.State
{
    public class OpenDoorState : IDoorState
    {
        public void CloseDoor(Door door)
        {
            door.TransitionTo(new CloseDoorState());
        }

        public void OpenDoor(Door door)
        {
            MessageBox.Show("Door is Unlock");
        }
    }
}
