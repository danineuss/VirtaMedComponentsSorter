/*
* (c) 2021 VirtaMed AG - Strictly Confidential - All rights reserved
*
* This document contains unpublished, confidential and proprietary information
* of VirtaMed AG. No disclosure or use of any portion of the contents of these
* materials may be made without the express consent of VirtaMed AG.
*/

#if UNITY_EDITOR
#endif
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class ComponentsCategorizer
    {
        public List<IComponentWithIndex> VirtaComponents;
        public List<IComponentWithIndex> UnityComponents;
        public List<IComponentWithIndex> FoundComponents;

        public int SeparatorPosition;

        private readonly string _separatorClassType;

        public ComponentsCategorizer(string separatorClassType)
        {
            _separatorClassType = separatorClassType;
        }

        public void Sort(List<IComponentWithIndex> componentsWithIndex, string nameFilter = "")
        {
            VirtaComponents = new List<IComponentWithIndex>();
            UnityComponents = new List<IComponentWithIndex>();
            FoundComponents = new List<IComponentWithIndex>();
            SeparatorPosition = 0;

            var counter = 0;
            foreach (var component in componentsWithIndex)
            {
                var componentType = component.TypeString;

                if (componentType.Contains(nameFilter))
                {
                    FoundComponents.Add(new ComponentWithIndex(counter, component.Component));
                }

                // Separate unity and Virtamed component into two lists.
                // This sorter script marks as separatation between VirtaMed and Unity components
                if (_separatorClassType.Contains(componentType))
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
                        VirtaComponents.Add(new ComponentWithIndex(counter, component.Component));
                    }
                    else
                    {
                        UnityComponents.Add(new ComponentWithIndex(counter, component.Component));
                    }
                }
                counter += 1;
            }
        }
    }
}
