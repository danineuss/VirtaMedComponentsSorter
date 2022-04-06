using UnityEngine;

namespace Assets.Scripts
{
    public interface IComponentWithIndex
    {
        int Position { get; set; }
        Component Component { get; }
        string TypeString { get; }
    }

    public class ComponentWithIndex : IComponentWithIndex
    {
        public int Position { get; set; }
        public Component Component { get; }
        public string TypeString => Component.GetType().ToString();

        public ComponentWithIndex(int position, Component component)
        {
            Position = position;
            Component = component;
        }
    }
}