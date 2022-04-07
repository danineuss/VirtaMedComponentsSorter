/*
* (c) 2021 VirtaMed AG - Strictly Confidential - All rights reserved
*
* This document contains unpublished, confidential and proprietary information
* of VirtaMed AG. No disclosure or use of any portion of the contents of these
* materials may be made without the express consent of VirtaMed AG.
*/

#if UNITY_EDITOR
#endif
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts
{
    public class ComponentsCategorizer : IComponentsCategorizer
    {
        public List<IComponentWithIndex> VirtaComponents { get; private set; }
        public List<IComponentWithIndex> UnityComponents { get; private set; }
        public List<IComponentWithIndex> FoundComponents { get; private set; }

        public int SeparatorPosition { get; private set; }

        private readonly string _separatorClassType;
        private readonly List<string> _virtamedIdentifiers;

        public ComponentsCategorizer(string separatorClassType, List<string> virtamedIdentifiers)
        {
            _separatorClassType = separatorClassType;
            _virtamedIdentifiers = virtamedIdentifiers;
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
                    FoundComponents.Add(new ComponentWithIndex(counter, component.Component));

                // Separate unity and Virtamed component into two lists.
                // This sorter script marks as separation between VirtaMed and Unity components
                if (_separatorClassType.Contains(componentType))
                {
                    SeparatorPosition = counter++;
                    continue;
                }

                if (IsVirtamedComponents(componentType))
                    VirtaComponents.Add(new ComponentWithIndex(counter, component.Component));
                else
                    UnityComponents.Add(new ComponentWithIndex(counter, component.Component));
                
                counter++;
            }
        }

        private bool IsVirtamedComponents(string componentType)
        {
            return _virtamedIdentifiers.Any(virtamedIdentifier => componentType.Contains(virtamedIdentifier));
        }
    }
}
