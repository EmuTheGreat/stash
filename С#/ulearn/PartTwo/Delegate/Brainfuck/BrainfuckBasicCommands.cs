using System;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        static string symbols =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            RegisterSymbols(vm);
            vm.RegisterCommand('.', b => write(Convert.ToChar(b.Memory[b.MemoryPointer])));
            vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = Convert.ToByte(read()));
            vm.RegisterCommand('>', b => b.MemoryPointer = ++b.MemoryPointer % b.Memory.Length);

            vm.RegisterCommand('<', b =>
            {
                ;
                if (b.MemoryPointer == 0) b.MemoryPointer = b.Memory.Length - 1;
                else b.MemoryPointer--;
            });

            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] == 255) b.Memory[b.MemoryPointer] = 0;
                else b.Memory[b.MemoryPointer]++;
            });

            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0) b.Memory[b.MemoryPointer] = 255;
                else b.Memory[b.MemoryPointer]--;
            });
        }

        public static void RegisterSymbols(IVirtualMachine vm)
        {
            foreach (var e in symbols)
                vm.RegisterCommand(e, b => { b.Memory[b.MemoryPointer] = Convert.ToByte(e); });
        }
    }
}