using System;
using System.Collections.Generic;
/*Спроектировать класс, для представления детерминированного конечного автомата (ДКА).
*/

namespace Lesson12.DS4
{
    public class Program
    {
        static void Main()
        {
            var alphabet = new HashSet<char> { '0', '1' };
            var states = new HashSet<string> { "A", "B", "C" };
            var initialState = "A";
            var acceptingStates = new HashSet<string> { "C" };
            var transitions = new Dictionary<(string, char), string>
            {
                { ("A", '0'), "A" },
                { ("A", '1'), "B" },
                { ("B", '0'), "C" },
                { ("B", '1'), "A" },
                { ("C", '0'), "B" },
                { ("C", '1'), "C" }
            };
            var automaton = new Automaton(alphabet, states, initialState, acceptingStates, transitions);
            Console.WriteLine(automaton.Accepts("1101"));
            Console.WriteLine(automaton.Accepts("0101"));
        }
    }

    public class Automaton
    {
        public HashSet<char> Alphabet { get; }
        public HashSet<string> States { get; }
        public string InitialState { get; }
        public HashSet<string> AcceptingStates { get; }
        public Dictionary<(string, char), string> Transitions { get; }

        public Automaton(
            HashSet<char> alphabet,
            HashSet<string> states,
            string initialState,
            HashSet<string> acceptingStates,
            Dictionary<(string, char), string> transitions)
        {
            Alphabet = alphabet;
            States = states;
            InitialState = initialState;
            AcceptingStates = acceptingStates;
            Transitions = transitions;
        }

        // Метод для проверки, принимает ли автомат данную строку
        public bool Accepts(string input)
        {
            var currentState = InitialState;
            foreach (var symbol in input)
            {
                if (!Alphabet.Contains(symbol))
                {
                    throw new ArgumentException($"Символ {symbol} не содержится в алфавите.");
                }
                if (!Transitions.ContainsKey((currentState, symbol)))
                {
                    return false;
                }
                currentState = Transitions[(currentState, symbol)];
            }
            return AcceptingStates.Contains(currentState);
        }
    }
}
