using Assets.Scripts;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Tests
{
    internal class Foo
    {
        public int Count => _gameObjects.Count;

        private readonly List<GameObject> _gameObjects;

        public Foo(List<GameObject> gameObjects)
        {
            _gameObjects = gameObjects;
        }
    }

    public class ComponentsSorter_Tests
    {
        [Test]
        public void Init_ShouldDisplayCorrectTestCount()
        {
            // Arrange
            var gameObjectList = new List<GameObject>
            {
                new GameObject("a"),
                new GameObject("b"),
                new GameObject("c"),
                new GameObject("d"),
                new GameObject("e")
            };

            // Act
            var foo = new Foo(gameObjectList);

            // Assert
            Assert.AreEqual(foo.Count, 5);
        }

        //[Test]
        //public void Init_ShouldDisplayCorrectTestName()
        //{

        //    // Arrange
        //    var gameObjectList = new List<GameObject>
        //    {
        //        new GameObject("a"),
        //        new GameObject("b"),
        //        new GameObject("c"),
        //        new GameObject("d"),
        //        new GameObject("e")
        //    };
        //    IBarable barable = Substitute.For<IBarable>();
        //    barable.Name.Returns("Bar");

        //    // Act
        //    var foo = new Foo(gameObjectList, barable);

        //    // Assert
        //    Assert.AreEqual(foo.Count, 5);
        //    Assert.AreEqual(foo.Name, "Bar");
        //}
    }
}
