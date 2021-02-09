using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLock.State
{
    public interface IDoorState
    {
        void OpenDoor(Door door);
        void CloseDoor(Door door);
    }
}
