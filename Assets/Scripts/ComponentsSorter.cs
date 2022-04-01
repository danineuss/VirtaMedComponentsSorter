/*
* (c) 2021 VirtaMed AG - Strictly Confidential - All rights reserved
*
* This document contains unpublished, confidential and proprietary information
* of VirtaMed AG. No disclosure or use of any portion of the contents of these
* materials may be made without the express consent of VirtaMed AG.
*/

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace VirtaMed.Unity.Common
{
    [DisallowMultipleComponent]
    public class ComponentsSorter : MonoBehaviour
    {
        private readonly List<string> sortOrder = new List<string>
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

        public void SortComponents()
        {
            var sorter = new ComponentSorterIntoCategories();
            
            AllVanillaUnityComponentsUp(sorter);
            AllVirtamedComponentsDown(sorter);
            SortVirtamedComponents(sorter);
            
        }

        private void AllVanillaUnityComponentsUp(ComponentSorterIntoCategories sorter)
        {
            sorter.Sort(gameObject.GetComponents(typeof(Component)));
            foreach (var component in sorter.UnityComponents)
            {
                if (component.position > sorter.SeparatorPosition)
                {
                    MoveComponentUp(component, component.position - sorter.SeparatorPosition);
                }
            }
        }

        private void AllVirtamedComponentsDown(ComponentSorterIntoCategories sorter)
        {
            sorter.Sort(gameObject.GetComponents(typeof(Component)));
            foreach (var component in sorter.VirtaComponents)
            {
                if (component.position < sorter.SeparatorPosition)
                {
                    MoveComponentDown(component, sorter.SeparatorPosition - component.position);
                }
            }
        }
 
        private void SortVirtamedComponents(ComponentSorterIntoCategories sorter)
        {
            foreach (var toSort in Enumerable.Reverse(sortOrder))
            {
                sorter.Sort(gameObject.GetComponents(typeof(Component)), toSort);
                if (sorter.FoundComponents.Count > 0)
                {
                    foreach (var component in sorter.FoundComponents)
                    {
                        MoveComponentUp(component, (component.position - sorter.SeparatorPosition) - 1);
                    }
                }
            }
        }

        private static void MoveComponentDown(ComponentWithIndex component, int numberOfMovements)
        {
            while (numberOfMovements > 0)
            {
#if UNITY_EDITOR
                UnityEditorInternal.ComponentUtility.MoveComponentDown(component.component);
#endif
                numberOfMovements--;
            }
        }

        private static void MoveComponentUp(ComponentWithIndex component, int numberOfMovements)
        {
            while (numberOfMovements > 0)
            {
#if UNITY_EDITOR
                UnityEditorInternal.ComponentUtility.MoveComponentUp(component.component);
#endif
                numberOfMovements--;
            }
        }

        private class ComponentWithIndex
        {
            public int position;
            public Component component;

            public ComponentWithIndex(int position, Component component)
            {
                this.position = position;
                this.component = component;
            }
        }

        private class ComponentSorterIntoCategories
        {
            public List<ComponentWithIndex> VirtaComponents;
            public List<ComponentWithIndex> UnityComponents;
            public List<ComponentWithIndex> FoundComponents;

            public int SeparatorPosition;

            public void Sort(Component[] components, string nameFilter="")
            {
                VirtaComponents = new List<ComponentWithIndex>();
                UnityComponents = new List<ComponentWithIndex>();
                FoundComponents = new List<ComponentWithIndex>();
                SeparatorPosition = 0;

                int counter = 0;
                foreach (var component in components)
                {
                    var componentType = component.GetType().ToString();

                    if (componentType.Contains(nameFilter))
                    {
                        FoundComponents.Add(new ComponentWithIndex(counter, component));
                    }
                    
                    // Separate unity and Virtamed component into two lists.
                    // This sorter script marks as separatation between VirtaMed and Unity components
                    if (this.GetType().ToString().Contains(componentType))
                    {
                        SeparatorPosition = counter;
                    }
                    else
                    {
                        if (componentType.Contains("Virta") ||
                            componentType.Contains("Fuse") || 
                            componentType.Contains("SoftBody") || 
                            componentType.Contains("OrganHaptics") || 
                            componentType.Contains("ICG"))
                        {
                            VirtaComponents.Add(new ComponentWithIndex(counter, component));
                        }
                        else
                        {
                            UnityComponents.Add(new ComponentWithIndex(counter, component));
                        }
                    }
                    counter += 1;
                }
            }
        }
    }
}
