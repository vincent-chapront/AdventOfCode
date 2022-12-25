using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2020
{
    internal class Day19 : GenericDay
    {
        public string Compute1(params string[] input)
        {
            var rawRules = input.TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            var rules = ParseRules(rawRules);
            var messages = input.SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Skip(1).ToArray();
            var regex = RulesToRegex(rules);

            var r = new Regex("^" + regex + "$");
            var result = 0;
            foreach (var message in messages)
            {
                if (r.IsMatch(message))
                {
                    result++;
                }
            }
            return result.ToString();
        }

        public string Compute2(params string[] input)
        {
            throw new NotImplementedException();
        }

        private static List<Rule> ParseRules(string[] input)
        {
            List<Rule> rules = new List<Rule>();

            foreach (var line in input)
            {
                var separator = line.IndexOf(':');
                int number = int.Parse(line.Substring(0, separator));
                string match = line.Substring(separator + 2);
                if (match.Contains("\""))
                {
                    char character = match[1];
                    var existingRule = rules.FirstOrDefault(x => x.Number == number);
                    if (existingRule == null)
                    {
                        existingRule = new Rule(number);
                        rules.Add(existingRule);
                    }

                    existingRule.Definition = new RuleLeaf(character);
                }
                else
                {
                    var existingParentRule = rules.FirstOrDefault(x => x.Number == number);
                    if (existingParentRule == null)
                    {
                        existingParentRule = new Rule(number);
                        rules.Add(existingParentRule);
                    }

                    var definition = new RuleNode();

                    existingParentRule.Definition = definition;
                    var optionalRules = match.Split(" | ").Select(x => x.Trim());
                    foreach (var optionalRule in optionalRules)
                    {
                        var rulesId = optionalRule.Split(' ');
                        var a = new List<Rule>();
                        foreach (var ruleId in rulesId)
                        {
                            var childNumber = int.Parse(ruleId);
                            var existingChildRule = rules.FirstOrDefault(x => x.Number == childNumber);
                            if (existingChildRule == null)
                            {
                                existingChildRule = new Rule(childNumber);
                                rules.Add(existingChildRule);
                            }
                            a.Add(existingChildRule);
                        }
                        definition.Rules.Add(a);
                    }
                }
            }

            return rules;
        }

        private static string RulesToRegex(List<Rule> rules)
        {
            return rules.FirstOrDefault(x => x.Number == 0).ToRegex();
        }

        private class Rule
        {
            public Rule(int number)
            {
                Number = number;
            }

            public RuleDefinition Definition { get; set; }
            public int Number { get; }

            public override string ToString()
            {
                if (Definition == null)
                {
                    return "Empty";
                }
                return (Definition is RuleNode ? "Node: " : "Leaf: ") + Definition.ToString();
            }

            internal string ToRegex()
            {
                return Definition.ToRegex();
            }
        }

        private abstract class RuleDefinition
        {
            public abstract string ToRegex();
        }

        private class RuleLeaf : RuleDefinition
        {
            public RuleLeaf(char character)
            {
                Character = character;
            }

            public char Character { get; set; }

            public override string ToRegex()
            {
                return Character.ToString();
            }

            public override string ToString()
            {
                return Character.ToString();
            }
        }

        private class RuleNode : RuleDefinition
        {
            public List<List<Rule>> Rules { get; } = new List<List<Rule>>();

            public override string ToRegex()
            {
                if (Rules.Count == 0)
                {
                    return string.Empty;
                }
                else if (Rules.Count == 1)
                {
                    return string.Join("", Rules[0].Select(x => x.ToRegex()));
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("(");
                    stringBuilder.Append(string.Join("|", Rules.Select(innerRule => string.Join("", innerRule.Select(x => x.ToRegex())))));
                    stringBuilder.Append(")");
                    return stringBuilder.ToString();
                }
            }

            public override string ToString()
            {
                return string.Join(" | ", Rules.Select(x => string.Join(", ", x.Select(y => y.Number))));
            }
        }
    }
}