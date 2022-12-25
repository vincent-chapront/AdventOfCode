using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day22 : GenericDay
    {
        public enum Spell
        {
            MagicMissile,
            Drain,
            Shield,
            Poison,
            Recharge,
            None
        }

        public string Compute1(params string[] input)
        {
            throw new NotImplementedException();
        }

        private enum Modes
        {
            Start,
            NewTurn,
            Retry,
            Rollback,
        }

        private static int ManaPerSpell(Spell spell)
        {
            switch (spell)
            {
                case Spell.MagicMissile:
                    return 53;

                case Spell.Drain:
                    return 73;

                case Spell.Shield:
                    return 113;

                case Spell.Poison:
                    return 173;

                case Spell.Recharge:
                    return 229;

                default:
                    return 0;
            }
        }

        private static long Run(int playerHp, int playerMp, int bossHp, int bossDamage)
        {
            var turns = new Stack<(Turn startState, Spell spell, Turn endState)>();
            turns.Push(
                (null, Spell.None, new Turn()
                {
                    Number = 0,
                    PlayerHp = playerHp,
                    PlayerMp = playerMp,
                    BossHp = bossHp,
                    BossDamage = bossDamage,
                    UsedMp = 0
                }
                )
            );

            var currentMode = Modes.NewTurn;
            while (true)
            {
                Turn startState = null;
                Spell spell = default;
                switch (currentMode)
                {
                    case Modes.NewTurn:
                        startState = turns.Peek().endState.Clone();
                        startState.Number++;
                        spell = Spell.MagicMissile;
                        break;

                    case Modes.Retry:
                        var v = turns.Pop();
                        startState = v.startState;
                        spell = v.spell + 1;
                        if (spell >= Spell.None)
                        {
                            Debug.WriteLine("Rollback");
                            continue;
                        }

                        break;

                    case Modes.Rollback:
                        break;
                }
                Debug.Write(startState.Number + " : " + currentMode);

                Debug.Write($"- Player :({startState.PlayerHp}/{startState.PlayerMp}) - Boss : ({startState.BossHp})");
                Debug.Write($" - Spell : {spell} ");
                Debug.Write($"({ManaPerSpell(spell)})");
                if (startState.PlayerMp < ManaPerSpell(spell))
                {
                    currentMode = Modes.Retry;
                    Debug.WriteLine(" - LOW MANA - Retry");
                    continue;
                }

                currentMode = Modes.NewTurn;
                var endState = ApplyTurn(startState, spell);

                Debug.Write($"- Player :({endState.PlayerHp}/{endState.PlayerMp}) - Boss : ({endState.BossHp})");
                if (endState.BossHp <= 0)
                {
                    Debug.WriteLine(" - VICTORY");
                    return endState.UsedMp;
                }

                if (endState.PlayerHp <= 0)
                {
                    Debug.Write(" - GAMEOVER Retry");
                    currentMode = Modes.Retry;
                }

                turns.Push((startState, spell, endState));
                Debug.WriteLine("");
            }

            return 0;

            // var isRollback = false;
            // var isRetry = false;

            // while (true)
            // {
            //     Turn currentTurn;
            //     var spell = Spell.MagicMissile;
            //     bool modeRollback=false;
            //     var modeRetry=false;
            //     if (isRetry)
            //     {
            //         modeRetry=true;
            //         isRetry = false;
            //         var lastTurn = turns.Pop();

            //         currentTurn = lastTurn.startState.Clone();
            //         Debug.Write(currentTurn.Number + " : Retry");
            //         spell = (Spell)(lastTurn.spell + 1);

            //     }
            //     else if (isRollback)
            //     {
            //          modeRollback=true;
            //         isRollback = false;
            //         var lastTurn = turns.Pop();

            //         currentTurn = lastTurn.startState.Clone();
            //         Debug.Write(currentTurn.Number+" : Rollback");
            //         spell = (Spell)(lastTurn.spell + 1);

            //     }
            //     else
            //     {
            //         currentTurn = turns.Peek().startState.Clone();
            //         Debug.Write(currentTurn.Number + " : NewTurn");
            //         currentTurn.Number++;
            //     }

            //     if(
            //         (spell== Spell.MagicMissile && currentTurn.PlayerMp<53)
            //         || (spell== Spell.Drain && currentTurn.PlayerMp<73)
            //         || (spell== Spell.Shield && currentTurn.PlayerMp<113)
            //         || (spell== Spell.Poison && currentTurn.PlayerMp<173)
            //         || (spell== Spell.Recharge && currentTurn.PlayerMp<229)
            //         )
            //     {
            //         isRetry = true;
            //         Debug.WriteLine("");
            //         continue;
            //     }
            //     if(spell== Spell.None)
            //     {
            //         isRollback = true;
            //         Debug.WriteLine("");
            //         continue;
            //     }
            //     Debug.Write($"- Player :({currentTurn.PlayerHp}/{currentTurn.PlayerMp}) - Boss : ({currentTurn.BossHp})");
            //     Debug.Write($" - Spell : {spell} ");
            //     var damageReduction = currentTurn.Effects.Where(effect => effect.V == Spell.Shield).Sum(x => x.Armor);
            //     currentTurn.PlayerMp += currentTurn.Effects.Where(effect => effect.V == Spell.Recharge).Sum(x => x.Mana);
            //     currentTurn.BossHp -= currentTurn.Effects.Where(effect => effect.V == Spell.Poison).Sum(x => x.Damage);
            //     currentTurn.Effects = currentTurn.Effects.Select(x => { var y = x.Clone(); y.Duration--; return y; }).Where(x => x.Duration > 0).ToList();

            //     switch (spell)
            //     {
            //         case Spell.MagicMissile:
            //             currentTurn.BossHp -= 4;
            //             currentTurn.PlayerMp -= 53;
            //             currentTurn.UsedMp += 53;
            //             break;
            //         case Spell.Drain:
            //             currentTurn.BossHp -= 2;
            //             currentTurn.PlayerHp += 2;
            //             currentTurn.PlayerMp -= 73;
            //             currentTurn.UsedMp += 73;
            //             break;
            //         case Spell.Shield:
            //             currentTurn.PlayerMp -= 113;
            //             currentTurn.Effects.Add(new Effect(spell, 7, 0, 0, 6));
            //             currentTurn.UsedMp += 113;
            //             break;
            //         case Spell.Poison:
            //             currentTurn.PlayerMp -= 173;
            //             currentTurn.Effects.Add(new Effect(spell, 0, 0, 3, 6));
            //             currentTurn.UsedMp += 173;
            //             break;
            //         case Spell.Recharge:
            //             currentTurn.PlayerMp -= 229;
            //             currentTurn.Effects.Add(new Effect(spell, 0, 101, 0, 5));
            //             currentTurn.UsedMp += 229;
            //             break;
            //         case Spell.None:
            //             break;
            //         default:
            //             break;
            //     }
            //     currentTurn.PlayerHp -= (currentTurn.BossDamage- damageReduction);
            //     Debug.Write($"- Player :({currentTurn.PlayerHp}/{currentTurn.PlayerMp}) - Boss : ({currentTurn.BossHp})");
            //     Debug.WriteLine("");
            //     if (currentTurn.BossHp < 0)
            //     {
            //         return currentTurn.UsedMp;
            //     }
            //     if (currentTurn.PlayerHp < 0)
            //     {
            //         if(modeRetry)
            //         {
            //             turns.Push((currentTurn,spell, null));
            //         }
            //         isRetry = true;
            //         continue;
            //     }
            //     turns.Push((currentTurn,spell, null));
            // }

            // return 0;
        }

        private static Turn ApplyTurn(Turn startState, Spell spell)
        {
            var endState = startState.Clone();
            var damageReduction = endState.Effects.Where(effect => effect.V == Spell.Shield).Sum(x => x.Armor);
            endState.PlayerMp += endState.Effects.Where(effect => effect.V == Spell.Recharge).Sum(x => x.Mana);
            endState.BossHp -= endState.Effects.Where(effect => effect.V == Spell.Poison).Sum(x => x.Damage);
            endState.Effects = endState.Effects.Select(x => { var y = x.Clone(); y.Duration--; return y; }).Where(x => x.Duration > 0).ToList();

            switch (spell)
            {
                case Spell.MagicMissile:
                    endState.BossHp -= 4;
                    endState.PlayerMp -= 53;
                    endState.UsedMp += 53;
                    break;

                case Spell.Drain:
                    endState.BossHp -= 2;
                    endState.PlayerHp += 2;
                    endState.PlayerMp -= 73;
                    endState.UsedMp += 73;
                    break;

                case Spell.Shield:
                    endState.PlayerMp -= 113;
                    endState.Effects.Add(new Effect(spell, 7, 0, 0, 6));
                    endState.UsedMp += 113;
                    break;

                case Spell.Poison:
                    endState.PlayerMp -= 173;
                    endState.Effects.Add(new Effect(spell, 0, 0, 3, 6));
                    endState.UsedMp += 173;
                    break;

                case Spell.Recharge:
                    endState.PlayerMp -= 229;
                    endState.Effects.Add(new Effect(spell, 0, 101, 0, 5));
                    endState.UsedMp += 229;
                    break;

                case Spell.None:
                    break;

                default:
                    break;
            }
            endState.PlayerHp -= (endState.BossDamage - damageReduction);
            return endState;
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }

        public class Effect
        {
            public Effect(Spell v, int armor, int mana, int damage, int duration)
            {
                V = v;
                Armor = armor;
                Mana = mana;
                Damage = damage;
                Duration = duration;
            }

            public int Armor { get; }
            public int Damage { get; }
            public int Duration { get; set; }
            public int Mana { get; }
            public Spell V { get; }

            internal Effect Clone()
            {
                return new Effect(V, Armor, Mana, Damage, Duration);
            }
        }

        public class Turn
        {
            public int BossDamage { get; set; }
            public int BossHp { get; set; }
            public List<Effect> Effects { get; set; } = new List<Effect>();
            public int PlayerHp { get; set; }
            public int PlayerMp { get; set; }
            public int UsedMp { get; internal set; }
            public int Number { get; internal set; }

            internal Turn Clone()
            {
                return new Turn()
                {
                    PlayerHp = this.PlayerHp,
                    PlayerMp = this.PlayerMp,
                    BossHp = this.BossHp,
                    BossDamage = this.BossDamage,
                    Effects = this.Effects.Select(x => x.Clone()).ToList(),
                    UsedMp = UsedMp,
                    Number = this.Number,
                };
            }
        }
    }
}