using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private const string _cSeparatorClassType = "Tests.EditMode.VirtamedSeparatorComponent";

        [Test, TestCaseSource(nameof(FindAllComponentsWithoutNameFilterCaseSource))]
        public void Sort_ShouldFindAllComponentsWithoutNameFilter(
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
            var gameObject = new GameObject("Abc");
            var componentWithIndexA = Substitute.For<IComponentWithIndex>();
            componentWithIndexA.Component.Returns(gameObject.AddComponent<VirtamedComponent>());
            componentWithIndexA.Position.Returns(0);
            componentWithIndexA.TypeString.Returns("Assets.Scripts.VirtamedComponent");
            
            var separatorComponent = Substitute.For<IComponentWithIndex>();
            separatorComponent.Component.Returns(gameObject.AddComponent<VirtamedSeparatorComponent>());
            separatorComponent.Position.Returns(1);
            separatorComponent.TypeString.Returns(_cSeparatorClassType);
            
            var componentWithIndexB = Substitute.For<IComponentWithIndex>();
            componentWithIndexB.Component.Returns(gameObject.AddComponent<DummyComponent>());
            componentWithIndexB.Position.Returns(2);
            componentWithIndexB.TypeString.Returns("Tests.EditMode.DummyComponent");

            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    componentWithIndexA,
                    separatorComponent,
                    componentWithIndexB
                }, 
                "",
                new List<IComponentWithIndex>()
                {
                    componentWithIndexA,
                    separatorComponent,
                    componentWithIndexB
                },
                new List<IComponentWithIndex>()
                {
                    componentWithIndexB
                },
                new List<IComponentWithIndex>()
                {
                    componentWithIndexA
                },
                1
            );
        }

        static bool ComponentWithIndexListEqual(List<IComponentWithIndex> lhs, List<IComponentWithIndex> rhs)
        {
            if (lhs.Count != rhs.Count)
                return false;

            for (int i = 0; i < lhs.Count; i++)
            {
                IComponentWithIndex leftComponent = lhs[i];
                IComponentWithIndex rightComponent = rhs[i];
                var a = leftComponent.Component.GetType();
                if (leftComponent.Component.GetType() != rightComponent.Component.GetType() ||
                    leftComponent.Position != rightComponent.Position ||
                    leftComponent.TypeString != rightComponent.TypeString)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
