using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        //IEnumerable allows to use Enumerators in foreach loops
        IEnumerable<float> GetAdditiveModifier(Stat stat);
    }
}