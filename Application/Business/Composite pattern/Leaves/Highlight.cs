using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Part of the Match detail
    /// </summary>
    [DataContract]
    public class Highlight : Component
    {
        [DataMember]
        public DateTime PostDate { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        /// <summary>
        /// Highlight constructor with 1 parameter, PostDate is initialized to the date when the post is created
        /// </summary>
        /// <param name="description">the description of the Highlight</param>
        public Highlight(string description)
        {
            PostDate    = DateTime.Now;
            Description = description.Trim();
        }

        public override void AddComponent(Component component)
        {
            throw new NotImplementedException("Cette opération est impossible sur un temps fort.");
        }
        
        public override void RemoveComponent(Component component)
        {
            throw new NotImplementedException("Cette opération est impossible sur un temps fort.");
        }
    }
}
