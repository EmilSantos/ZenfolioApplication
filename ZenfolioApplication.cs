using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Collections;

namespace ZenfolioApplication
{
  class ZenfolioApplication
  {
    static void Main(string[] args)
    {
      String userInput = "";


      //WHILE LOOP FOR USER INPUT UNTIL THEY TYPE "quit"
      while(userInput != "quit"){
        userInput = Console.ReadLine();
        int inputContainsLetters = Regex.Matches(userInput,@"[a-zA-Z]").Count;
        int inputContainsNumbers = Regex.Matches(userInput,@"\d").Count;
        bool b = Convert.ToBoolean(inputContainsLetters);

        //CHECK TO SEE IF THE USER INPUT CONTAINS LETTERS OR IS A SEQUENCE OF NUMBERS
        if(inputContainsLetters > 0){
          var count = letterFrequency(userInput);

          foreach(var character in count)
          {
          	Console.WriteLine("{0}: {1}", character.Key, character.Value);
          }

        }else if(inputContainsNumbers > 0){
          Console.WriteLine("weve got a sequence of numbers");
          String[] entries = userInput.Split();
          Double[] numbersArray = Array.ConvertAll(entries, Double.Parse);
          Array.Sort(numbersArray);

          //Mean
          Console.WriteLine("Mean: " + numbersArray.Average());

          //Median
          Console.WriteLine("Median: " + findMedian(numbersArray));

          //Mode
          Console.WriteLine("Mode: " + findMode(numbersArray));

          //Range
          Console.WriteLine("Range: " + numbersArray.Count());
        }else{
          Console.WriteLine("Please enter a sequence of numbers or a string containing letters");
        }
      }
    }

      static Double findMedian(Double[] numbers)
      {
        int numberCount = numbers.Count();
        int halfIndex = numbers.Count() / 2;

        double median;
        if((numberCount % 2) == 0)
        {
          median = ((numbers.ElementAt(halfIndex) +  numbers.ElementAt(halfIndex - 1)) / 2);
        }else{
          median = numbers.ElementAt(halfIndex);
        }
        return median;
      }

      /*
      Find the mode(s) by iterating through a sorted number list.
       
      A counter is kept in order to keep track of the largest frequency of a number.
      
      A dictionary is used to keep a frequency count for each number.
      if any of the numbers' frequencies matches the largest count they will be added to the modeList
      
      This solution is multimodal.
      */
      static string findMode(Double[] numbers)
      {
        int largest = 0;
        int counter = 1;
        Dictionary<Double,int> numberFrequencies = new Dictionary<Double,int>();
        List<Double> modeList = new List<Double>();

        for(int i = 0; i < numbers.Count(); i++)
        {
          if(i < numbers.Count()-1)
          {
            if(numbers[i] == numbers[i+1])
            {
              counter++;
            }else{
              if (counter > largest) { largest = counter; }
              numberFrequencies.Add(numbers[i], counter);
              counter = 1;
            }
          }else{
          counter = (numbers[i-1] == numbers[i]) ? counter : 1;
          if (counter > largest) { largest = counter; }
          numberFrequencies.Add(numbers[i], counter);
          }
        }

        foreach(var entry in numberFrequencies)
        {
          Console.WriteLine("{0}, {1}", entry.Key, entry.Value);
          if(entry.Value == largest)
          {
            modeList.Add(entry.Key);
          }
        }
        Console.WriteLine("largest number: " + largest);
        return String.Join(", ", modeList);
    }

    /*
    *Although Hashtables run in O(1), a SortedDictionary is much easier to use so that we
    *don't have to sort the letters.
    */
    public static SortedDictionary<char, int> letterFrequency(String stringToCount)
    {
    	SortedDictionary<char, int> characterCount = new SortedDictionary<char, int>();

    	foreach (var character in stringToCount)
    	{
    		if(char.IsLetter(character))
    		{
    			if(!characterCount.ContainsKey(character))
    			{
    				characterCount.Add(character, 1);
    			}else{
    				characterCount[character]++;
    			}
    		}
    	}

    	return characterCount;
    }
  }
}
