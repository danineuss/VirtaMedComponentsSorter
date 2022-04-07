using System.Collections.Generic;
using Assets.Scripts;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    internal class VirtamedSeparatorComponent : MonoBehaviour
    {
        public string Name { get; set; }
    }
    
    internal class DummyComponent : MonoBehaviour
    {
        public string Name { get; set; }
    }
    
    public class ComponentsCategorizerTests
    {
        private ComponentsCategorizer _componentsCategorizer;
        
        private static readonly GameObject DummyGameObject = new GameObject("Foo");
        
        private const string _cSeparatorClassType = "Tests.EditMode.VirtamedSeparatorComponent";

        [Test, TestCaseSource(nameof(FindAllComponentsWithoutNameFilterCaseSource))]
        public void Sort_ShouldFindAllComponents(
            List<IComponentWithIndex> components, 
            string nameFilter,
            List<IComponentWithIndex> expectedFoundComponents,
            List<IComponentWithIndex> expectedUnityComponents,
            List<IComponentWithIndex> expectedVirtaComponents,
            int expectedSeparatorPosition)
        {
            // Arrange
            _componentsCategorizer = new ComponentsCategorizer(_cSeparatorClassType);
            
            // Act
            _componentsCategorizer.Sort(components, nameFilter);
            
            // Assert
            Assert.True(ComponentWithIndexListEqual(expectedFoundComponents, _componentsCategorizer.FoundComponents));
            Assert.True(ComponentWithIndexListEqual(expectedUnityComponents, _componentsCategorizer.UnityComponents));
            Assert.True(ComponentWithIndexListEqual(expectedVirtaComponents, _componentsCategorizer.VirtaComponents));
            Assert.AreEqual(expectedSeparatorPosition, _componentsCategorizer.SeparatorPosition);
        }

        private static IEnumerable<TestCaseData> FindAllComponentsWithoutNameFilterCaseSource()
        {
            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    VirtamedComponentSubstitute(0),
                    SeparatorComponentSubstitute(1),
                    UnityComponentSubstitute(2)
                },
                "",
                new List<IComponentWithIndex>()
                {
                    VirtamedComponentSubstitute(0),
                    SeparatorComponentSubstitute(1),
                    UnityComponentSubstitute(2)
                },
                new List<IComponentWithIndex>()
                {
                    UnityComponentSubstitute(2)
                },
                new List<IComponentWithIndex>()
                {
                    VirtamedComponentSubstitute(0)
                },
                1
            );
        }

        static IComponentWithIndex VirtamedComponentSubstitute(int position)
        {
            var component = Substitute.For<IComponentWithIndex>();
            component.Component.Returns(DummyGameObject.AddComponent<VirtamedComponent>());
            component.Position.Returns(position);
            component.TypeString.Returns("Assets.Scripts.VirtamedComponent");
            return component;
        }
        
        static IComponentWithIndex SeparatorComponentSubstitute(int position)
        {
            var component = Substitute.For<IComponentWithIndex>();
            component.Component.Returns(DummyGameObject.AddComponent<VirtamedSeparatorComponent>());
            component.Position.Returns(position);
            component.TypeString.Returns(_cSeparatorClassType);
            return component;
        }
        
        static IComponentWithIndex UnityComponentSubstitute(int position)
        {
            var component = Substitute.For<IComponentWithIndex>();
            component.Component.Returns(DummyGameObject.AddComponent<DummyComponent>());
            component.Position.Returns(position);
            component.TypeString.Returns("Tests.EditMode.DummyComponent");
            return component;
        }

        static bool ComponentWithIndexListEqual(List<IComponentWithIndex> lhs, List<IComponentWithIndex> rhs)
        {
            if (lhs.Count != rhs.Count)
                return false;

            for (int i = 0; i < lhs.Count; i++)
            {
                var lhsType = lhs[i].Component.GetType().ToString();
                var rhsType = rhs[i].Component.GetType().ToString();
                
                if (lhsType != rhsType ||
                    lhs[i].Position != rhs[i].Position ||
                    lhs[i].TypeString != rhs[i].TypeString)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
