using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Y2022
{
    public class Day07 : GenericDay
    {
        //protected override object Part1()
        //{
        //    Assert.AreEqual(95437, Compute1(Resources.Year2022.Day07Test.ToLines()));
        //    var res = Compute1(Resources.Year2022.Day07.ToLines());
        //    Assert.AreEqual(1543140, res);
        //    return res;
        //}

        //protected override object Part2()
        //{
        //    Assert.AreEqual(24933642, Compute2(Resources.Year2022.Day07Test.ToLines()));
        //    var res = Compute2(Resources.Year2022.Day07.ToLines());
        //    Assert.AreEqual(1117448, res);
        //    return res;
        //}
        abstract class Item
        {
            public Item(string name)
            {
                Name = name;
            }

            public string Name { get;  }
            public abstract int Size { get; }
            public override string ToString()
            {
                return Name + " : " + Size;
            }
        }
        class Directory : Item
        {
            public Directory(string name, Directory parent) : base(name){ Parent = parent; }

            public Directory Parent { get; }

            public List<Item> Content { get;  } = new List<Item>();

            public override int Size => Content.Sum(x => x.Size); 
        }
        class File : Item
        {
            public File(string name,int size) : base(name){ Size = size; }

            public override int Size { get;  }
        }

        public string Compute1(string[] input)
        {
            (_,List<Directory> directories) = GetDirectories(input);

            return directories.Select(x => x.Size).Where(x => x <= 100_000).Sum().ToString();
        }

        private static (Directory root,List<Directory> directories) GetDirectories(string[] input)
        {
            var root = new Directory("root", null);
            Directory current = root;
            var directories = new List<Directory>();
            for (int idxLine = 0; idxLine < input.Length; idxLine++)
            {
                string line = input[idxLine];
                if (line.StartsWith("$ cd /"))
                {
                    current = root;
                }
                else if (line.StartsWith("$ cd .."))
                {
                    current = current.Parent;
                }
                else if (line.StartsWith("$ cd"))
                {
                    var destName = line.Substring(5);
                    var dest = current.Content.FirstOrDefault(x => x.Name == destName) as Directory;
                    if (dest == null)
                    {
                        throw new Exception();
                    }
                    current = dest;
                }
                else
                {
                    int i = 0;
                    for (i = idxLine + 1; i < input.Length; i++)
                    {
                        line = input[i];
                        if (line.StartsWith("$"))
                        {
                            break;
                        }

                        if (line.StartsWith("dir"))
                        {
                            Directory dir = new Directory(line.Substring(4), current);
                            directories.Add(dir);
                            current.Content.Add(dir);
                        }
                        else
                        {
                            var a = line.Split(" ");
                            current.Content.Add(new File(a[1], int.Parse(a[0])));
                        }
                    }

                    idxLine = i - 1;

                }
            }

            return (root,directories);
        }

        public string Compute2(string[] input)
        {
            (Directory root, List<Directory> directories) = GetDirectories(input);
            var diskSize = 70_000_000;
            var updateSize = 30_000_000;
            var unusedSize = diskSize - root.Size;
            var neededSize = updateSize - unusedSize;



            return directories.Select(x => x.Size).OrderBy(x=>x).Where(x => x >=neededSize).First().ToString();
        }
    }
}