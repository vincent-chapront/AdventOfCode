using AdventOfCode.Y2022;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
        public class YearFromJson
        {
            private readonly InputFile_Year year;

            public YearFromJson(InputFile_Year year)
            {
                this.year = year;
            }

            private List<DayFromJson> GetDays(params InputFile_Day[] days)
            {
                var res = new List<DayFromJson>();
                var types = 
                    AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(GenericDay).IsAssignableFrom(p))
                    .Where(p => !p.IsAbstract)
                    .Where(x => x.Namespace.Contains("Y" + year.Year))
                    .ToList();
                foreach (var day in days)
                {
                    var type = types.Where(t => (t.Name == "Day" + day.Day.ToString("00")))
                    .Where(t => t.Name != "DayXX")
                    .Select(t => new DayFromJson(year.Year, t, day))
                    .ToList();
                    ;
                    if (type.Count == 1)
                    {
                        res.Add(type[0]);
                    }
                }
                return res;
            }

            public DayFromJson Day(InputFile_Day day)
            {
                var days = GetDays(day);
                if (days.Count > 0) return days.First();
                return null;
            }

            public List<DayFromJson> AllDays()
            {
                return GetDays(year.Days.ToArray());
            }
        }
    }
