using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    /// <summary>
    /// Our personal event
    /// </summary>
    public class MagicNameDetectedEventArgs : EventArgs
    {
        public string Message { get; private set; }

        /// <summary>
        /// MagicNameDetectedEventArgs with 1 parameter
        /// </summary>
        /// <param name="message">the message that will be showed in a MessageBox</param>
        public MagicNameDetectedEventArgs(string message)
        {
            Message = message;
        }
    }
}
