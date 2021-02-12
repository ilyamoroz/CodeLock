using System;

namespace CodeLock.State
{
    public class Door
    {
        public IDoorState currentState { get; set; }
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
        }
        public void Close()
        {
            currentState.CloseDoor(this);
        }
    }
}
