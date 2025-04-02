using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        bool _exit = false;

        do
        {
            Console.WriteLine("Select a string operation to perform:");
            Console.WriteLine("1  - Find duplicated characters in a string");
            Console.WriteLine("2  - Get all unique characters in a string");
            Console.WriteLine("3  - Remove duplicated characters in a string");
            Console.WriteLine("4  - Reverse a string");
            Console.WriteLine("5  - Reverse a string in a sentence but maintain word order");
            Console.WriteLine("6  - Get word count");
            Console.WriteLine("7  - Check palindrome");
            Console.WriteLine("8  - Find character(s) with max occurrences");
            Console.WriteLine("9  - Get all possible substrings in a string");
            Console.WriteLine("10 - Get and capitalize the first letter of each word in a sentence");
            Console.WriteLine("X  - Exit");

            Console.Write("Your choice: ");

            var _readKey = Console.ReadLine();

            string _result = string.Empty;

            switch (_readKey.ToLower())
            {
                case "1":
                    Prompt(FindDupCharsOp, false, "The duplicated character(s) is/are: ");
                    break;
                case "2":
                    Prompt(FindUniqueCharsOp, false, "The unique character(s) is/are: ");
                    break;
                case "3":
                    Prompt(RemoveDupCharsOp, false, "The result is: ");
                    break;
                case "4":
                    Prompt(ReverseStringOp, true, "The reversed string is: ");
                    break;
                case "5":
                    Prompt(ReverseStringInSentenceOp, true, "The reversed string is: ");
                    break;
                case "6":
                    Prompt(GetWordCountOp, false, "The word count is: ");
                    break;
                case "7":
                    Prompt(CheckPalindromeOp, false, "Is palindrome? ");
                    break;
                case "8":
                    Prompt(FindMaxOccurrencesOp, false, "Character(s) with max occurrences: ");
                    break;
                case "9":
                    Prompt(GetAllSubstringsOp, false, "All possible substrings:\n");
                    break;
                case "10":
                    Prompt(GetFirstLetterCapitalOp, false, "First letter with capital: ");
                    break;
                case "x":
                    _exit = true;
                    break;
                default:
                    Console.WriteLine("Incorrect choice. Try again.");
                    break;
            }
        }
        while (!_exit);
    }

    private static void Prompt(Func<string, string> p_dlgToRun, bool p_strCaseSensitive, string p_strResultSentence)
    {
        Console.WriteLine("Enter a string to check:");

        var _input = Console.ReadLine();

        string _result = string.Empty;

        if (p_strCaseSensitive)
            _result = p_dlgToRun(_input);
        else
            _result = p_dlgToRun(_input.ToLower());

        Console.WriteLine(p_strResultSentence + _result);
        Console.WriteLine("\n");
    }

    private static string FindDupCharsOp(string p_strInput)
    {
        HashSet<char> dupChars = new HashSet<char>();

        for (int i = 1; i < p_strInput.Length; i++)
            if (p_strInput.Substring(i).IndexOf(p_strInput[i - 1]) != -1)
                dupChars.Add(p_strInput[i - 1]);

        StringBuilder _result = new StringBuilder();

        foreach (char @char in dupChars)
            _result.Append(@char);

        return _result.ToString();
    }

    private static string FindUniqueCharsOp(string p_strInput)
    {
        HashSet<char> uniqueChars = new HashSet<char>();
        HashSet<char> usedChars = new HashSet<char>();

        for (int i = 0; i < p_strInput.Length; i++)
        {
            if (!uniqueChars.Contains(p_strInput[i]) && !usedChars.Contains(p_strInput[i]))
                uniqueChars.Add(p_strInput[i]);
            else
            {
                uniqueChars.Remove(p_strInput[i]);
                usedChars.Add(p_strInput[i]);
            }
        }

        StringBuilder _result = new StringBuilder();

        foreach (char @char in uniqueChars)
            _result.Append(@char);

        return _result.ToString();
    }

    private static string RemoveDupCharsOp(string p_strInput)
    {
        HashSet<char> usedChars = new HashSet<char>();
        StringBuilder _result = new StringBuilder();

        foreach (char @char in p_strInput)
            if (usedChars.Add(@char))
                _result.Append(@char);

        return _result.ToString();
    }

    private static string ReverseStringOp(string p_strInput)
    {
        StringBuilder _result = new StringBuilder();

        for (int i = p_strInput.Length - 1; i >= 0; i--)
            _result.Append(p_strInput[i]);

        return _result.ToString();
    }

    private static string ReverseStringInSentenceOp(string p_strInput)
    {
        StringBuilder _result = new StringBuilder();

        string[] words = p_strInput.Split(' ');

        foreach (string word in words)
        {
            for (int i = word.Length - 1; i >= 0; i--)
                _result.Append(word[i]);
            _result.Append(' ');
        }

        return _result.ToString().Substring(0, _result.Length - 1);
    }

    private static string GetWordCountOp(string p_strInput)
    {
        if (string.IsNullOrEmpty(p_strInput))
            return "0";

        return p_strInput.Trim().Split(' ').Length.ToString();
    }

    private static string CheckPalindromeOp(string p_strInput)
    {
        string _temp = p_strInput.Replace(" ", "");

        for (
            int i = 0; i < _temp.Length / 2; i++)
            if (!_temp[i].Equals(_temp[_temp.Length - i - 1]))
                return "False";

        return "True";
    }

    private static string FindMaxOccurrencesOp(string p_strInput)
    {
        if (string.IsNullOrEmpty(p_strInput))
            return "N/A";

        Dictionary<char, int> occurrences = new Dictionary<char, int>();

        foreach (char @char in p_strInput)
            if (!@char.Equals(' '))
            {
                if (occurrences.ContainsKey(@char))
                    occurrences[@char]++;
                else
                    occurrences.Add(@char, 1);
            }

        int _max = 0;

        foreach (KeyValuePair<char, int> _pair in occurrences)
        {
            if (_pair.Value > _max)
                _max = _pair.Value;
        }

        HashSet<char> _maxChars = new HashSet<char>();

        foreach (KeyValuePair<char, int> _pair in occurrences)
        {
            if (_pair.Value == _max)
                _maxChars.Add(_pair.Key);
        }

        StringBuilder _result = new StringBuilder();
        foreach (char @char in _maxChars)
            _result.Append(@char + " ");

        string _strResult = _result.ToString();

        return _strResult.Substring(0, _strResult.Length - 1) + " (" + _max + ")";
    }

    private static string GetAllSubstringsOp(string p_strInput)
    {
        HashSet<string> _substrings = new HashSet<string>();

        for (int i = 0; i < p_strInput.Length; i++)
        {
            for (int j = 0; j < p_strInput.Length - i; j++)
                _substrings.Add(p_strInput.Substring(i, j + 1));
        }

        StringBuilder _result = new StringBuilder();

        foreach (string _substring in _substrings)
            _result.Append(_substring + "\n");

        return _result.ToString();
    }

    private static string GetFirstLetterCapitalOp(string p_strInput)
    {
        if (string.IsNullOrEmpty(p_strInput))
            return string.Empty;

        string _temp = p_strInput.Trim();
        string[] _words = p_strInput.Split(" ");

        StringBuilder _result = new StringBuilder();

        foreach (string _word in _words)
            _result.Append(_word.Substring(0, 1).ToUpper() + " ");

        return _result.ToString(0, _result.Length - 1);
    }
}