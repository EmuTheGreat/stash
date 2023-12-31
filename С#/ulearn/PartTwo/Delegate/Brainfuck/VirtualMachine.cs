using System;
using System.Collections;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }
        public Dictionary<char, Action<IVirtualMachine>> Command;

        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
            Command = new Dictionary<char, Action<IVirtualMachine>>();
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute) => Command.Add(symbol, execute);

        public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
                if (Command.TryGetValue(Instructions[InstructionPointer], out Action<IVirtualMachine> command))
                    command(this);
                InstructionPointer++;
            }
        }
    }
}