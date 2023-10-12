/*Переберите все арифметические формулы размера L, использующие константы 0, 1, 2, переменную x, бинарные операции -, +, *, и скобки. 
 * Размер выражения — это количество использованных бинарных операций. Пример формул размера 1: 0+1, x*2, размера 4: x*x+2*x+1, (1+x)*1*2+0.
Как можно не перечислять выражения, эквивалентные уже перечисленным ранее? Например, если перечислено выражение 2+x*1, 
то не нужно перечислять выражения x*1+2, 1*x+2 и x+2+0.*/

using System;
using System.Collections.Generic;

namespace Lesson2.YR1
{
    class Program : Calculator
    {
        static char[] values = new[] { '0', '1', '2', 'x' };
        static char[] operations = new[] { '-', '+', '*' };

        static IEnumerable<char[]> DoTransposition(char[] s, int pos, char[] elements)
        {
            if (pos == s.Length)
            {
                yield return s;
                yield break;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                s[pos] = elements[i];
                foreach (var e in DoTransposition(s, pos + 1, elements)) yield return e;
            }
        }

        static List<char> Merge(char[] values, char[] operation)
        {
            var s = new List<char>();
            for (int i = 0; i < operation.Length; i++)
            {
                s.Add(values[i]);
                s.Add(operation[i]);
            }
            s.Add(values[values.Length - 1]);
            return s;
        }

        static string AddBrackets(List<char> s)
        {
            for (var i = 0; i < s.Count; i++)
            {
                if (s[i] == '+' || s[i] == '-')
                {
                    s.Insert(i - 1, '(');
                    i += 1;
                }
                if (s[i] == '*')
                {
                    s.Insert(i, ')');
                    i += 1;
                }
            }
            return string.Join("", s);
        }

        static void Main()
        {
            int L = 2;

            var e = new string[] { "a", "b", "c" };
            var s = new string[] { "a", "b", "c" };
            Console.WriteLine(s.C);

            //foreach (var e in DoTransposition(new char[L + 1], 0, values))
            //{
            //    foreach (var r in DoTransposition(new char[L], 0, operations))
            //    {
            //        Console.WriteLine(string.Join("", Merge(e, r)));

            //        //var item = Merge(r, e);
            //        //var itemString = string.Join("", item);
            //        //Console.WriteLine(Calculate(Transform(itemString)));
            //        //if (itemString.IndexOf('*') != -1 && (itemString.IndexOf('-') != -1 || itemString.IndexOf('+') != -1))
            //        //    Console.WriteLine(Calculate(Transform(AddBrackets(item))));


            //        //var item = Merge(r, e);
            //        //var itemString = string.Join("", item);
            //        //if (itemString.IndexOf('*') != -1 && (itemString.IndexOf('-') != -1 || itemString.IndexOf('+') != -1))
            //        //    Console.WriteLine(string.Join("", AddBrackets(item)));
            //    }
            //}

            //Console.WriteLine(Calculate(Transform("0-x-0")));
        }
    }

    public class Calculator
    {
        static private bool IsDelimeter(char c)
        {
            return " ".IndexOf(c) != -1;
        }
        static private int GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                default: return 5;
            }
        }
        static private bool IsOperator(char с)
        {
            return "+-*()".IndexOf(с) != -1;
        }
        static public string Transform(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]) || input[i] == 'x') //Если цифра или 'x'
                {
                    //Читаем до оператора
                    while (!IsOperator(input[i]))
                    {
                        output += input[i]; //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                }

                //Если символ - оператор
                if (IsOperator(input[i])) //Если оператор
                {
                    if (input[i] == '(') //Если символ - открывающая скобка
                        operStack.Push(input[i]); //Записываем её в стек
                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {
                        //Если в стеке есть элементы
                        if (operStack.Count > 0 && GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                            output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением
                        operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                    }
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output;
        }
        static public string Calculate(string input)
        {
            string result = string.Empty;
            Stack<string> temp = new Stack<string>();

            for (int i = 0; i < input.Length; i++)
            {
                //Если символ - цифра или 'x', то читаем все число и записываем на вершину стека
                if (Char.IsDigit(input[i]) || input[i] == 'x')
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(a); //Записываем в стек
                    i--;
                }
                else if (IsOperator(input[i])) //Если символ - оператор
                {
                    //Берем два последних значения из стека
                    var a = temp.Pop();
                    var b = temp.Pop();

                    if (a.IndexOf("x") != -1 || b.IndexOf("x") != -1)
                    {
                        if (input[i] == '*') result = b + input[i] + a;
                        else if (input[i] == '-') result = b + input[i] + a;
                        else result = b + input[i] + a;
                    }

                    else
                    {
                        int c = int.Parse(a);
                        int d = int.Parse(b);

                        switch (input[i])
                        {
                            case '+': result = (d + c).ToString(); break;
                            case '-': result = (d - c).ToString(); break;
                            case '*': result = (d * c).ToString(); break;
                        }
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }
    }
}

