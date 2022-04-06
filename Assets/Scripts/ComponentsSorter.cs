using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        public event EventHandler<ComponentMovementArgs> MoveComponentUpEvent;
        public event EventHandler<ComponentMovementArgs> MoveComponentDownEvent;

        private readonly ComponentsCategorizer _componentsCategorizer;

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

        public ComponentsSorter(ComponentsCategorizer componentsCategorizer)
        {
            _componentsCategorizer = componentsCategorizer;
        }

        public void SortComponents(Component[] components)
        {
            AllVanillaUnityComponentsUp(components, _componentsCategorizer);
            AllVirtamedComponentsDown(components, _componentsCategorizer);
            SortVirtamedComponents(components, _componentsCategorizer);
        }

        private void AllVanillaUnityComponentsUp(Component[] components, ComponentsCategorizer sorter)
        {
            sorter.Sort(components);
            foreach (var component in sorter.UnityComponents)
            {
                if (component.position <= sorter.SeparatorPosition)
                    continue;

                MoveComponentUpEvent?.Invoke(
                    this,
                    new ComponentMovementArgs(component, component.position - sorter.SeparatorPosition));
            }
        }

        private void AllVirtamedComponentsDown(Component[] components, ComponentsCategorizer sorter)
        {
            sorter.Sort(components);
            foreach (var component in sorter.VirtaComponents)
            {
                if (component.position >= sorter.SeparatorPosition)
                    continue;

                MoveComponentDownEvent(
                    this,
                    new ComponentMovementArgs(component, sorter.SeparatorPosition - component.position));
            }
        }

        private void SortVirtamedComponents(Component[] components, ComponentsCategorizer sorter)
        {
            foreach (var toSort in Enumerable.Reverse(_sortOrder))
            {
                sorter.Sort(components, toSort);
                if (sorter.FoundComponents.Count <= 0)
                    continue;

                foreach (var component in sorter.FoundComponents)
                {
                    MoveComponentUpEvent?.Invoke(
                        this,
                        new ComponentMovementArgs(component, component.position - sorter.SeparatorPosition - 1));
                }
            }
        }
    }
}
