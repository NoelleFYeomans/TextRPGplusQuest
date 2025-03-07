﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Slime : Enemy
    {

        public Slime(Position pos, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager gameManager, Hud hud, Exit exit, SoundManager soundManager, QuestManager questManager, ShopManager shopManager) : base(pos, Constants.slimeBaseHP, Constants.slimeBaseAttack, Constants.slimeSprite, Constants.slimeName, map, player, enemyManager, itemManager, rend, gameManager, hud, exit, Constants.slimeXP, soundManager, questManager, shopManager)
        {

        }

        public override void Update()
        {
            if (alive)
            {

                if (player.isPlayerAt(new Position(pos.x, pos.y - 1)) || player.isPlayerAt(new Position(pos.x, pos.y + 1)) || player.isPlayerAt(new Position(pos.x - 1, pos.y)) || player.isPlayerAt(new Position(pos.x + 1, pos.y)))       //
                {                                                                                                                                   //
                    AttackPlayer(player);                                                                                                           //  Enemy uses turn to attack player if they're adjacent
                }                                                                                                                                   //
                else                //
                {                   //  Move in a random direction if hasn't attacked
                    RandomMove();   //
                }                   //

            }
        }
    }
}
