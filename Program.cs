using System;

namespace CRC
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CRC Calculater (CRC-16-IBM)");
            Console.WriteLine("データを入力してください");
            Console.WriteLine("入力フォーマット：[16bit整数] 半角スペース [16bit整数] 半角スペース・・・・・[16bit整数]");
            Console.WriteLine("入力例） 12 34 56 78 90");

            var inputError = true;
            var data = new int[0];

            while(inputError)
            {
                var input = Console.ReadLine().Split(' ');
                Array.Resize(ref data, input.Length);

                for(var i =0; i<input.Length; i++)
                {
                    if (!int.TryParse(input[i], out data[i]))
            　　    {
                        Console.WriteLine("数字を入力してください");
                        Console.ReadKey();
                        inputError = true;
                        break;
          　　      }

                    if(data[i]<0 || 65535<data[i])
                    {
                        Console.WriteLine("数値は0～65535の間で入力してください（16bit整数）");
                        Console.ReadKey();
                        inputError = true;
                        break;
                    }
                    inputError = false;
                }
            }

            //CRC計算（CRC16-IBM / 生成多項式:x16+x15+x2+1, initial value is 0xFFFF, 右回り）
            int CRC = 0xffff;
            int CRC16POLY = 0xa001;

            foreach (var d in data)
            {
                CRC ^= d;

                for (var j = 0; j < 8; j++)
                {
                    if ((CRC & 1) == 1)
                    {
                        CRC >>= 1;
                        CRC ^= CRC16POLY;
                    }
                    else
                    {
                        CRC >>= 1;
                    }
                }
            }

            Console.WriteLine("DEC:"+CRC+", HEX:"+Convert.ToString(CRC,16));
            Console.ReadKey();
        }
    }
}
