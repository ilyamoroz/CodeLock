using System;

namespace CodeLock.State
{
    public class Door
    {
        IDoorState currentState { get; set; }
        public DoorState state;
        public Door(IDoorState state)
        {
            this.TransitionTo(state);
        }
        public void TransitionTo(IDoorState state)
        {
            currentState = state;
        }
        public void Open()
        {
            currentState.OpenDoor(this);
            state = DoorState.Open;
        }
        public void Close()
        {
            currentState.CloseDoor(this);
            state = DoorState.Close;
        }
    }
}
