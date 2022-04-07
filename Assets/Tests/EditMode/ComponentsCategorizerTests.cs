using System.Collections.Generic;
using Assets.Scripts;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class ComponentsCategorizerTests
    {
        private ComponentsCategorizer _componentsCategorizer;

        private const string _cSeparatorClassType = "Tests.EditMode.VirtamedSeparatorComponent";

        [Test, TestCaseSource(nameof(ShouldFindAllComponentsCaseSource))]
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
            Assert.True(Helper.ComponentWithIndexListEqual(
                expectedFoundComponents, _componentsCategorizer.FoundComponents));
            Assert.True(Helper.ComponentWithIndexListEqual(
                expectedUnityComponents, _componentsCategorizer.UnityComponents));
            Assert.True(Helper.ComponentWithIndexListEqual(
                expectedVirtaComponents, _componentsCategorizer.VirtaComponents));
            Assert.AreEqual(expectedSeparatorPosition, _componentsCategorizer.SeparatorPosition);
        }

        private static IEnumerable<TestCaseData> ShouldFindAllComponentsCaseSource()
        {
            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.SeparatorComponentSubstitute(1, _cSeparatorClassType),
                    Helper.UnityComponentSubstitute(2)
                },
                "",
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.SeparatorComponentSubstitute(1, _cSeparatorClassType),
                    Helper.UnityComponentSubstitute(2)
                },
                new List<IComponentWithIndex>()
                {
                    Helper.UnityComponentSubstitute(2)
                },
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0)
                },
                1
            );
            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(2),
                    Helper.VirtamedComponentSubstitute(3),
                    Helper.VirtamedComponentSubstitute(4)
                },
                "",
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(2),
                    Helper.VirtamedComponentSubstitute(3),
                    Helper.VirtamedComponentSubstitute(4)
                },
                new List<IComponentWithIndex>(),
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(2),
                    Helper.VirtamedComponentSubstitute(3),
                    Helper.VirtamedComponentSubstitute(4)
                },
                0
            );
            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    Helper.UnityComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                },
                "",
                new List<IComponentWithIndex>()
                {
                    Helper.UnityComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                },
                new List<IComponentWithIndex>()
                {
                    Helper.UnityComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                },
                new List<IComponentWithIndex>(),
                0
            );
            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    Helper.SeparatorComponentSubstitute(0, _cSeparatorClassType),
                },
                "",
                new List<IComponentWithIndex>()
                {
                    Helper.SeparatorComponentSubstitute(0, _cSeparatorClassType),
                },
                new List<IComponentWithIndex>(),
                new List<IComponentWithIndex>(),
                0
            );
            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.SeparatorComponentSubstitute(2, _cSeparatorClassType),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.VirtamedComponentSubstitute(5)
                },
                "Dummy",
                new List<IComponentWithIndex>()
                {
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                },
                new List<IComponentWithIndex>()
                {
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                },
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.VirtamedComponentSubstitute(5)
                },
                2
            );
            yield return new TestCaseData(
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.SeparatorComponentSubstitute(2, _cSeparatorClassType),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.VirtamedComponentSubstitute(5)
                },
                "Non-applicable",
                new List<IComponentWithIndex>(),
                new List<IComponentWithIndex>()
                {
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                },
                new List<IComponentWithIndex>()
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.VirtamedComponentSubstitute(5)
                },
                2
            );
        }
    }
}
