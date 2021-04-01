using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider
    {
        //IEnumerable allows to use Enumerators in foreach loops
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}