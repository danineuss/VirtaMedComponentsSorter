using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class ComponentsCategorizerTests
    {
        private ComponentsCategorizer _componentsCategorizer;

        [Test, TestCaseSource(nameof(FindAllComponentsWithoutNameFilterCaseSource))]
        public void Sort_ShouldFindAllComponentsWithoutNameFilter(
            Component[] components, string nameFilter, List<IComponentWithIndex> expectedComponentsFound)
        {
            // Arrange
            var a = components.First();
            var b = a.GetType().ToString();
            
            // Act
            _componentsCategorizer.Sort(components);
            
            // Assert
            _componentsCategorizer.FoundComponents()
        }

        private static IEnumerable<TestCaseData> FindAllComponentsWithoutNameFilterCaseSource()
        {
            var type = Substitute.For<Type>();
            type.ToString().Returns("foo");
            component.GetType().ToString().Returns("foo");
            Component b = new Component();
            Component[] a = new List<Component>() { b }.ToArray();
            yield return new TestCaseData(
                component, "bla", new List<ComponentWithIndex>() 
            );
        }
    }
}
