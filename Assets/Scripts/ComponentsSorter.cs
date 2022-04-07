using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class ComponentMovementArgs : EventArgs
    {
        public IComponentWithIndex ComponentWithIndex { get; }
        public int NumberOfMovements { get; }

        public ComponentMovementArgs(IComponentWithIndex componentWithIndex, int numberOfMovements)
        {
            ComponentWithIndex = componentWithIndex;
            NumberOfMovements = numberOfMovements;
        }
    }

    public class ComponentsSorter
    {
        public event EventHandler<ComponentMovementArgs> MoveComponentEvent;

        private readonly IComponentsCategorizer _componentsCategorizer;

        public ComponentsSorter(IComponentsCategorizer componentsCategorizer)
        {
            _componentsCategorizer = componentsCategorizer;
        }

        public void AllVanillaUnityComponentsUp(List<IComponentWithIndex> componentWithIndices)
        {
            _componentsCategorizer.Sort(componentWithIndices);
            foreach (var component in _componentsCategorizer.UnityComponents)
            {
                if (component.Position <= _componentsCategorizer.SeparatorPosition)
                    continue;

                var componentMovementArgs = new ComponentMovementArgs(
                    component, component.Position - _componentsCategorizer.SeparatorPosition);
                MoveComponentEvent?.Invoke(this, componentMovementArgs);
            }
        }

        public void AllVirtamedComponentsDown(List<IComponentWithIndex> componentWithIndices)
        {
            _componentsCategorizer.Sort(componentWithIndices);
            foreach (var component in _componentsCategorizer.VirtaComponents)
            {
                if (component.Position >= _componentsCategorizer.SeparatorPosition)
                    continue;

                var componentMovementArgs = new ComponentMovementArgs(
                    component, component.Position - _componentsCategorizer.SeparatorPosition);
                MoveComponentEvent?.Invoke(this, componentMovementArgs);
            }
        }

        public void SortVirtamedComponents(List<IComponentWithIndex> componentWithIndices, string nameFilter)
        {
            _componentsCategorizer.Sort(componentWithIndices, nameFilter);
            List<IComponentWithIndex> foundComponents = _componentsCategorizer.FoundComponents;
            if (foundComponents.Count <= 0)
                return;

            foreach (var component in foundComponents)
            {
                var componentMovementArgs = new ComponentMovementArgs(
                    component, component.Position - _componentsCategorizer.SeparatorPosition - 1);
                MoveComponentEvent?.Invoke(this, componentMovementArgs);
            }
        }
    }
}
