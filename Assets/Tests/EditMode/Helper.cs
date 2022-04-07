using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Components;
using Moq;
using UnityEngine;

namespace Tests.EditMode
{
    public class VirtamedSeparatorComponent : MonoBehaviour
    {
        public string Name { get; set; }
    }
    
    public class DummyComponent : MonoBehaviour
    {
        public string Name { get; set; }
    }
    
    public static class Helper
    {
        public static IComponentWithIndex VirtamedComponentSubstitute(int position)
        {
            var dummyGameObject = new GameObject("Foo");
            var component = new Mock<IComponentWithIndex>();
            component
                .SetupGet(mock => mock.Component)
                .Returns(dummyGameObject.AddComponent<VirtamedComponent>());
            component
                .SetupGet(mock => mock.Position)
                .Returns(position);
            component
                .SetupGet(mock => mock.TypeString)
                .Returns(dummyGameObject.GetComponent<VirtamedComponent>().GetType().ToString());
            return component.Object;
        }
        
        public static IComponentWithIndex BarVirtaComponent(int position)
        {
            var dummyGameObject = new GameObject("Foo");
            var component = new Mock<IComponentWithIndex>();
            component
                .SetupGet(mock => mock.Component)
                .Returns(dummyGameObject.AddComponent<BarVirtaComponent>());
            component
                .SetupGet(mock => mock.Position)
                .Returns(position);
            component
                .SetupGet(mock => mock.TypeString)
                .Returns(dummyGameObject.GetComponent<BarVirtaComponent>().GetType().ToString());
            return component.Object;
        }
        
        public static IComponentWithIndex BazVirtaComponent(int position)
        {
            var dummyGameObject = new GameObject("Foo");
            var component = new Mock<IComponentWithIndex>();
            component
                .SetupGet(mock => mock.Component)
                .Returns(dummyGameObject.AddComponent<BazVirtaComponent>());
            component
                .SetupGet(mock => mock.Position)
                .Returns(position);
            component
                .SetupGet(mock => mock.TypeString)
                .Returns(dummyGameObject.GetComponent<BazVirtaComponent>().GetType().ToString());
            return component.Object;
        }
        
        public static IComponentWithIndex FooVirtaComponent(int position)
        {
            var dummyGameObject = new GameObject("Foo");
            var component = new Mock<IComponentWithIndex>();
            component
                .SetupGet(mock => mock.Component)
                .Returns(dummyGameObject.AddComponent<FooVirtaComponent>());
            component
                .SetupGet(mock => mock.Position)
                .Returns(position);
            component
                .SetupGet(mock => mock.TypeString)
                .Returns(dummyGameObject.GetComponent<FooVirtaComponent>().GetType().ToString());
            return component.Object;
        }
        
        public static IComponentWithIndex SeparatorComponentSubstitute(int position, string separatorClassType)
        {
            var dummyGameObject = new GameObject("Foo");
            var component = new Mock<IComponentWithIndex>();
            component
                .SetupGet(mock => mock.Component)
                .Returns(dummyGameObject.AddComponent<VirtamedSeparatorComponent>());
            component
                .SetupGet(mock => mock.Position)
                .Returns(position);
            component
                .SetupGet(mock => mock.TypeString)
                .Returns(separatorClassType);
            return component.Object;
        }
        
        public static IComponentWithIndex UnityComponentSubstitute(int position)
        {
            var dummyGameObject = new GameObject("Foo");
            var component = new Mock<IComponentWithIndex>();
            component
                .SetupGet(mock => mock.Component)
                .Returns(dummyGameObject.AddComponent<DummyComponent>());
            component
                .SetupGet(mock => mock.Position)
                .Returns(position);
            component
                .SetupGet(mock => mock.TypeString)
                .Returns(dummyGameObject.GetComponent<DummyComponent>().GetType().ToString());
            return component.Object;
        }

        public static bool ComponentWithIndexListEqual(List<IComponentWithIndex> lhs, List<IComponentWithIndex> rhs)
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

        public static bool IsArgsListEqual(List<ComponentMovementArgs> lhs, List<ComponentMovementArgs> rhs)
        {
            if (lhs.Count != rhs.Count)
                return false;
            
            for (int i = 0; i < lhs.Count; i++)
            {
                if (lhs[i].ComponentWithIndex.Position != rhs[i].ComponentWithIndex.Position ||
                    lhs[i].ComponentWithIndex.TypeString != rhs[i].ComponentWithIndex.TypeString ||
                    lhs[i].NumberOfMovements != rhs[i].NumberOfMovements)
                {
                    return false;
                }
            }

            return true;
        }
    }
}