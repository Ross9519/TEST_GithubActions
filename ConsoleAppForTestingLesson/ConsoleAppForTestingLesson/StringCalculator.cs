namespace ConsoleAppForTestingLesson
{
    public class StringCalculator
    {
        private string[] _separators = [",", "\n"];
        private readonly char _baseSeparator = ',';

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;
            if (numbers.StartsWith("//"))
            {
                var tmp = numbers.Split('\n')[0];
                var tmpLength = tmp.Length;
                if(tmpLength == 3)
                    _separators[0] = tmp[2].ToString();
                var tmpSeparators = _separators.ToList();
                while(tmp.Contains('['))
                {
                    var open = tmp.IndexOf('[');
                    var close = tmp.IndexOf(']');
                    tmpSeparators.Add(tmp[(open + 1)..close]);
                    tmp = tmp[(close + 1)..];
                }
                _separators = [.. tmpSeparators];
                numbers = numbers[(tmpLength+1)..];
            }
            _separators.ToList().ForEach(s => numbers = numbers.Replace(s, _baseSeparator.ToString()));
            var parsedNumbers = numbers.Split(_baseSeparator).Select(n => int.Parse(n)).Where(n => n <= 1000);
            if(parsedNumbers.Any(n => n < 0))
                throw new Exception($"Negatives not allowed: {string.Join(", ", parsedNumbers.Where(n => n < 0))}");
            return parsedNumbers.Sum();
        }
    }
}