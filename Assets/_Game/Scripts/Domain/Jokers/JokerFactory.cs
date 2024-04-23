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
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'A'}),
                    (ref int points, ref int _) => ExtraPointsModifier(ref points, 10)
                }
            },

            JokerIdentifier.BaseVogal => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'A', 'E', 'I', 'O', 'U'}),
                    (ref int points, ref int _) => ExtraPointsModifier(ref points, 5)
                }
            },

            JokerIdentifier.MultM => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'M'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.MultiplyZ => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'Z'}),
                }
            },

            JokerIdentifier.MoneyS => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'S'}),
                    (ref int points, ref int multiplier) => MoneyModifier(1)
                }
            },

            JokerIdentifier.PowerUpBombB => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'B'}),
                    (ref int points, ref int multiplier) => PowerUpModifier(Player.Bombs, 1)
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
                        ExtraMultiplierModifier(ref multiplier, ReadCounterModifierFromPlayer(id))
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
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'A'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.LustyJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'E'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.WrathfulJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'I'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.GluttonousJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'O'}),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.PridefulJoker => new Joker(id)
            {
                new OnLetterScoredListener
                {
                    (_, letter) => IsOneOfCharsValidator(letter, new List<char> {'U'}),
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

            JokerIdentifier.CeremonialDagger => new Joker(id)
            {
                new OnMatchStartedListener()
                {
                    () =>
                    {
                        var value = DeleteNextJoker(id);
                        AddCounterModifierValue(id, 2 * value);
                    }
                },
                new OnWordScoredListener
                {
                    (ref int points, ref int multiplier) =>
                        ExtraMultiplierModifier(ref multiplier, ReadCounterModifierFromPlayer(id))
                }
            },

            JokerIdentifier.Banner => new Joker(id)
            {
                new OnWordScoredListener
                {
                    (ref int points, ref int multiplier) =>
                        ExtraPointsModifier(ref points,
                            40 * (Player.Bombs.Uses + Player.Swaps.Uses + Player.Shuffles.Uses))
                }
            },

            JokerIdentifier.MysticSummit => new Joker(id)
            {
                new OnWordScoredListener
                {
                    word => HasNoPowerUpsValidator(),
                    (ref int points, ref int multiplier) => ExtraMultiplierModifier(ref multiplier, 15)
                }
            },

            JokerIdentifier.LoyaltyCard => new Joker(id)
            {
                new OnWordScoredListener
                {
                    (ref int points, ref int multiplier) => AddCounterModifierValue(id)
                },

                new OnWordScoredListener
                {
                    word => ReadCounterModifierFromPlayer(id) % 6 == 0,
                    (ref int points, ref int multiplier) => MultiplyMultiplierModifier(ref multiplier, 4)
                }
            },

            JokerIdentifier.Misprint => new Joker(id)
            {
                new OnWordScoredListener
                {
                    (ref int points, ref int multiplier) =>
                        ExtraMultiplierModifier(ref multiplier, UnityEngine.Random.Range(0, 24))
                }
            },

            JokerIdentifier.Dusk => new Joker(id)
            {
                new OnLetterScoredListener()
                {
                    (level, _) => level.Tentativas == 0,
                    //(ref int points, ref int multiplier) =>
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

    private bool HasNoPowerUpsValidator()
    {
        var hasShuffles = Player.Shuffles.Uses > 0;
        var hasBombs = Player.Bombs.Uses > 0;
        var hasSwaps = Player.Swaps.Uses > 0;
        return !hasShuffles && !hasBombs && !hasSwaps;
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

    private void PowerUpModifier(PowerUp powerUp, int quantity)
    {
        powerUp.Gain(quantity);
    }

    private void AddCounterModifierValue(JokerIdentifier identifier, int counter = 1)
    {
        if (Player.JokersExtraParams.TryGetValue(identifier, out var value))
        {
            Player.JokersExtraParams[identifier] = value + counter;
        }
        else
        {
            Player.JokersExtraParams.Add(identifier, counter);
        }
    }

    private int ReadCounterModifierFromPlayer(JokerIdentifier identifier)
    {
        if (Player.JokersExtraParams.TryGetValue(identifier, out var value))
        {
            return value;
        }

        return 0;
    }

    private int DeleteNextJoker(JokerIdentifier identifier)
    {
        var dagger = Player.Jokers.FirstOrDefault(j => j.Identifier == identifier);

        if (dagger == null)
            return 0;

        var index = Player.Jokers.IndexOf(dagger);

        if (Player.Jokers.Count <= index + 1)
            return 0;

        var nextJoker = Player.Jokers[index + 1];

        Player.Jokers.Remove(nextJoker);

        return nextJoker.SellValue;
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
    Mime, // ???
    CreditCard, // ???
    CeremonialDagger,
    Banner, // discards = powerups
    MysticSummit, // discards = powerups
    MarbleJoker, // ???
    LoyaltyCard,
    EightBall, // ??? what is a planet
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