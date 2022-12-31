using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal partial class Program
    {
        private static Point2d GetFirstValid(Range g1X, Range g1Y, Range g2X, Range g2Y)
        {
            for (int r = g1X.Min; r < g1X.Max; r++)
            {
                if (!g2X.IsInRange(r))
                {
                    for (int c = g1Y.Min; c < g1Y.Max; c++)
                    {
                        if (!g2Y.IsInRange(r))
                        {
                            return new Point2d(r, c);
                        }
                    }
                }
            }

            return null;
        }

        private static void Main(string[] args)
        {
            var years = Newtonsoft.Json.JsonConvert.DeserializeObject<InputFile_Year>(System.IO.File.ReadAllText("Input2022.json"));
            var y = new YearFromJson(years);
            var d = y.Day(years.Days.First(x=>x.Day==15));
            d.Run();
            //var days = y.AllDays();
            //days.ForEach(d => d.Run());
            //d.Run();
            //var d2 = y.AllDays();
            //GenericDays.GenericRun2(years[0].Year, years[0].Days[0].Day);
            ;
            //new Y2022.Days().Run(07);
        }

        private static List<Edge> RangesToEdges(Range g1X, Range g1Y)
        {
            var p1 = new Point2d(g1X.Min, g1Y.Min);
            var p2 = new Point2d(g1X.Min, g1Y.Max + 1);
            var p3 = new Point2d(g1X.Max + 1, g1Y.Max + 1);
            var p4 = new Point2d(g1X.Max + 1, g1Y.Min);
            return new List<Edge>()
            {
                new Edge(p1, p2),
                new Edge(p2,p3),
                new Edge(p3,p4),
                new Edge(p4,p1)
            };
        }

        private static List<Point2d> RangesToPoints(Range g1X, Range g1Y)
        {
            return new List<Point2d>()
            {
                new Point2d(g1X.Min, g1Y.Min),
                new Point2d(g1X.Min, g1Y.Max+1),
                new Point2d(g1X.Max+1, g1Y.Max+1),
                new Point2d(g1X.Max+1, g1Y.Min)
            };
        }

        private static void SutherlandHodgeman()
        {
            var subjectPolygon = RangesToPoints(new Range(0, 2), new Range(0, 2));
            var clipPolygon = RangesToEdges(new Range(0, 1), new Range(0, 1));

            ;
            var outputList = subjectPolygon;

            foreach (Edge clipEdge in clipPolygon)
            {
                var inputList = outputList;
                outputList.Clear();

                for (int i = 0; i < inputList.Count; i += 1)
                {
                    var current_point = inputList[i];
                    var prev_point = inputList[(i - 1) % inputList.Count];
                    /*

                Point Intersecting_point = ComputeIntersection(prev_point, current_point, clipEdge)

                if (current_point inside clipEdge) then
                    if (prev_point not inside clipEdge) then
                        outputList.add(Intersecting_point);
                    end if
                    outputList.add(current_point);
                else if (prev_point inside clipEdge) then
                    outputList.add(Intersecting_point);
                end if

                     * */
                }
            }
        }

        private static void Test()
        {
            var g1X = new Range(1, 3);
            var g1Y = new Range(1, 3);

            var g2X = new Range(1, 2);
            var g2Y = new Range(1, 2);
            Point2d p;
            p = GetFirstValid(g1X, g1Y, g2X, g2Y);
            ;
        }

        private class Edge
        {
            public Point2d P1;
            public Point2d P2;

            public Edge(Point2d p1, Point2d p2)
            {
                P1 = p1;
                P2 = p2;
            }
        }
    }
}