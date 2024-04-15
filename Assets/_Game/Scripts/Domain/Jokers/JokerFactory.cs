using System;
using System.Collections.Generic;
using System.Linq;

public class JokerFactory
{
    private Player Player { get; }

    public JokerFactory(Player player)
    {
        Player = player;
    }

    public Joker CreateJoker(JokerIdentifier id)
    {
        return id switch
        {
            JokerIdentifier.BaseA => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'A'}),
                    (ref int points, ref int multiplier) => ExtraPointsModifier(ref points, 10)
                }
            },

            JokerIdentifier.BaseVogal => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'A', 'E', 'I', 'O', 'U'}),
                    (ref int points, ref int multiplier) => ExtraPointsModifier(ref points, 5)
                }
            },

            JokerIdentifier.MultM => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'M'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.MultiplyZ => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'Z'}),
                }
            },

            JokerIdentifier.MoneyS => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'S'}),
                    (ref int points, ref int multiplier) => MoneyModifier(1)
                }
            },

            JokerIdentifier.PowerUpBombB => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'B'}),
                    (ref int points, ref int multiplier) => PowerUpModifier(PowerUpType.Bomba, 1)
                }
            },

            JokerIdentifier.MultSimpleWord => new Joker(id)
            {
                new OnWordScoredListener()
                {
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 10)
                }
            },

            JokerIdentifier.MultiplySimpleWord => new Joker(id)
            {
                new OnWordScoredListener()
                {
                    (ref int points, ref int multiplier) => MultiplyMultiplierModifier(ref multiplier, 2)
                }
            },

            JokerIdentifier.EscalatingJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    (ref int points, ref int multiplier) => AddCounterModifierValue(id),
                    (ref int points, ref int multiplier) =>
                        ExtraMultiplierModifier(ref multiplier, ReadCounterModifierPlayer(id))
                }
            },

            //Balatro
            JokerIdentifier.Joker => new Joker(id)
            {
                new OnWordScoredListener()
                {
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.GreedyJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'A'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.LustyJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'E'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.WrathfulJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'I'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.GluttonousJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'O'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.PridefulJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    letter => IsOneOfCharsValidator(letter, new List<char> {'U'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.JollyJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => MultipleLetterWordValidator(word, 2),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 8)
                }
            },

            JokerIdentifier.ZanyJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => MultipleLetterWordValidator(word, 3),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 12)
                }
            },

            JokerIdentifier.MadJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => MultipleLetterWordValidator(word, 4),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 20)
                }
            },

            JokerIdentifier.SlyJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => MultipleLetterWordValidator(word, 2),
                    (ref int points, ref int multiplier) => ExtraPointsModifier(ref points, 50)
                }
            },

            JokerIdentifier.WillyJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => MultipleLetterWordValidator(word, 3),
                    (ref int points, ref int multiplier) => ExtraPointsModifier(ref points, 100)
                }
            },

            JokerIdentifier.CleverJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => MultipleLetterWordValidator(word, 4),
                    (ref int points, ref int multiplier) => ExtraPointsModifier(ref points, 150)
                }
            },

            JokerIdentifier.HalfJoker => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => word.Length <= 3,
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 20)
                }
            },

            JokerIdentifier.JokerStencil => new Joker(id)
            {
                new OnWordScoredListener
                {
                    (ref int points, ref int multiplier) =>
                        MultiplyMultiplierModifier(ref multiplier, 1 + Player.QuantidadeJokers - Player.Jokers.Count)
                }
            },

            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
    }

    private bool IsOneOfCharsValidator(Letter letter, List<char> characters)
    {
        return characters.Contains(letter.Character);
    }

    private bool IsPrizeLetterValidator(Letter letter, IPrize prize)
    {
        return prize == letter.Prize;
    }

    private bool MultipleLetterWordValidator(string word, int count)
    {
        return word.GroupBy(x => x).Select(y => y).Count(z => z.Count() >= count) > 0;
    }

    private bool EndsWithLetterWordValidator(string word, char character)
    {
        return word.LastOrDefault() == character;
    }

    private bool StartsWithLetterWordValidator(string word, char character)
    {
        return word.FirstOrDefault() == character;
    }

    private bool WordWithPatternValidator(string word, string subString)
    {
        return word.Contains(subString);
    }

    private void ExtraPointsModifier(ref int points, int extraPoints)
    {
        points += extraPoints;
    }

    private void ExtraMultiplierModifier(ref int multiplier, int extraMultiplier)
    {
        multiplier += extraMultiplier;
    }

    private void MultiplyMultiplierModifier(ref int multiplier, int extraMultiplier)
    {
        multiplier *= extraMultiplier;
    }

    private void MoneyModifier(int money)
    {
        Player.GivePrize(money);
    }

    private void PowerUpModifier(PowerUpType powerUpType, int quantity)
    {
        switch (powerUpType)
        {
            case PowerUpType.Troca:
                Player.Swaps.Gain(quantity);
                break;
            case PowerUpType.Bomba:
                Player.Bombs.Gain(quantity);
                break;
            case PowerUpType.Misturar:
                Player.Shuffles.Gain(quantity);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void AddCounterModifierValue(JokerIdentifier identifier)
    {
        if (Player.JokersExtraParams.TryGetValue(identifier, out var value))
        {
            Player.JokersExtraParams[identifier] = value + 1;
        }
        else
        {
            Player.JokersExtraParams.Add(identifier, 1);
        }
    }

    private int ReadCounterModifierPlayer(JokerIdentifier identifier)
    {
        if (Player.JokersExtraParams.TryGetValue(identifier, out var value))
        {
            return value;
        }

        return 0;
    }
}

public enum JokerIdentifier
{
    BaseA,
    BaseVogal,
    MultM,
    MultiplyZ,
    MoneyS,
    PowerUpBombB,
    MultSimpleWord,
    MultiplySimpleWord,
    EscalatingJoker,

    //Balatro mechanics test
    Joker,
    GreedyJoker, // A
    LustyJoker, // E
    WrathfulJoker, // I
    GluttonousJoker, // O
    PridefulJoker, // U *NOVO
    JollyJoker, // 2 Letters
    ZanyJoker, // 3 Letters
    MadJoker, // 4 Letters
    SlyJoker, // 2 Letters
    WillyJoker, // 3 Letters
    CleverJoker, // 4 Letters
    HalfJoker, // Palavras <3 lenght
    JokerStencil,
    Mime,
    CreditCard,
    CeremonialDagger,
    Banner,
    MysticSummit,
    MarbleJoker,
    LoyaltyCard,
    EightBall,
    Misprint,
    Dusk,
    ChaosTheClown,
    Fibonacci,
    SteelJoker,
    ScaryFace,
    AbstractJoker,
    DelayedGratification,
    Hack,
    Pareidolia,
    GrosMichel,
    EvenSteven,
    OddTodd,
    Scholar,
    BusinessCard,
    SuperNova,
    RideTheBus,
    SpaceJoker,
    Egg,
    Burglar,
    BlackBoard,
    Runner,
    IceCream,
    DNA,
    Splash,
    BlueJoker,
    SixthSense,
    Constellation,
    Hiker,
    FacelessJoker,
    GreenJoker,
    SuperPosition,
    ToDoList,
    Cavendish,
    CardSharp,
    RedCard,
    Madness,
    SquareJoker,
    Seance,
    RiffRaff,
    Vampire,
    Shortcut,
    Hologram,
    Vagabond,
    Baron,
    CloudNine,
    Rocket,
    Obelisk,
    MidasMask,
    Luchador,
    Photograph,
    GiftCard,
    TurtleBean,
    Erosion,
    ReservedParking,
    MailInRebate,
    ToTheMoon,
    Hallucination,
    FortuneTeller,
    Juggler,
    Drunkard,
    StoneJoker,
    GoldenJoker,
    LuckyCat,
    BaseballCard,
    Bull,
    DietCola,
    FlashCard,
    Popcorn,
    SpareTrousers,
    AncientJoker,
    WalkieTalkie,
    Seltzer,
    Castle,
    SmileyFace,
    CampFire,
    GoldenTicket,
    MrBones,
    Acrobat,
    SockAndBuskin,
    Swashbuckler,
    Troubadour,
    Certificate,
    SmearedJoker,
    Throwback,
    HangingChad,
    RoughGem,
    BloodStone,
    ArrowHead,
    OnyxAgate,
    GlassJoker,
    ShowMan,
    FlowerPot,
    BluePrint,
    WeeJoker,
    MerryAndy,
    OopsAllSix,
    TheIdol,
    SeeingDouble,
    Matador,
    HitTheRoad,
    TheDuo,
    TheTrio,
    TheFamily,
    TheTribe,
    Stuntman,
    InvisibleJoker,
    Brainstorm,
    Satellite,
    ShootTheMoon,
    DriversLicense,
    Astronomer,
    BurntJoker,
    Bootstraps,
    Canio,
    Triboulet,
    Yorick,
    Chicot,
    Perkeo,
}