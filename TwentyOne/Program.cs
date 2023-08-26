using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

using TwentyOne;

internal class Program
{
    static void Main(string[] args)
    {
        Deck deck = new Deck();

        Player player = new Player();
        Player computer = new Player();

        Console.WriteLine("Welcome to TwentyOne!\n" +
            "Press any key to start...\n");
        Console.ReadKey(intercept : true);

        for (int i = 0; i < 2; ++i)
        {
            player.TakeCard(deck);
            computer.TakeCard(deck);
        }

        Console.WriteLine("Player cards: ");
        player.ShowHand();
        computer.ShowHand();

        while (true)
        {
            Console.Write("Do you want to hit or stand? (h/s): ");
            char response = Console.ReadKey().KeyChar;
            if (response == 'h' || response == 'H')
            {
                player.TakeCard(deck);
                Console.WriteLine("\nPlayer's hand:");
                player.ShowHand();

                if (player.currentPoints > 21)
                {
                    Console.WriteLine("\nBust! You lose.");
                    return;
                }
            }
            else if (response == 's' || response == 'S')
            {
                Console.Write("\n\n");
                break;
            }
        }

        Console.WriteLine("Dealer turn!");
        while (computer.currentPoints < 17)
        {
            Console.WriteLine("Dealer take card...");
            computer.TakeCard(deck);
            computer.ShowHand();
            Thread.Sleep(1500);
        }

        if (player.currentPoints > 21 || (computer.currentPoints <= 21 && computer.currentPoints > player.currentPoints))
        {
            Console.WriteLine("\nDealer wins!");
        }
        else if (computer.currentPoints > 21 || player.currentPoints > computer.currentPoints)
        {
            Console.WriteLine("\nPlayer wins!");
        }
        else
        {
            Console.WriteLine("\nIt's a tie!");
        }

        Console.ReadKey();
    }
}
