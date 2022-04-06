using UnityEngine;

namespace Assets.Scripts
{
    public class ComponentWithIndex
    {
        public int Position { get; }
        public Component Component { get; }
        public string TypeString => Component.GetType().ToString();

        public ComponentWithIndex(int position, Component component)
        {
            Position = position;
            Component = component;
        }
    }
}