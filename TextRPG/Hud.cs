﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Hud
    {
        private Player player;
        private EnemyManager enemyManager;
        private Enemy enemy;
        public string message;
        private int x;
        private int y;

        public Hud(Player player, EnemyManager enemyManager, int x, int y)
        {
            this.player = player;
            this.enemyManager = enemyManager;
            this.x = x;
            this.y = y;
        }

        public void draw()
        {
            Console.ResetColor();
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("                                   ");

            }


            Console.SetCursorPosition(x, y);
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║                                 ║");
            Console.WriteLine("╚═════════════════════════════════╝");

            if(message != null)
            {
                Console.SetCursorPosition(x + 1, y + 1);
                Console.Write(message);
            }

            Console.SetCursorPosition(x, y+3);
            Console.WriteLine("╔════════════════╦════════════════╗");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("║                ║                ║");
            }
            Console.WriteLine("╚════════════════╩════════════════╝");


            Console.SetCursorPosition(x + 1, y + 4);
            Console.Write("Player");
            Console.SetCursorPosition(x + 1, y + 5);
            Console.Write("HP: " + player.HP + "/" + player.maxHP);
            Console.SetCursorPosition(x + 1, y + 6);
            Console.Write("Shield: " + player.shield + "/" + player.maxShield);
            Console.SetCursorPosition(x + 1, y + 7);
            Console.Write("ATK: " + player.ATK);


            if (enemyManager.lastAttacked != null)
            {
                enemy = enemyManager.lastAttacked;
                Console.SetCursorPosition(x + 18, y + 4);
                Console.Write(enemy.name);
                Console.SetCursorPosition(x + 18, y + 5);
                Console.Write("HP: " + enemy.HP + "/" + enemy.maxHP);
                Console.SetCursorPosition(x + 18, y + 6);
                Console.Write("ATK: " + enemy.ATK);
            }
        }

    }
}
