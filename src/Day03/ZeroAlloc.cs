namespace Day03;

public class ZeroAlloc
{
    public static void Run(string dataString)
    {
        // hell yeah, of course I wrote a shitty parser :D (ZERO ALLOC CHALLENGE!!!)
        var data = dataString.AsSpan();

        int addedPart1 = 0;
        int addedPart2 = 0;
        
        int entrance = -1;
        bool canMultiply = true;
        
        // ReSharper disable ConvertIfStatementToSwitchStatement
        for (int i = 0; i < data.Length; i++)
        {
            var current = data[i];
            
            if (current == '(')
                entrance = i;

            if (current != ')') // we only want end of function 
                continue; 
            
            if (entrance == i - 1) // look for do() or don't()
            {
                entrance = -1;
                    
                // select previous 2 characters
                if (3 > i)
                    continue; // cannot be "do"

                var buffer = data.Slice(i - 3, 2);
                if (buffer is "do")
                {
                    // "do" instruction
                    canMultiply = true;
                    continue;
                }
                    
                // select previous 5 characters
                if (6 > i)
                    continue; // cannot be "don't"

                buffer = data.Slice(i - 6, 5);
                if (buffer is "don't")
                {
                    // "don't" instruction
                    canMultiply = false;
                }
            }
            else if (entrance > 2) // make sure it's not invalid or at the beginning
            {
                var buffer = data.Slice(entrance - 3, 3);
                var startOfFirstNum = entrance + 1;
                entrance = -1;

                // We ONLY care when we want mul(...)
                if (buffer is not "mul")
                    continue;
                
                buffer = data.Slice(startOfFirstNum, i - startOfFirstNum);
                var separator = buffer.IndexOf(',');
                        
                if (separator == -1) // missing separator, invalid
                    continue;

                if (buffer.ContainsAnyExcept("0123456789,")) // only numbers allowed
                    continue;
                        
                // currently, the buffer SHOULD look like this if its valid: 401,230

                // parse out the two numbers
                if (!int.TryParse(buffer[..separator], out var firstNumber) || !int.TryParse(buffer[(separator + 1)..], out var secondNumber))
                    continue;

                var result = firstNumber * secondNumber;
                addedPart1 += result;

                if (canMultiply)
                    addedPart2 += result;
            }
        }
        
        Console.WriteLine($"(Zero Alloc) Part 1: {addedPart1}");
        Console.WriteLine($"(Zero Alloc) Part 2: {addedPart2}");
    }
}