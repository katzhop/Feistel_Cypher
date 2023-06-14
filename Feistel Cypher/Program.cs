using System;

namespace Feistel_Cypher
{
    class Program
    {
        public static char[] alphabet = { ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', 
            '/', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 
            'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 
            '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~' };
        public static int[] binary = { 00100000, 00100001, 00100010, 00100011, 00100100, 00100101, 00100110, 00100111, 
            00101000, 00101001, 00101010, 00101011, 00101100, 00101101, 00101110, 00101111, 00110000, 00110001, 00110010, 
            00110011, 00110100, 00110101, 00110110, 00110111, 00111000, 00111001, 00111010, 00111011, 00111100, 00111101, 
            00111110, 00111111, 01000000, 01000001, 01000010, 01000011, 01000100, 01000101, 01000110, 01000111, 01001000, 
            01001001, 01001010, 01001011, 01001100, 01001101, 01001110, 01001111, 01010000, 01010001, 01010010, 01010011, 
            01010100, 01010101, 01010110, 01010111, 01101000, 01011001, 01011010, 01011011, 01011100, 01011101, 01011110, 
            01011111, 01100000, 01100001, 01100010, 01100011, 01100100, 01100101, 01100110, 01100111, 01101000, 01101001, 
            01101010, 01101011, 01101100, 01101101, 01101110, 01101111, 01110000, 01110001, 01110010, 01110011, 01110100, 
            01110101, 01110110, 01110111, 01111000, 01111001, 01111010, 01111011, 01111100, 01111101, 01111110, 01111111 };
        public static int ContainsL(char letter)
        {
            bool stop = false;
            int i = 0;
            while (stop == false && i < alphabet.Length)
            {
                if (letter == alphabet[i])
                {
                    stop = true;
                }
                else
                {
                    i++;
                }
            }
            return i;
        }
        public static int ContainsB(int num)
        {
            bool stop = false;
            int i = 0;
            while (stop == false && i < binary.Length)
            {
                if (num == binary[i])
                {
                    stop = true;
                }
                else
                {
                    i++;
                }
            }
            return i;
        }
        public static int[] digits(int a)
        {
            int[] digits = new int[8];
            for (int i = 7; i >= 0; i--)
            {
                digits[i] = a % 10;
                a = a / 10;
            }
            return digits;
        }
        public static int arrayToPosition(int[] a)
        {
            string num = "";
            for (int i = 0; i < 8; i++)
            {
                num += a[i];
            }
            int numN = Int32.Parse(num);
            numN = ContainsB(numN);
            return numN;
        }
        public static int Function(int b)
        {
            b = b + 33;
            return b;
        }
        public static char[] Feistel(char a, char b)
        {
            char[] output = new char[2];
            output[0] = b;
            int aP = ContainsL(a);
            int bP = ContainsL(b);
            bP = Function(bP);
            aP = binary[aP];
            if (bP >= alphabet.Length)
            {
                output[1] = '.';
            }
            else
            {
                bP = binary[bP];
                int[] aD = digits(aP);
                int[] bD = digits(bP);
                int[] XOR = new int[8];
                for (int i = 0; i < 8; i++)
                {
                    if (aD[i] != bD[i])
                    {
                        XOR[i] = 1;
                    }
                    else
                    {
                        XOR[i] = 0;
                    }
                }
                bP = arrayToPosition(XOR);
                if (bP >= alphabet.Length)
                {
                    output[1] = '.';
                }
                else
                {
                    output[1] = alphabet[bP];
                }
            }
            return output;
        }
        public static void Main(string[] args)
        {
            bool stop = false;
            Console.WriteLine("Enter String: ");
            string input = Console.ReadLine();
            char[] inputA = input.ToCharArray();
            while (stop == false)
            {
                Console.WriteLine("-----------------MAIN MENU---------------------------\n1. Enter String\n2. Encrypt Strin`g\n3. Exit program\nEnter option number: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter String: ");
                        input = Console.ReadLine();
                        inputA = input.ToCharArray();
                        break;
                    case 2:
                        char[] output = new char[2];
                        if (inputA.Length % 2 == 1)
                        {
                            for (int i = 0; i < inputA.Length; i += 2)
                            {
                                if (i == inputA.Length - 1)
                                {
                                    output = Feistel(inputA[i], ' ');
                                    inputA[i] = output[1];
                                }
                                else
                                {
                                    output = Feistel(inputA[i], inputA[i + 1]);
                                    inputA[i] = output[1];
                                    inputA[i + 1] = output[0];
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < inputA.Length - 1; i += 2)
                            {
                                output = Feistel(inputA[i], inputA[i + 1]);
                                inputA[i] = output[1];
                                inputA[i + 1] = output[0];
                            }
                        }
                        Console.Write("Encrypted String: ");
                        for (int i = 0; i < inputA.Length; i++)
                        {
                            Console.Write(inputA[i]);
                        }
                        Console.WriteLine();
                        break;
                    case 3:
                        stop = true;
                        break;
                    default:
                        Console.WriteLine("Enter a valid option: ");
                        break;

                }
            }
        }
    }
}
