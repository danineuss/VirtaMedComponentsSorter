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

namespace VirtaMed.Unity.Common
{
    public class ComponentWithIndex
    {
        public int position;
        public Component component;

        public ComponentWithIndex(int position, Component component)
        {
            this.position = position;
            this.component = component;
        }
    }

    public class ComponentsCategorizer
    {
        public List<ComponentWithIndex> VirtaComponents;
        public List<ComponentWithIndex> UnityComponents;
        public List<ComponentWithIndex> FoundComponents;

        public int SeparatorPosition;

        private readonly string _separatorClassType;

        public ComponentsCategorizer(string separatorClassType)
        {
            _separatorClassType = separatorClassType;
        }

        public void Sort(Component[] components, string nameFilter = "")
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
