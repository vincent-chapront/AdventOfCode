using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Y2015
{
    internal class Day23 : GenericDay
    {
        protected override object Part1()
        {
            var test=@"inc a
jio a, +2
tpl a
inc a".ToLines();
            Assert.AreEqual(2,Compute1(test).a);
            var res = Compute1(Resources.Year2015.Day23.ToLines());
            Assert.AreEqual(170, res.b);
            return res.b;
        }

        protected override object Part2()
        {
            var res = Compute1(Resources.Year2015.Day23.ToLines(),1);
            Assert.AreEqual(247, res.b);
            return res.b;
        }

        private static (long a, long b) Compute1(string[] input, long startA=0, long startB=0)
        {
            var a=startA;
            var b=startB;
            var currentLine=0;
            while(currentLine<input.Length)
            {
                var line=input[currentLine];
                if(line.StartsWith("hlf"))
                {
                    if(line.EndsWith("a")){
                        a/=2;
                    }
                    else{
                        b/=2;
                    }
                    currentLine++;
                }
                else if(line.StartsWith("tpl"))
                {
                    if(line.EndsWith("a")){
                        a*=3;
                    }
                    else{
                        b*=3;
                    }
                    currentLine++;
                }
                else if(line.StartsWith("inc"))
                {
                    if(line.EndsWith("a")){
                        a++;
                    }
                    else{
                        b++;
                    }
                    currentLine++;
                }
                else if(line.StartsWith("jmp"))
                {
                    var value=int.Parse(line.Split(" ")[1].Replace("+",""));
                    currentLine+=value;
                }
                else if(line.StartsWith("jio"))
                {
                    var val=0L;
                    if(line.Contains("a,")){
                        val=a;
                    }
                    else{
                        val=b;
                    }
                    var value=int.Parse(line.Split(" ")[2].Replace("+",""));
                    currentLine+=val==1?value:1;
                }
                else if(line.StartsWith("jie"))
                {
                    var val=0L;
                    if(line.Contains("a,")){
                        val=a;
                    }
                    else{
                        val=b;
                    }
                    var value=int.Parse(line.Split(" ")[2].Replace("+",""));
                    currentLine+=val%2==0?value:1;
                }
            }

            return (a,b);
        }

        public string Compute2(params string[] input)
        {
            return "ERROR";
        }
    }
}