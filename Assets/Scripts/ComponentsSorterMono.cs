/*
* (c) 2021 VirtaMed AG - Strictly Confidential - All rights reserved
*
* This document contains unpublished, confidential and proprietary information
* of VirtaMed AG. No disclosure or use of any portion of the contents of these
* materials may be made without the express consent of VirtaMed AG.
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [DisallowMultipleComponent]
    public class ComponentsSorterMono : MonoBehaviour
    {
        private ComponentsSorter _componentsSorter;

        private readonly List<string> _virtamedIdentifiers = new List<string>
        {
            "Virta",
            "Fuse",
            "SoftBody",
            "OrganHaptics",
            "ICG"
        };

        public void SortComponents()
        {
            if (_componentsSorter == null)
                InitializeComponentsSorter();

            _componentsSorter.AllVanillaUnityComponentsUp(ComponentWithIndices());
            _componentsSorter.AllVirtamedComponentsDown(ComponentWithIndices());
            _componentsSorter.SortVirtamedComponents(ComponentWithIndices());
       }

        private void InitializeComponentsSorter()
        {
            _componentsSorter = new ComponentsSorter(new ComponentsCategorizer(
                this.GetType().ToString(), _virtamedIdentifiers));

            _componentsSorter.MoveComponentEvent += ComponentsSorterOnMoveComponentEvent;
        }

        private void ComponentsSorterOnMoveComponentEvent(object sender, ComponentMovementArgs e)
        {
            if (e.NumberOfMovements > 0)
                MoveComponentUp(e.ComponentWithIndex.Component, e.NumberOfMovements);
            else
                MoveComponentDown(e.ComponentWithIndex.Component, -e.NumberOfMovements);
        }

        private static void MoveComponentUp(Component component, int numberOfMovements)
        {
            while (numberOfMovements > 0)
            {
#if UNITY_EDITOR
                UnityEditorInternal.ComponentUtility.MoveComponentUp(component);
#endif
                numberOfMovements--;
            }
        }
        
        private static void MoveComponentDown(Component component, int numberOfMovements)
        {
            while (numberOfMovements > 0)
            {
#if UNITY_EDITOR
                UnityEditorInternal.ComponentUtility.MoveComponentDown(component);
#endif
                numberOfMovements--;
            }
        }

        private List<IComponentWithIndex> ComponentWithIndices()
        {
            var components = gameObject.GetComponents(typeof(Component));
            
            var position = 0;
            return new List<IComponentWithIndex>(
                components.Select(x => new ComponentWithIndex(position++, x)));
        }
    }
}
