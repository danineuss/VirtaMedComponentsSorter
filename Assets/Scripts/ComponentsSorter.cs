using System;
using System.Collections.Generic;
using System.Linq;

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

        private readonly List<string> _sortOrder = new List<string>()
        {
            "FuseRenderMeshComponent",
            "FuseTetMeshComponent",
            "FuseTriMeshComponent",
            "FuseTetMeshDistanceFieldComponent",
            "FuseCobaComponent",
            "FuseCobaMaterialComponent",
            "FuseCobaDeformableFixationComponent",
            "FuseCobaLabelFixationComponent",
            "FuseCuttableComponent",
            "FuseGraspedBodyComponent",
            "FusePaintableComponent",
            "SoftBodyAnatomy",
            "OrganHapticsConfigurator"
        };

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

        public void SortVirtamedComponents(List<IComponentWithIndex> componentWithIndices)
        {
            foreach (var toSort in Enumerable.Reverse(_sortOrder))
            {
                _componentsCategorizer.Sort(componentWithIndices, toSort);
                if (_componentsCategorizer.FoundComponents.Count <= 0)
                    continue;

                foreach (var component in _componentsCategorizer.FoundComponents)
                {
                    var componentMovementArgs = new ComponentMovementArgs(
                        component, component.Position - _componentsCategorizer.SeparatorPosition - 1);
                    MoveComponentEvent?.Invoke(this, componentMovementArgs);
                }
            }
        }
    }
}
