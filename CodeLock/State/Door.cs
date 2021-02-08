using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLock.State
{
    public class Door
    {
        IDoorState currentState { get; set; }
        public Door(IDoorState state)
        {
            currentState = state;
        }
        public string OpenDoor()
        {
            currentState.OpenDoor();
            return "Unlock";
        }
        public string CloseDoor()
        {
            currentState.CloseDoor();
            return "Lock";
        }
    }
}
