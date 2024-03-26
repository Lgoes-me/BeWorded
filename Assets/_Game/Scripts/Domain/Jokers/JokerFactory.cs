using System;
using System.Collections.Generic;

public class JokerFactory
{
    public BaseJoker CreateJokerFromIdentifier(string id)
    {
        return id switch
        {
            "BaseA" => new ExtraPrizeCreditedPerCharacterJoker(id, new List<char> {'A'}, 10),
            "BaseVogal" => new ExtraPrizeCreditedPerCharacterJoker(id, new List<char> {'A', 'E', 'I', 'O', 'U'}, 5),
            "MultM" => new ExtraMultiplierPerCharacterCreditedJoker(id, new List<char> {'M'}, 4),
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }
}