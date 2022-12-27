using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Day1;

public static class SolutionDay1
{
    public static int Run(string[] calories) 
    {
        var caloriesCarriedByElves = new List<int>();
        var currentElf = 0;

        for(var i = 0; i < calories.Length; i++)
        {
            var calorieValue = calories[i];

            if (calorieValue.Length == 0)
            {
                caloriesCarriedByElves.Add(currentElf);
                currentElf = 0;
            }
            else
            {
                currentElf += int.Parse(calorieValue);
            }
        }

        if (currentElf > 0) 
        {
            caloriesCarriedByElves.Add(currentElf); 
        }

        return caloriesCarriedByElves.OrderDescending().First();
    }

    public static int RunFaster(string[] calories)
    {
        var currentHighestCalorieValue = 0;
        var currentElf = 0;

        for (var i = 0; i < calories.Length; i++)
        {
            var calorieValue = calories[i];

            if (calorieValue.Length == 0)
            {
                currentHighestCalorieValue = currentElf > currentHighestCalorieValue ? currentElf : currentHighestCalorieValue;
                currentElf = 0;
            }
            else
            {
                currentElf += int.Parse(calorieValue);
            }
        }

        if(currentElf > 0)
        {
            currentHighestCalorieValue = currentElf > currentHighestCalorieValue ? currentElf : currentHighestCalorieValue;
        }

        return currentHighestCalorieValue;
    }

    public static int GetTop3Total(string[] calories)
    {
        var highestCalorieValues = new List<int>();
        var currentElf = 0;

        for (var i = 0; i < calories.Length; i++)
        {
            var calorieValue = calories[i];

            if (calorieValue.Length == 0)
            {
                highestCalorieValues.Add(currentElf);
                highestCalorieValues = highestCalorieValues.OrderDescending().Take(3).ToList();
                currentElf = 0;
            }
            else
            {
                currentElf += int.Parse(calorieValue);
            }
        }

        if (currentElf > 0)
        {
            highestCalorieValues.Add(currentElf);
            highestCalorieValues = highestCalorieValues.OrderDescending().Take(3).ToList();
        }

        return highestCalorieValues.Sum();
    }
}