using System.Collections.Generic;

namespace func.brainfuck;

public class BrainfuckLoopCommands
{
    static Stack<int> Stack = new Stack<int>();
    static Dictionary<int, int> CycleOpen = new Dictionary<int, int>();
    static Dictionary<int, int> CycleClose = new Dictionary<int, int>();
    private static void FindBrackets(IVirtualMachine vm)
    {
        for (int i = 0; i < vm.Instructions.Length; i++)
        {
            if (vm.Instructions[i] == '[') Stack.Push(i);
            if (vm.Instructions[i] == ']')
            {
                CycleOpen[i] = Stack.Pop();
                CycleClose[CycleOpen[i]] = i;
            }
        }
    }

    public static void RegisterTo(IVirtualMachine vm)
    {
        FindBrackets(vm);
        vm.RegisterCommand(']', b =>
        {
            if (b.Memory[b.MemoryPointer] != 0)
                b.InstructionPointer = CycleOpen[b.InstructionPointer];
        });
        vm.RegisterCommand('[', b =>
        {
            if (b.Memory[b.MemoryPointer] == 0)
                b.InstructionPointer = CycleClose[b.InstructionPointer];
        });
    }
}
