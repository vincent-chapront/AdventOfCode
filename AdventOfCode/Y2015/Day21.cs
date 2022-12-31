using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2015
{
    internal class Day21 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            var boss = new Character(109, 8, 2);
            var res =
                GenerateKit()
                .Select(x => new Character(100, x))
                .Where(x => x.CanWin(boss))
                .Min(x => x.EquipmentsCost);

            return res.ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var boss = new Character(109, 8, 2);
            var res =
                GenerateKit()
                .Select(x => new Character(100, x))
                .Where(x => !x.CanWin(boss))
                .Max(x => x.EquipmentsCost);

            return res.ToString();
        }

        private static IEnumerable<Equipment[]> GenerateKit()
        {
            var weapons = GetWeapons();
            var armors = GetArmors();
            var rings = GetRings();

            foreach (var weapon in weapons)
            {
                foreach (var armor in armors)
                {
                    for (int i = 0; i < rings.Length - 1; i++)
                    {
                        for (int j = i + 1; j < rings.Length; j++)
                        {
                            yield return new Equipment[]
                            {
                                weapon, armor, rings[i],rings[j]
                            };
                        }
                    }
                }
            }
        }

        private static Equipment[] GetArmors()
        {
            return new Equipment[]
            {
                new Equipment("None",0,0,0),
                new Equipment("Leather",13,0,1),
                new Equipment("Chainmail",31,0,2),
                new Equipment("Splintmail",53,0,3),
                new Equipment("Bandedmail",75,0,4),
                new Equipment("Platemail",102,0,5)
            };
        }

        private static Equipment[] GetRings()
        {
            return new Equipment[]
            {
                new Equipment("None",0,0,0),
                new Equipment("None",0,0,0),
                new Equipment("Damage +1",25,1,0),
                new Equipment("Damage +2",50,2,0),
                new Equipment("Damage +3",100,3,0),
                new Equipment("Defense +1",20,0,1),
                new Equipment("Defense +2",40,0,2),
                new Equipment("Defense +3",80,0,3)
            };
        }

        private static Equipment[] GetWeapons()
        {
            return new Equipment[]
            {
                new Equipment("Dagger",8,4,0),
                new Equipment("Shortsword",10,5,0),
                new Equipment("Warhammer",25,6,0),
                new Equipment("Longsword",40,7,0),
                new Equipment("Greataxe", 74,8,0)
            };
        }

        private class Character
        {
            public Character(int hitpoints, int damage, int armor)
            {
                HitPoints = hitpoints;
                Damage = damage;
                Armor = armor;
            }

            public Character(int hitPoints, Equipment[] equipments)
            {
                HitPoints = hitPoints;
                EquipmentsCost = equipments.Sum(x => x.Cost);
                Damage = equipments.Sum(x => x.Damage);
                Armor = equipments.Sum(x => x.Armor);
                Equipments = equipments;
            }

            public int Armor { get; }
            public int Damage { get; }
            public Equipment[] Equipments { get; }
            public int EquipmentsCost { get; }
            public int HitPoints { get; }

            public bool CanWin(Character ennemy)
            {
                var damageAgainstEnnemy = this.Damage - ennemy.Armor;
                var damageFromEnnemy = ennemy.Damage - this.Armor;

                if (damageAgainstEnnemy <= 0 && damageFromEnnemy <= 0) return false;
                if (damageAgainstEnnemy > 0 && damageFromEnnemy <= 0) return true;
                if (damageFromEnnemy > 0 && damageAgainstEnnemy <= 0) return false;

                var turnToKill = (int)Math.Ceiling(((float)ennemy.HitPoints) / damageAgainstEnnemy);
                var turnToBeKill = (int)Math.Ceiling(((float)this.HitPoints) / damageFromEnnemy);
                return
                    turnToKill <= turnToBeKill;
            }
        }

        private class Equipment
        {
            public Equipment(string name, int cost, int damage, int armor)
            {
                Name = name;
                Cost = cost;
                Damage = damage;
                Armor = armor;
            }

            public int Armor { get; set; }
            public int Cost { get; set; }
            public int Damage { get; set; }
            public string Name { get; set; }
        }
    }
}