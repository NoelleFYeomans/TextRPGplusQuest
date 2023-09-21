using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class ShopManager
    {
        private List<Shopkeep> keepers = new List<Shopkeep>();
        private Shopkeep[,] shopkeeperMap = new Shopkeep[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];
        private Random random = Constants.rand;
        private Map map;
        private ItemManager itemManager;
        private GameManager gameManager;
        private EnemyManager enemyManager;
        private InputManager inputManager;
        private SoundManager soundManager;
        private LoadManager loadManager;
        private Hud hud;
        private Exit exit;
        private Render rend;

        //private bool toMove;

        public ShopManager(Map map, Render rend, ItemManager itemManager, GameManager manager, Exit exit, EnemyManager enemyManager, InputManager inputManager, SoundManager soundManager, Hud hud)
        {
            this.map = map;
            this.itemManager = itemManager;
            this.gameManager = manager;
            this.enemyManager = enemyManager;
            this.inputManager = inputManager;
            this.soundManager = soundManager;
            this.exit = exit;
            this.rend = rend;
            this.hud = hud;

            //toMove = true;
        }

        public void getLoadManager(LoadManager loadManager)
        {
            this.loadManager = loadManager;
        }

        public void DrawShopkeep()   //Save keeper to rend arrays
        {
            foreach (Shopkeep shopkeep in keepers)
            {
                shopkeep.Draw();
            }
        }

        public void UpdateShopkeepers() //Move each keeper on every other turn
        {
            foreach (Shopkeep shopkeeper in keepers)
            {
                shopkeeperMap[shopkeeper.GetPos().x, shopkeeper.GetPos().y] = null;
                shopkeeper.update();
                shopkeeperMap[shopkeeper.GetPos().x, shopkeeper.GetPos().y] = shopkeeper;
            }
        }

        public void generateShopkeep(Player player) //self explanitory
        {
            ClearShopkeep();
            if (Globals.currentFloor == Constants.BossFloor) return; 
            Position tempPos;
            int placedKeepers = 0;
            while (placedKeepers < Constants.keeperCap)
            {
                tempPos = new Position(random.Next(Constants.mapWidth * Constants.roomWidth), random.Next(Constants.mapHeight * Constants.roomHeight));
                if ((Math.Abs(player.GetPos().x - tempPos.x) > 5 || Math.Abs(player.GetPos().y - tempPos.y) > 5) && map.isFloorAt(tempPos) && itemManager.ItemAt(tempPos) == null && exit.isExitAt(tempPos, false) == false && keeperAt(tempPos) == null)
                {
                    keepers.Add(new Shopkeep(tempPos, map, enemyManager, rend, gameManager, inputManager, itemManager, exit, soundManager, player, hud, loadManager));   
                    placedKeepers++;
                    shopkeeperMap[tempPos.x, tempPos.y] = keepers[placedKeepers - 1];
                }
            }

        }

        public Shopkeep keeperAt(Position pos)    //Returns the keeper at the provided coords.
        {
            Shopkeep foundKeeper = shopkeeperMap[pos.x, pos.y];
            return foundKeeper;
        }

        public void ClearShopkeep() //removes all existing shopkeepers from reality
        {
            foreach (Shopkeep keeper in keepers)
            {
                shopkeeperMap[keeper.GetPos().x, keeper.GetPos().y] = null;
            }
            keepers.Clear();
        }
    }
}
