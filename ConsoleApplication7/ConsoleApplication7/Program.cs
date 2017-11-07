using System;
using System.Threading;

namespace PingPong
{
    class Program
    {
        static void Main(string[] args)
        {
            //posisi Bola dan gerakan tambahnya
            int posLeft = 40;
            int posTop = 8;
            int leftPlus = 1;
            int topPlus = 1;
            //dimulai dengan kecepatan 200
            int Speed = 200;
            //posisi Player 1 dan 2
            int Player1 = 5;
            int Player2 = 5;
            //mencetak player1 dan player 2
            for (int i = Player1; i < Player1 + 4; i++)
            {
                Console.SetCursorPosition(5, i); Console.WriteLine("▓");
            }

            for (int i = Player2; i < Player2 + 4; i++)
            {
                Console.SetCursorPosition(70, i); Console.WriteLine("▓");
            }

            //bidang permainan
            Console.SetCursorPosition(5, 0); Console.WriteLine("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.SetCursorPosition(5, 15); Console.WriteLine("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");

            //perulangan selama permainan
            bool Selesai = false;
            while (Selesai == false)
            {
                //mencetak posisi bola
                Console.SetCursorPosition(posLeft, posTop); Console.WriteLine((char)2);
                //membaca tombol yang ditekan
                if (Console.KeyAvailable == true)
                {
                    //menghapus player 1 dan 2
                    for (int i = Player1; i < Player1 + 4; i++)
                    {
                        Console.SetCursorPosition(5, i); Console.WriteLine(" ");
                    }

                    for (int i = Player2; i < Player2 + 4; i++)
                    {
                        Console.SetCursorPosition(70, i); Console.WriteLine(" ");
                    }
                    //proses pembacaan tombol
                    ConsoleKeyInfo keyb = Console.ReadKey(true);
                    switch (keyb.Key)
                    {
                        case ConsoleKey.DownArrow: //jika tombol panah bawah
                            Player2++; //player2 turun/bertambah
                            if (Player2 == 12) //jika sudah posisi 12 di kembalikan ke 11 (max 11)
                            {
                                Player2 = 11;
                            }
                            break;
                        case ConsoleKey.UpArrow: //jika tombol panah atas
                            Player2--; //player2 naik/berkurang
                            if (Player2 == 0) //jika sudah posisi 0 kembalikan ke 1 (min 1)
                            {
                                Player2 = 1;
                            }
                            break;
                        case ConsoleKey.S: //tombol turun untuk player1
                            Player1++;
                            if (Player1 == 12)
                            {
                                Player1 = 11;
                            }
                            break;
                        case ConsoleKey.W: //tombol naik untuk player1
                            Player1--;
                            if (Player1 == 0)
                            {
                                Player1 = 1;
                            }
                            break;
                    }
                    //mencetak lagi player1 dan 2 di posisi yang sudah berubah
                    for (int i = Player1; i < Player1 + 4; i++)
                    {
                        Console.SetCursorPosition(5, i); Console.WriteLine("▓");
                    }

                    for (int i = Player2; i < Player2 + 4; i++)
                    {
                        Console.SetCursorPosition(70, i); Console.WriteLine("▓");
                    }

                }

                //menunggu selama beberapa milidetik
                Thread.Sleep(Speed);
                //menghapus posisi bola
                Console.SetCursorPosition(posLeft, posTop); Console.WriteLine(" ");
                //menambahkan posisi bola sehingga bola bergerak
                posLeft = posLeft + leftPlus;
                posTop = posTop + topPlus;
                //jika bola menabrak batas bawah
                if (posTop == 14)
                {
                    topPlus = -1;
                }
                //jika bola menabrak batas atas
                else if (posTop == 1)
                {
                    topPlus = 1;
                }
                //jika bola menabrak batas kanan
                if (posLeft == 69)
                {
                    //check dengan posisi player
                    if (posTop >= Player2 && posTop <= Player2 + 4)
                    {
                        //berbalik arah dan menambah kecepatan
                        leftPlus = -1;
                        Speed = Speed - 10;
                    }
                }
                //jika bola tidak menabrak dan lewat sampai posisi 72
                else if (posLeft == 72)
                {
                    Console.Clear();
                    Console.WriteLine("Game over player 2 modiar");
                    Selesai = true;
                }
                //pengecekan yang sama untuk player1
                if (posLeft == 6)
                {
                    if (posTop >= Player1 && posTop <= Player1 + 4)
                    {
                        leftPlus = 1;
                        Speed = Speed - 10;
                    }
                }
                else if (posLeft == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Game over player 1 modiar");
                    Selesai = true;
                }

                //jika speed suah <10 maka tetep dibuat 10
                if (Speed < 10)
                {
                    Speed = 10;
                }

            }

            Console.Read();
        }
    }
}