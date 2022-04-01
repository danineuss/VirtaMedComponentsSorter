//#if UNITY_EDITOR
//using UnityEditor;
//#endif
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace VirtaMed.Unity.Common
{
    //[DisallowMultipleComponent]
    public class ComponentsSorter// : MonoBehaviour
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

        //public void SortComponents(List<Component> components)
        //{
        //    var sorter = new ComponentSorterIntoCategories();
            
        //    AllVanillaUnityComponentsUp(sorter);
        //    AllVirtamedComponentsDown(sorter);
        //    SortVirtamedComponents(sorter);
            
        //}

        //private void AllVanillaUnityComponentsUp(ComponentSorterIntoCategories sorter)
        //{
        //    sorter.Sort(gameObject.GetComponents(typeof(Component)));
        //    foreach (var component in sorter.UnityComponents)
        //    {
        //        if (component.position > sorter.SeparatorPosition)
        //        {
        //            MoveComponentUp(component, component.position - sorter.SeparatorPosition);
        //        }
        //    }
        //}

        //private void AllVirtamedComponentsDown(ComponentSorterIntoCategories sorter)
        //{
        //    sorter.Sort(gameObject.GetComponents(typeof(Component)));
        //    foreach (var component in sorter.VirtaComponents)
        //    {
        //        if (component.position < sorter.SeparatorPosition)
        //        {
        //            MoveComponentDown(component, sorter.SeparatorPosition - component.position);
        //        }
        //    }
        //}
 
        //private void SortVirtamedComponents(ComponentSorterIntoCategories sorter)
        //{
        //    foreach (var toSort in Enumerable.Reverse(sortOrder))
        //    {
        //        sorter.Sort(gameObject.GetComponents(typeof(Component)), toSort);
        //        if (sorter.FoundComponents.Count > 0)
        //        {
        //            foreach (var component in sorter.FoundComponents)
        //            {
        //                MoveComponentUp(component, (component.position - sorter.SeparatorPosition) - 1);
        //            }
        //        }
        //    }
        //}

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
