using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    /// <summary>
    /// Detects whether the name is "BOUHOURS"
    /// </summary>
    public class MagicNameDetecter
    {
        /// <summary>
        /// Checks whether the name is "BOUHOURS" and initializes the first name to "Jean-Claude" if so
        /// </summary>
        /// <param name="name">the last name</param>
        /// <param name="firstName">the first name to initialize</param>
        public void Detect(string name, out string firstName)
        {
            if (name.ToUpper().Equals("BOUHOURS"))
            {
                OnMagicNameDetected(new MagicNameDetectedEventArgs("Bienvenue à vous, Jean-Claude."));
                firstName = "Jean-Claude";
            }
            else
            {
                firstName = null;
            }
        }

        public event EventHandler<MagicNameDetectedEventArgs> MagicNameDetected;

        protected internal void OnMagicNameDetected(MagicNameDetectedEventArgs args)
        {
            MagicNameDetected?.Invoke(this, args);
        }
    }
}
