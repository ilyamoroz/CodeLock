using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLock.State
{
    public class Door
    {
        public IDoorState currentState { get; set; }
        public Door(IDoorState state)
        {
            currentState = state;
        }
        public string OpenDoor()
        {
            currentState.OpenDoor(this);
            return "Unlock";
        }
        public string CloseDoor()
        {
            currentState.CloseDoor(this);
            return "Lock";
        }
    }
}
