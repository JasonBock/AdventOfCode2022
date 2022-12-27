using System.Collections.Generic;

namespace AdventOfCode2022.Day10;

public static class SolutionDay10
{
    public static CPU RunCPU(IEnumerable<string> instructions) => 
        new CPU(instructions);
}

public sealed class CPU
{
    public CPU(IEnumerable<string> instructions)
    {
        var screenBuffer = new char[240];

        this.XRegister = 1;

        foreach(var instruction in instructions) 
        {
            this.DrawPixel(screenBuffer);
            this.Cycle++;
            this.UpdateSignalStrength();

            if (instruction.StartsWith("addx"))
            {
                this.DrawPixel(screenBuffer);
                this.Cycle++;
                this.UpdateSignalStrength();
                var value = int.Parse(instruction.Split(' ')[1]);
                this.XRegister += value;
            }
        }

        this.Display = new[]
        {
            new string(screenBuffer[0..40]),
            new string(screenBuffer[40..80]),
            new string(screenBuffer[80..120]),
            new string(screenBuffer[120..160]),
            new string(screenBuffer[160..200]),
            new string(screenBuffer[200..240]),
        };
    }

    private void DrawPixel(char[] screenBuffer)
    {
        var currentPixel = this.Cycle;

        screenBuffer[currentPixel] = (currentPixel % 40) >= this.XRegister - 1 && (currentPixel % 40) <= this.XRegister + 1 ?
            '#' : '.';
    }

    private void UpdateSignalStrength()
    {
        if (this.Cycle == 20 ||
            (this.Cycle > 20) && (this.Cycle - 20) % 40 == 0)
        {
            this.SignalStrength += this.Cycle * this.XRegister;
        }
    }

    public int Cycle { get; private set; }
    public string[] Display { get; private set; }
    public int XRegister { get; private set; }
    public int SignalStrength { get; private set; }
}