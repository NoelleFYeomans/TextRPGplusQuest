﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EnemyManager
    {
        private List<Enemy> enemies = new List<Enemy>();
        private Enemy[,] enemyMap = new Enemy[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];
        private Map map;
        private Random random = Constants.rand;
        private bool toMove;
        private Render rend;
        private ItemManager itemManager;
        private Enemy lastAttacked;
        GameManager manager;
        Exit exit;
        private Hud hud;
        private SoundManager soundManager;
        private QuestManager questManager;
        private ShopManager shopManager;

        private int slimeCount; //int for counting slimes
        private int koboldCount; //int for counting kobolds
        private int goblinCount; //int for counding goblins
        

        public int getSlimeCount() //allowing questManager to access this info
        {
            return slimeCount;
        }
        public int getKoboldCount()
        {
            return koboldCount;
        }
        public int getGoblinCount()
        {
            return goblinCount;
        }

        private void resetCount() //resets count for all enemies
        {
            slimeCount = 0;
            koboldCount = 0;
            goblinCount = 0;
        }

        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }

        public EnemyManager(Map map, Render rend, ItemManager itemManager, GameManager manager, Exit exit, SoundManager soundManager, QuestManager questManager)
        {
            this.map = map;
            this.rend = rend;
            this.itemManager = itemManager;
            this.manager = manager;
            this.exit = exit;
            this.soundManager = soundManager;
            this.questManager = questManager;
        }

        public void getShopManager(ShopManager shopManager)
        {
            this.shopManager = shopManager;
        }

        public void GenerateEnemies(Player player)
        {
            Position tempPos;
            ClearEnemies();
            resetCount();
            int placedEnemies = 0;
            while(placedEnemies < Constants.EnemyAmount)
            {
                //Console.WriteLine("Attempt(Enemy)");
                tempPos = new Position(random.Next(Constants.mapWidth * Constants.roomWidth), random.Next(Constants.mapHeight * Constants.roomHeight));
                if((Math.Abs(player.GetPos().x - tempPos.x) > 5 || Math.Abs(player.GetPos().y - tempPos.y) > 5) && map.isFloorAt(tempPos) && itemManager.ItemAt(tempPos) == null && exit.isExitAt(tempPos, false) == false && EnemyAt(tempPos, false) == null)
                {
                    switch (random.Next(5))
                    {
                        case 0:
                        case 1:
                            enemies.Add(new Slime(tempPos, map, player, this, itemManager, rend, manager, hud, exit, soundManager, questManager, shopManager));
                            slimeCount++;
                            placedEnemies++;
                            enemyMap[tempPos.x, tempPos.y] = enemies[placedEnemies - 1];
                            break;
                        case 2:
                        case 3:
                            enemies.Add(new Kobold(tempPos, map, player, this, itemManager, rend, manager, hud, exit, soundManager, questManager, shopManager));
                            koboldCount++;
                            placedEnemies++;
                            enemyMap[tempPos.x, tempPos.y] = enemies[placedEnemies - 1];
                            break;
                        case 4:
                            enemies.Add(new Goblin(tempPos, map, player, this, itemManager, rend, manager, hud, exit, soundManager, questManager, shopManager));
                            goblinCount++;
                            placedEnemies++;
                            enemyMap[tempPos.x, tempPos.y] = enemies[placedEnemies - 1];
                            break;
                    }
                }
            }

        }

        public void GenerateBoss(Player player)
        {
            Position tempPos;
            ClearEnemies();
            bool placedBoss = false;
            while (placedBoss == false)
            {
                //Console.WriteLine("Attempt(Enemy)");\
                tempPos = new Position(random.Next(Constants.BossRoomWidth), random.Next(Constants.BossRoomHeight));
                if ((Math.Abs(player.GetPos().x - tempPos.x) > 2 || Math.Abs(player.GetPos().y - tempPos.y) > 2) && map.isFloorAt(tempPos) && itemManager.ItemAt(tempPos) == null && exit.isExitAt(tempPos, false) == false && EnemyAt(tempPos, false) == null)
                {
                    enemies.Add(new Boss(tempPos, map, player, this, itemManager, rend, manager, hud, exit, soundManager, questManager, shopManager));
                    placedBoss = true;
                    enemyMap[tempPos.x, tempPos.y] = enemies[0];
                }
            }
        }

        public void UpdateEnemies() //Move each enemy on every other turn
        {
            if (toMove)
            {
                foreach(Enemy enemy in enemies)
                {
                    enemyMap[enemy.GetPos().x, enemy.GetPos().y] = null;
                    enemy.Update();
                    enemyMap[enemy.GetPos().x, enemy.GetPos().y] = enemy;
                }
            }
            toMove = !toMove;
        }

        public Enemy EnemyAt(Position pos, bool isAttack)    //Returns the enemy at the provided coords. Saves lastAttacked enemy if attacking
        {
            Enemy foundEnemy = enemyMap[pos.x, pos.y];
            if (isAttack && foundEnemy != null) lastAttacked = foundEnemy;
            return foundEnemy;
        }

        public void DrawEnemies()   //Save enemies to rend arrays
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw();
            }
        }


        public void RemoveEnemy(Enemy enemy)    //Removes enemy from enemies array
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
                enemyMap[enemy.GetPos().x, enemy.GetPos().y] = null;
            }
        }

        public int GetEnemyCount()
        {
            return enemies.Count();
        }

        public Enemy GetLastAttacked()
        {
            return lastAttacked;
        }

        public void ClearEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemyMap[enemy.GetPos().x, enemy.GetPos().y] = null;
            }
            enemies.Clear();
        }

    }
}
