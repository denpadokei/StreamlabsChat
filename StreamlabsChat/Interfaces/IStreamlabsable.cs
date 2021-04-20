using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamlabsChat.Interfaces
{
    public interface IStreamlabsable
    {
        event Action<string> OnReciveMessage;

        void OnMessage(string Message);
    }
}
