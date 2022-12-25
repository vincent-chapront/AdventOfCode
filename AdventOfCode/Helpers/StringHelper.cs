using System;
using System.Text;

namespace AdventOfCode
{
    public static class StringHelper
    {
        public static string ReplaceAtPosition(this string oldValue, int position, char newValue)
        {
            var sb = new StringBuilder(oldValue);
            sb.Remove(position, 1);
            sb.Insert(position, newValue);
            return sb.ToString();
        }

        public static string[] ToLines(this string resource)
        {
            return resource.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n");
        }
    }

    public class MyCharEnumerator
    {
        public long NumberRead = 0;
        private CharEnumerator enumerator;
        private bool hasOther;

        public MyCharEnumerator(string input)
        {
            Input = input;
            enumerator = input.GetEnumerator();
            hasOther = enumerator.MoveNext();
        }

        public bool HasOther => hasOther;
        public string Input { get; private set; }

        public char Read()
        {
            NumberRead++;
            var res = enumerator.Current;
            hasOther = enumerator.MoveNext();
            return res;
        }

        public string Read(int length)
        {
            NumberRead += length;
            var sb = new StringBuilder();
            for (int i = 0; i < length && hasOther; i++)
            {
                sb.Append(enumerator.Current);
                hasOther = enumerator.MoveNext();
            }
            return sb.ToString();
        }

        public string Read(long length)
        {
            NumberRead += length;
            var sb = new StringBuilder();
            for (int i = 0; i < length && hasOther; i++)
            {
                sb.Append(enumerator.Current);
                hasOther = enumerator.MoveNext();
            }
            return sb.ToString();
        }
    }
}