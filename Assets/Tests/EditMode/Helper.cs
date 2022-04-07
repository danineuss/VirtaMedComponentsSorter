using System.Collections.Generic;
using Assets.Scripts;
using NSubstitute;
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
        private static readonly GameObject DummyGameObject = new GameObject("Foo");

        public static IComponentWithIndex VirtamedComponentSubstitute(int position)
        {
            var component = Substitute.For<IComponentWithIndex>();
            component.Component.Returns(DummyGameObject.AddComponent<VirtamedComponent>());
            component.Position.Returns(position);
            component.TypeString.Returns("Assets.Scripts.VirtamedComponent");
            return component;
        }
        
        public static IComponentWithIndex SeparatorComponentSubstitute(int position, string separatorClassType)
        {
            var component = Substitute.For<IComponentWithIndex>();
            component.Component.Returns(DummyGameObject.AddComponent<VirtamedSeparatorComponent>());
            component.Position.Returns(position);
            component.TypeString.Returns(separatorClassType);
            return component;
        }
        
        public static IComponentWithIndex UnityComponentSubstitute(int position)
        {
            var component = Substitute.For<IComponentWithIndex>();
            component.Component.Returns(DummyGameObject.AddComponent<DummyComponent>());
            component.Position.Returns(position);
            component.TypeString.Returns("Tests.EditMode.DummyComponent");
            return component;
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
    }
}