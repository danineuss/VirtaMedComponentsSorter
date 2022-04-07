using System.Collections.Generic;
using Assets.Scripts;
using Moq;
using NUnit.Framework;

namespace Tests.EditMode
{
    public class ComponentsSorterTests
    {
        private ComponentsSorter _componentsSorter;
        
        private const string _cSeparatorClassType = "Tests.EditMode.VirtamedSeparatorComponent";
        
        [Test, TestCaseSource(nameof(AllVanillaUnityComponentsUpCaseSource))]
        public void AllVanillaUnityComponentsUp_ShouldTriggerCorrectMovementEvents(
            List<IComponentWithIndex> components,
            Mock<IComponentsCategorizer> componentsCategorizerMock,
            List<ComponentMovementArgs> expectedMovementEventArgs)
        {
            // Arrange
            _componentsSorter = new ComponentsSorter(componentsCategorizerMock.Object);
            var componentMovementArgsList = new List<ComponentMovementArgs>();
            
            // Act
            _componentsSorter.MoveComponentEvent += delegate(object sender, ComponentMovementArgs args)
            {
                componentMovementArgsList.Add(args);
            };
            _componentsSorter.AllVanillaUnityComponentsUp(components);

            // Assert
            componentsCategorizerMock
                .Verify(mock => mock.Sort(components, ""), Times.Once);
            Assert.True(Helper.IsArgsListEqual(expectedMovementEventArgs, componentMovementArgsList));
        }

        private static IEnumerable<TestCaseData> AllVanillaUnityComponentsUpCaseSource()
        {
            var componentsCategorizerA = new Mock<IComponentsCategorizer>();
            componentsCategorizerA
                .SetupGet(mock => mock.UnityComponents)
                .Returns(new List<IComponentWithIndex>
            {
                Helper.UnityComponentSubstitute(1),
                Helper.UnityComponentSubstitute(3),
                Helper.UnityComponentSubstitute(4)
            });
            componentsCategorizerA
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(2);
            
            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.SeparatorComponentSubstitute(2, _cSeparatorClassType),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.VirtamedComponentSubstitute(5)
                },
                componentsCategorizerA,
                new List<ComponentMovementArgs>
                {
                    new ComponentMovementArgs(Helper.UnityComponentSubstitute(3), 1),
                    new ComponentMovementArgs(Helper.UnityComponentSubstitute(4), 2),
                }
            );
            
            var componentsCategorizerB = new Mock<IComponentsCategorizer>();
            componentsCategorizerB
                .SetupGet(mock => mock.UnityComponents)
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                });
            componentsCategorizerB
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(6);
            
            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.VirtamedComponentSubstitute(5),
                    Helper.SeparatorComponentSubstitute(6, _cSeparatorClassType),
                },
                componentsCategorizerB,
                new List<ComponentMovementArgs>()
            );
            
            var componentsCategorizerC = new Mock<IComponentsCategorizer>();
            componentsCategorizerC
                .SetupGet(mock => mock.UnityComponents)
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4)
                });
            componentsCategorizerC
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(0);
            
            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.SeparatorComponentSubstitute(0, _cSeparatorClassType),
                    Helper.UnityComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.VirtamedComponentSubstitute(5),
                },
                componentsCategorizerC,
                new List<ComponentMovementArgs>
                {
                    new ComponentMovementArgs(Helper.UnityComponentSubstitute(1), 1),
                    new ComponentMovementArgs(Helper.UnityComponentSubstitute(3), 3),
                    new ComponentMovementArgs(Helper.UnityComponentSubstitute(4), 4),
                }
            );
        }
    }
}
