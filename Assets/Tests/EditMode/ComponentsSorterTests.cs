using System.Collections.Generic;
using System.Linq;
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
            
            var componentsCategorizerD = new Mock<IComponentsCategorizer>();
            componentsCategorizerD
                .SetupGet(mock => mock.UnityComponents)
                .Returns(new List<IComponentWithIndex>());
            componentsCategorizerD
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(0);
            
            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.SeparatorComponentSubstitute(0, _cSeparatorClassType),
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(2),
                },
                componentsCategorizerD,
                new List<ComponentMovementArgs>()
            );
        }
        
        [Test, TestCaseSource(nameof(AllVirtamedComponentsDownCaseSource))]
        public void AllVirtamedComponentsDown_ShouldTriggerCorrectMovementEvents(
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
            _componentsSorter.AllVirtamedComponentsDown(components);
        
            // Assert
            componentsCategorizerMock
                .Verify(mock => mock.Sort(components, ""), Times.Once);
            Assert.True(Helper.IsArgsListEqual(expectedMovementEventArgs, componentMovementArgsList));
        }
        
        private static IEnumerable<TestCaseData> AllVirtamedComponentsDownCaseSource()
        {
            var componentsCategorizerA = new Mock<IComponentsCategorizer>();
            componentsCategorizerA
                .SetupGet(mock => mock.VirtaComponents)
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.VirtamedComponentSubstitute(0),
                    Helper.VirtamedComponentSubstitute(5)
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
                    new ComponentMovementArgs(Helper.VirtamedComponentSubstitute(0), -2)
                }
            );
            
            var componentsCategorizerB = new Mock<IComponentsCategorizer>();
            componentsCategorizerB
                .SetupGet(mock => mock.VirtaComponents)
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(5)
                });
            componentsCategorizerB
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(0);

            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.SeparatorComponentSubstitute(0, _cSeparatorClassType),
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.VirtamedComponentSubstitute(5)
                },
                componentsCategorizerB,
                new List<ComponentMovementArgs>()
            );
            
            var componentsCategorizerC = new Mock<IComponentsCategorizer>();
            componentsCategorizerC
                .SetupGet(mock => mock.VirtaComponents)
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.VirtamedComponentSubstitute(5)
                });
            componentsCategorizerC
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(6);

            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.UnityComponentSubstitute(0),
                    Helper.VirtamedComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.VirtamedComponentSubstitute(5),
                    Helper.SeparatorComponentSubstitute(6, _cSeparatorClassType),
                },
                componentsCategorizerC,
                new List<ComponentMovementArgs>
                {
                    new ComponentMovementArgs(Helper.VirtamedComponentSubstitute(1), -5),
                    new ComponentMovementArgs(Helper.VirtamedComponentSubstitute(5), -1)
                }
            );
            
            var componentsCategorizerD = new Mock<IComponentsCategorizer>();
            componentsCategorizerD
                .SetupGet(mock => mock.VirtaComponents)
                .Returns(new List<IComponentWithIndex>());
            componentsCategorizerD
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(6);

            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.UnityComponentSubstitute(0),
                    Helper.UnityComponentSubstitute(1),
                    Helper.UnityComponentSubstitute(2),
                    Helper.UnityComponentSubstitute(3),
                    Helper.UnityComponentSubstitute(4),
                    Helper.UnityComponentSubstitute(5),
                    Helper.SeparatorComponentSubstitute(6, _cSeparatorClassType),
                },
                componentsCategorizerD,
                new List<ComponentMovementArgs>()
            );
        }
        
        [Test, TestCaseSource(nameof(SortVirtamedComponentsCaseSource))]
        public void SortVirtamedComponents_ShouldTriggerCorrectMovementEvents(
            List<IComponentWithIndex> components,
            Mock<IComponentsCategorizer> componentsCategorizerMock,
            List<string> sortOrder,
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
            foreach (var toSort in Enumerable.Reverse(sortOrder))
                _componentsSorter.SortVirtamedComponents(components, toSort);

            // Assert
            componentsCategorizerMock.Verify(
                mock => mock.Sort(components, It.IsAny<string>()), 
                Times.Exactly(sortOrder.Count));
            Assert.True(Helper.IsArgsListEqual(expectedMovementEventArgs, componentMovementArgsList));
        }

        private static IEnumerable<TestCaseData> SortVirtamedComponentsCaseSource()
        {
            var componentsCategorizerA = new Mock<IComponentsCategorizer>();
            componentsCategorizerA
                .SetupSequence(mock => mock.FoundComponents)
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.FooVirtaComponent(1),
                    Helper.FooVirtaComponent(3),
                })
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.BazVirtaComponent(3),
                })
                .Returns(new List<IComponentWithIndex>
                {
                    Helper.BarVirtaComponent(4),
                    Helper.BarVirtaComponent(5)
                });
            componentsCategorizerA
                .SetupGet(mock => mock.SeparatorPosition)
                .Returns(0);

            yield return new TestCaseData(
                new List<IComponentWithIndex>
                {
                    Helper.SeparatorComponentSubstitute(0, _cSeparatorClassType),
                    Helper.FooVirtaComponent(1),
                    Helper.BazVirtaComponent(2),
                    Helper.FooVirtaComponent(3),
                    Helper.BarVirtaComponent(4),
                    Helper.BarVirtaComponent(5),
                },
                componentsCategorizerA,
                new List<string>
                {
                    "BarVirtaComponent",
                    "BazVirtaComponent",
                    "FooVirtaComponent"
                },
                new List<ComponentMovementArgs>
                {
                    new ComponentMovementArgs(Helper.FooVirtaComponent(1), 0),
                    new ComponentMovementArgs(Helper.FooVirtaComponent(3), 2),
                    new ComponentMovementArgs(Helper.BazVirtaComponent(3), 2),
                    new ComponentMovementArgs(Helper.BarVirtaComponent(4), 3),
                    new ComponentMovementArgs(Helper.BarVirtaComponent(5), 4)
                }
            );
        }
    }
}
