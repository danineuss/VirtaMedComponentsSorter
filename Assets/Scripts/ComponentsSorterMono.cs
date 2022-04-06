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

namespace Assets.Scripts
{
    [DisallowMultipleComponent]
    public partial class ComponentsSorterMono : MonoBehaviour
    {
        private ComponentsSorter _componentsSorter = null;

        public void SortComponents()
        {
            if (_componentsSorter == null)
                InitializeComponentsSorter();

            _componentsSorter.SortComponents(gameObject.GetComponents(typeof(Component)));
        }

        private void InitializeComponentsSorter()
        {
            _componentsSorter = new ComponentsSorter(new ComponentsCategorizer(this.GetType().ToString()));

            _componentsSorter.MoveComponentDownEvent += MoveComponentDownEvent;
            _componentsSorter.MoveComponentUpEvent += MoveComponentUpEvent;
        }

        private void MoveComponentDownEvent(object sender, ComponentMovementArgs e)
        {
            int numberOfMovements = e.NumberOfMovements;
            while (numberOfMovements > 0)
            {
#if UNITY_EDITOR
                UnityEditorInternal.ComponentUtility.MoveComponentDown(e.ComponentWithIndex.component);
#endif
                numberOfMovements--;
            }
        }

        private void MoveComponentUpEvent(object sender, ComponentMovementArgs e)
        {
            int numberOfMovements = e.NumberOfMovements;
            while (numberOfMovements > 0)
            {
#if UNITY_EDITOR
                UnityEditorInternal.ComponentUtility.MoveComponentUp(e.ComponentWithIndex.component);
#endif
                numberOfMovements--;
            }
        }
    }
}
