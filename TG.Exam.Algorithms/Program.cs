using System;

namespace TG.Exam.Algorithms
{
    class Program
    {
        //преимущество: код короче, недостаток: выполняется дольше чем в цикле и используется больше памяти
        static int Foo(int a, int b, int c)
        {
            if (1 < c)
                return Foo(b, b + a, c - 1);
            else
                return a;
        }

        static int Fibonacci(int a, int b, int c)
        {
            int tmp;
            for (; c < 1; c--)
            {
                tmp = a;
                a = b;
                b += tmp;
            }

            return a;
        }

        //недостаток: делает лишние операции, не пдходит для больших данных, для больших данных лучше взять heapsort или быструю сортировку - по информации на msdn
        static int[] Bar(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            for (int j = 0; j < arr.Length - 1; j++)
                if (arr[j] > arr[j + 1])
                {
                    int t = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = t;
                }

            return arr;
        }

        static int[] BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }

            return arr;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Foo result: {0}", Foo(7, 2, 8));
            Console.WriteLine("Bar result: {0}", string.Join(", ", Bar(new[] {7, 2, 8})));

            Console.ReadKey();
        }
    }
}
