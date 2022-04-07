using System.Collections.Generic;

namespace Assets.Scripts
{
    public interface IComponentsCategorizer
    {
        List<IComponentWithIndex> VirtaComponents { get; }
        List<IComponentWithIndex> UnityComponents { get; }
        List<IComponentWithIndex> FoundComponents { get; }
        int SeparatorPosition { get; }
        void Sort(List<IComponentWithIndex> componentsWithIndex, string nameFilter = "");
    }
}