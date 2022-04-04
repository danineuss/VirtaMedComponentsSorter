using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VirtaMed.Unity.Common;

namespace Tests
{
    public class ComponentsCategorizer_Tests
    {
        private ComponentsCategorizer _componentsCategorizer;

        [Test, TestCaseSource("FindAllComponentsWithoutNameFilterCaseSource")]
        public void Sort_ShouldFindAllComponentsWithoutNameFilter(
            Component[] components, string nameFilter, List<ComponentWithIndex> expectedComponentsFound)
        {
            var a = components.First();
            var b = a.GetType().ToString();
        }

        private static IEnumerable<TestCaseData> FindAllComponentsWithoutNameFilterCaseSource()
        {
            var component = Substitute.For<Component>();
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
