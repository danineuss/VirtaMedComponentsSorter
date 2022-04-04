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
    public partial class ComponentsSorterMono : MonoBehaviour
    {
        private ComponentsSorter _componentsSorter = null;
        
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
            if (_componentsSorter == null)
                InitializeComponentsSorter();

            var sorter = new ComponentsCategorizer(this.GetType().ToString());

            AllVanillaUnityComponentsUp(sorter);
            AllVirtamedComponentsDown(sorter);
            SortVirtamedComponents(sorter);
        }

        private void InitializeComponentsSorter()
        {
            _componentsSorter = new ComponentsSorter();
        }

        private void AllVanillaUnityComponentsUp(ComponentsCategorizer sorter)
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

        private void AllVirtamedComponentsDown(ComponentsCategorizer sorter)
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

        private void SortVirtamedComponents(ComponentsCategorizer sorter)
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
    }
}
