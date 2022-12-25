namespace AdventOfCode.Y2022
{
    internal class Day02 : GenericDay
    {
        public string Compute1(string[] input)
        {
            var totalScore = 0;
            foreach (var line in input)
            {
                var a = line.Split(' ');
                var score = 0;
                switch (a[0])
                {
                    case "A":
                        switch (a[1])
                        {
                            case "X":
                                score = 1 + 3;
                                break;
                            case "Y":
                                score = 2 + 6;
                                break;
                            case "Z":
                                score = 3 + 0;
                                break;
                        }
                        break;
                    case "B":
                        switch (a[1])
                        {
                            case "X":
                                score = 1 + 0;
                                break;
                            case "Y":
                                score = 2 + 3;
                                break;
                            case "Z":
                                score = 3 + 6;
                                break;
                        }
                        break;
                    case "C":
                        switch (a[1])
                        {
                            case "X":
                                score = 1 + 6;
                                break;
                            case "Y":
                                score = 2 + 0;
                                break;
                            case "Z":
                                score = 3 + 3;
                                break;
                        }
                        break;
                }
                totalScore += score;
            }
            return totalScore.ToString();
        }

        public string Compute2(string[] input)
        {
            var totalScore = 0;
            foreach (var line in input)
            {
                var a = line.Split(' ');
                var score = 0;
                switch (a[0])
                {
                    case "A":
                        switch (a[1])
                        {
                            case "X":
                                score = 0 + 3;
                                break;
                            case "Y":
                                score = 3 + 1;
                                break;
                            case "Z":
                                score = 6 + 2;
                                break;
                        }
                        break;
                    case "B":
                        switch (a[1])
                        {
                            case "X":
                                score = 0 + 1;
                                break;
                            case "Y":
                                score = 3 + 2;
                                break;
                            case "Z":
                                score = 6 + 3;
                                break;
                        }
                        break;
                    case "C":
                        switch (a[1])
                        {
                            case "X":
                                score = 0 + 2;
                                break;
                            case "Y":
                                score = 3 + 3;
                                break;
                            case "Z":
                                score = 6 + 1;
                                break;
                        }
                        break;
                }
                totalScore += score;
            }
            return totalScore.ToString();
        }
    }

}