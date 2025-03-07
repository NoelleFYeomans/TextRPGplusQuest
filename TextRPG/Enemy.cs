﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    abstract class Enemy : GameCharacter
    {
        protected Player player;

        //protected bool moved;

        protected string name;

        protected int sightRange = Constants.EnemySightRange;

        protected ItemManager itemManager;

        protected Hud hud;

        protected Exit exit;

        protected int XPReward;

        protected QuestManager questManager;

        protected ShopManager shopManager;
        

        public Enemy(Position pos, int HP, int ATK, Tile sprite, string name, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager manager, Hud hud, Exit exit, int XPReward, SoundManager soundManager, QuestManager questManager, ShopManager shopManager) : base(pos, HP, ATK, sprite, map, enemyManager, rend, manager, soundManager)
        {
            this.name = name;
            this.player = player;
            this.itemManager = itemManager;
            this.hud = hud;
            this.exit = exit;
            this.XPReward = XPReward;
            this.questManager = questManager;
            this.shopManager = shopManager;
        }

        public abstract void Update();

        protected void RandomMove()
        {
            targetPos = pos;
            bool moved = false;
            int checks = 0;

            int dir = Constants.rand.Next(4);

            while (moved == false)
            {
                checks++;
                if (checks >= 4)
                    moved = true;
                switch (dir)
                {
                    case 0:
                        if (IsSpaceAvailable(new Position(targetPos.x, targetPos.y - 1)))
                        {
                            targetPos.y--;
                            moved = true;
                        }
                        else
                            dir++;
                        break;
                    case 1:
                        if (IsSpaceAvailable(new Position(targetPos.x, targetPos.y + 1)))
                        {
                            targetPos.y++;
                            moved = true;
                        }
                        else
                            dir++;
                        break;
                    case 2:
                        if (IsSpaceAvailable(new Position(targetPos.x - 1, targetPos.y)))
                        {
                            targetPos.x--;
                            moved = true;
                        }
                        else
                            dir++;
                        break;
                    case 3:
                        if (IsSpaceAvailable(new Position(targetPos.x + 1, targetPos.y)))
                        {
                            targetPos.x++;
                            moved = true;
                        }
                        else
                            dir = 0;
                        break;
                }
            }
            if(IsSpaceAvailable(targetPos))
            {
                pos = targetPos;
            }
        }

        public override void TakeDMG(int DMG)
        {
            base.TakeDMG(DMG);
            if (HP <= 0)
            {
                questManager.tryIncrementQuest(this, null);
                enemyManager.RemoveEnemy(this);
                player.giveXP(XPReward);
            }
        }

        public void AttackPlayer(Player player) 
        {
            player.TakeDMG(ATK);

            if (player.GetHealth() <= 0) hud.SetMessage(name + " killed Player");

            else if ((player.GetHealth() > 0) && (player.getIsAttack())) hud.SetMessage("Player and " + name + " both attacked");

            else if ((player.GetHealth() > 0) && (!player.getIsAttack())) hud.SetMessage(name + " attacked Player");

            else hud.SetMessage("Attack state error");


        }

        public bool CanSeePlayer()
        {
            bool check = false;

            int a2 = (player.GetPos().x - pos.x);
            a2 = a2 * a2;

            int b2 = (player.GetPos().y - pos.y);
            b2 = b2 * b2;

            int c = (int)Math.Sqrt(a2+b2);

            if (c <= sightRange)
            {
                check = true;
            }

            return check;
        }

        public int GetXP()
        {
            return XPReward;
        }

        public bool IsSpaceAvailable(Position pos)
        {
            bool available = true;
            if (map.isFloorAt(pos) == false)
                available = false;
            if (enemyManager.EnemyAt(pos, false) != null)
                available = false;
            if (itemManager.ItemAt(pos) != null)
                available = false;
            if (player.isPlayerAt(pos))
                available = false;
            if (shopManager.keeperAt(pos) != null)
                available = false;
            if(exit.isExitAt(pos, false))
                available = false;

            return available;

        }

        public string GetName()
        {
            return name;
        }

    }
}
