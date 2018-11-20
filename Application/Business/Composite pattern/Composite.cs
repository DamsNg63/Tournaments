using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Business
{
    /// <summary>
    /// Component inside the Composite design pattern
    /// </summary>
    [DataContract]
    public abstract class Composite : Component
    {
        [DataMember]
        private ObservableCollection<Component> Components { get; set; }
        public ReadOnlyObservableCollection<Component> ReadOnlyComponents => new ReadOnlyObservableCollection<Component>(Components);

        /// <summary>
        /// Composite constructor with no parameter, initializes the Compoonents list
        /// </summary>
        public Composite()
        {
            Components = new ObservableCollection<Component>();
        }

        public override void AddComponent(Component component)
        {
            if (Components.Contains(component))
            {
                throw new ArgumentException("Cet élément est déjà dans la liste.");
            }
            Components.Add(component);
        }

        public override void RemoveComponent(Component component)
        {
            Components.Remove(component);
        }
    }
}
