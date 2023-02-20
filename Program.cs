using System;

class Program
{
    static void Main(string[] args)
    {
        string input = string.Empty;

        string[] inputArray;
        string inputPattern = string.Empty;
        string inputValue = string.Empty;

        do{
            Console.Write("Enter String: ");
            input = Console.ReadLine();

            if(input != string.Empty){
                
                inputArray = input.Split("|");
                inputPattern = inputArray[0];
                inputValue = inputArray[1];

                Console.WriteLine(PatternMatches(inputPattern,inputValue));
            }
        }
        while(input != string.Empty);

    }

    static bool PatternMatches(string pattern, string input)
    {
        int patternIndex = 0;
        int inputIndex = 0;

        while(patternIndex < pattern.Length && inputIndex < input.Length){
            char currentPatternChar = pattern[patternIndex];
            char currentInputChar = input[inputIndex];
            
            switch (currentPatternChar) 
            {
                case '@':
                    if(!char.IsLetter(currentInputChar))
                    {
                        return false;
                    }
                    break;
                case '!':
                    if(!char.IsDigit(currentInputChar) || currentInputChar == '0')
                    {
                        return false;
                    }
                    break;
                case '%':
                    //default sequence character length
                    int sequenceLength = 2;

                    int indexFrom = pattern.IndexOf('{') + 1;
                    int indexTo = pattern.IndexOf('}');
                    if(patternIndex < pattern.Length - 2 && pattern[patternIndex + 1] == '{')
                    {
                        int.TryParse(pattern.Substring(indexFrom,indexTo - indexFrom),out sequenceLength);
                        
                        if(sequenceLength < 2)
                        {
                            Console.WriteLine("sequence default length is 2");
                            break;
                        }
                        
                        patternIndex += indexTo;
                    }
                    for (int i = 0; i < sequenceLength; i++)
                    {
                        if(inputIndex + i >=input.Length || !char.IsLetter(input[inputIndex + 1]))
                        {
                            return false;
                        }
                    }
                    inputIndex += sequenceLength - 1;
                    break;
                default:
                    throw new ArgumentException("Invalid Character: " + currentPatternChar);
            }

            patternIndex++;
            inputIndex++;
        }

        return patternIndex == pattern.Length && inputIndex == input.Length;
    }

}
