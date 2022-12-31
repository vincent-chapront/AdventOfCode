using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day04 : GenericDay
    {
        public string Compute1(string[] input, string args)
        {
            Func<Passport, bool> isValid = p =>
            {
                return p.BirthYear.HasValue
                        && p.IssueYear.HasValue
                        && p.ExpirationYear.HasValue
                        && !string.IsNullOrEmpty(p.Height)
                        && !string.IsNullOrEmpty(p.HairColor)
                        && !string.IsNullOrEmpty(p.EyeColor)
                        && !string.IsNullOrEmpty(p.PassportId);
            };

            return Parse(input, isValid).ToString();
        }

        public string Compute2(string[] input, string args)
        {
            var validEyeColors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            var regexHairColor = new Regex(@"#[0-9a-f]{6}");
            Func<string, bool> isHeightValid = s =>
            {
                if (s.EndsWith("cm"))
                {
                    int h = int.Parse(s.Substring(0, s.Length - 2));
                    var r = h >= 150 && h <= 193;
                    return r;
                }
                else if (s.EndsWith("in"))
                {
                    int h = int.Parse(s.Substring(0, s.Length - 2));
                    var r = h >= 59 && h <= 76;
                    return r;
                }
                return false;
            };
            Func<Passport, bool> isValid = p =>
            {
                return p.BirthYear.HasValue && p.BirthYear >= 1920 && p.BirthYear <= 2002
                        && p.IssueYear.HasValue && p.IssueYear >= 2010 && p.IssueYear <= 2020
                        && p.ExpirationYear.HasValue && p.ExpirationYear >= 2020 && p.ExpirationYear <= 2030
                        && !string.IsNullOrEmpty(p.Height)
                            && isHeightValid(p.Height)
                        && !string.IsNullOrEmpty(p.HairColor) && regexHairColor.IsMatch(p.HairColor)
                        && !string.IsNullOrEmpty(p.EyeColor) && validEyeColors.Contains(p.EyeColor)
                        && !string.IsNullOrEmpty(p.PassportId) && p.PassportId.Length == 9
                        ;
            };

            return Parse(input, isValid).ToString();
        }

        private static int Parse(string[] input, Func<Passport, bool> isValid)
        {
            var p = new Passport();
            var valid = 0;
            foreach (string l in input)
            {
                if (!string.IsNullOrWhiteSpace(l))
                {
                    p.Add(l);
                }
                else
                {
                    if (isValid(p))
                    {
                        valid++;
                    }
                    p = new Passport();
                }
            }
            if (isValid(p))
            {
                valid++;
            }
            return valid;
        }

        internal class Passport
        {
            public int? BirthYear { get; set; }
            public string CountryId { get; set; }
            public int? ExpirationYear { get; set; }
            public string EyeColor { get; set; }
            public string HairColor { get; set; }
            public string Height { get; set; }
            public int? IssueYear { get; set; }
            public string PassportId { get; set; }

            public void Add(string data)
            {
                var pairs = data.Split(" ");
                foreach (var pair in pairs)
                {
                    var v = pair.Split(":");
                    switch (v[0])
                    {
                        case "byr": BirthYear = ParseInt(v[1]); break;
                        case "iyr": IssueYear = ParseInt(v[1]); break;
                        case "eyr": ExpirationYear = ParseInt(v[1]); break;
                        case "hgt": Height = v[1]; break;
                        case "hcl": HairColor = v[1]; break;
                        case "ecl": EyeColor = v[1]; break;
                        case "pid": PassportId = v[1]; break;
                        case "cid": CountryId = v[1]; break;
                        default:
                            break;
                    }
                }
            }

            private static int? ParseInt(string s)
            {
                int r;
                if (int.TryParse(s, out r))
                {
                    return r;
                }
                return null;
            }
        }
    }
}