using System;
using System.Collections.Generic;

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
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'A'}))
                    .WithModifier(new ExtraPrizeScoreModifier(10))
                    .Build(),
            },

            JokerIdentifier.BaseVogal => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'A', 'E', 'I', 'O', 'U'}))
                    .WithModifier(new ExtraPrizeScoreModifier(5))
                    .Build(),
            },

            JokerIdentifier.MultM => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'M'}))
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build(),
            },

            JokerIdentifier.MultiplyZ => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'Z'}))
                    .WithModifier(new MultiplyMultiplierScoreModifier(2))
                    .Build(),
            },

            JokerIdentifier.MoneyS => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'S'}))
                    .WithModifier(new MoneyPerCharacterScoreModifier(Player, 1))
                    .Build(),
            },

            JokerIdentifier.PowerUpBombB => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'B'}))
                    .WithModifier(new PowerUpPerScoreModifier(Player, PowerUpType.Bomba, 1))
                    .Build(),
            },

            JokerIdentifier.MultSimpleWord => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithModifier(new ExtraMultiplierScoreModifier(10))
                    .Build(),
            },

            JokerIdentifier.MultiplySimpleWord => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithModifier(new MultiplyMultiplierScoreModifier(2))
                    .Build(),
            },

            JokerIdentifier.EscalatingJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithModifier(new ScoreCounterModifier(Player, id))
                    .Build(),
                new OnLetterScoredListenerBuilder()
                    .WithModifier(new ExtraMultiplierFromCounterScoreModifier(Player, id))
                    .Build(),
            },
            
            //Balatro
            JokerIdentifier.Joker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build(),
            },
            
            JokerIdentifier.GreedyJoker => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'A'}))
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build()
            },

            JokerIdentifier.LustyJoker => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'E'}))
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build()
            },
            
            JokerIdentifier.WrathfulJoker => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'I'}))
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build()
            },
            
            JokerIdentifier.GluttonousJoker => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'O'}))
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build()
            },
            
            JokerIdentifier.PridefulJoker => new Joker(id)
            {
                new OnLetterScoredListenerBuilder()
                    .WithValidator(new ContainsCharacterLetterValidator(new List<char> {'U'}))
                    .WithModifier(new ExtraMultiplierScoreModifier(4))
                    .Build()
            },
            
            JokerIdentifier.JollyJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(2))
                    .WithModifier(new ExtraMultiplierScoreModifier(8))
                    .Build()
            },
            
            JokerIdentifier.ZanyJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(3))
                    .WithModifier(new ExtraMultiplierScoreModifier(12))
                    .Build()
            },
            
            JokerIdentifier.MadJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(4))
                    .WithModifier(new ExtraMultiplierScoreModifier(20))
                    .Build()
            },
            
            JokerIdentifier.CrazyJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(5))
                    .WithModifier(new ExtraMultiplierScoreModifier(50))
                    .Build()
            },
            
            JokerIdentifier.SlyJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(2))
                    .WithModifier(new ExtraPrizeScoreModifier(50))
                    .Build()
            },
            
            JokerIdentifier.WillyJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(3))
                    .WithModifier(new ExtraPrizeScoreModifier(100))
                    .Build()
            },
            
            JokerIdentifier.CleverJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(4))
                    .WithModifier(new ExtraPrizeScoreModifier(150))
                    .Build()
            },
            
            JokerIdentifier.DeviousJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new MultipleLetterValidator(5))
                    .WithModifier(new ExtraPrizeScoreModifier(200))
                    .Build()
            },
            
            JokerIdentifier.HalfJoker => new Joker(id)
            {
                new OnWordScoredListenerBuilder()
                    .WithValidator(new WordFuncValidator(word => word.Length <= 3))
                    .WithModifier(new ExtraMultiplierScoreModifier(20))
                    .Build()
            },
            
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
        };
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
    CrazyJoker, // 5 Letters ??? vale a pena ser outra coisa
    DrollJoker, // ??? o que significa shuffle em uma palavra
    SlyJoker, // 2 Letters
    WillyJoker, // 3 Letters
    CleverJoker, // 4 Letters
    DeviousJoker, // 5 Letters ??? vale a pena ser outra coisa
    CraftyJoker, // ??? o que significa shuffle em uma palavra
    HalfJoker, // Palavras <3 lenght
    JokerStencil,
    FourFingers,
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