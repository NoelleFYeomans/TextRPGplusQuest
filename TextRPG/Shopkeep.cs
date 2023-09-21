using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shopkeep : GameCharacter //***TODO LIST*** Add 2 more trades(almost done!)
    {
        private Player player;
        private ItemManager itemManager;
        private InputManager inputManager;
        private LoadManager loadManager;
        private Exit exit;
        private Hud hud;

        private bool trading;
        public bool ascend;
        public bool penant;
        private int bloodPaid;
        //private List<string> shopDialogue;

        private enum tradeType
        {
            power,
            passage,
            pennance,
            
        }

        tradeType currentType;

        public Shopkeep(Position pos, Map map, EnemyManager enemyManager, Render rend, GameManager manager, InputManager inputManager, ItemManager itemManager, Exit exit, SoundManager soundManager, Player player, Hud hud, LoadManager loadManager) : base(pos, Constants.shopkeepBaseHP, Constants.shopkeepBaseAttack, Constants.shopkeepSprite, map, enemyManager, rend, manager, soundManager) 
        {
            this.itemManager = itemManager;
            this.inputManager = inputManager;
            this.loadManager = loadManager;
            this.exit = exit;
            this.player = player;
            this.hud = hud;

            trading = false;
            ascend = false;
            penant = false;
            currentType = tradeType.power;
        }
        public void tradeStart()
        {
            if (currentType == tradeType.power)
            {
                hud.SetMessage("Ye seeketh blood power? Y/N"); //accept blood for exp trade
                tradeMethod();
            }
            else if (currentType == tradeType.passage)
            {
                hud.SetMessage("Ye seeketh safe passage? Y/N"); //accept blood & shield for instant passage to next floor
                tradeMethod();
            }
            else if (currentType == tradeType.pennance)
            {
                hud.SetMessage("Ye seeketh soul pennance? Y/N"); //accept ???
                tradeMethod();
            }
            else
            {
                currentType = tradeType.power;
                hud.SetMessage("Begone desireless soul."); //accept no trade
                trading = false;
            }
        }
        private void tradeMethod()
        {
            switch (inputManager.GetKey())
            {
                case ConsoleKey.Y:
                    shopTrade(currentType);
                    break;
                case ConsoleKey.N:
                    noTrade();
                    break;
            }
        }
        public void update()
        {
            ascend = false;
            if (alive && (player.GetPos().x == (pos.x - 1)) && (player.GetPos().y == pos.y) || (player.GetPos().x == (pos.x + 1)) && (player.GetPos().y == pos.y) || (player.GetPos().x == pos.x) && (player.GetPos().y == (pos.y - 1)) || (player.GetPos().x == pos.x) && (player.GetPos().y == (pos.y + 1)) || (player.GetPos() == pos)) 
            {
                trading = true; //if the player is adjacent, trading starts, requires shopkeep to update *after* player
            } //first thing the shopkeep needs to do 
            else
            {
                trading = false;
            }

            if (alive && !trading)
            {                                                                                                                               
                randomMove(); 
            }
            else if (alive && trading)
            {
                tradeStart(); 
            }
            else
            {
                //do nothing
            }
        }
        private void shopTrade(tradeType tType)
        {
            if (tType == tradeType.power)
            {
                bloodPaid = (player.GetHealth() - 1);
                player.giveXP(bloodPaid * Constants.powerOfBlood);
                while (player.GetXP() >= Constants.playerXPThreshold) player.LevelUp();
                player.setHP(1); //sets HP to 1
                hud.SetMessage("Be careful..."); //end of trade 1
                trading = false;
            }
            else if (tType == tradeType.passage)
            {
                player.TakeDMG((player.GetShield() + player.GetHealth() - 1));
                hud.SetMessage("Ascend the towering heights."); //end of trade 2
                trading = false;
                ascend = true;
            }
            else if (tType == tradeType.pennance)
            {
                player.LevelDown();
                //do something positive xd, maybe special win screen? maybe fully map the current floor
                hud.SetMessage("Wash your hands of the blood."); //end of trade 3
                trading = false;
                penant = true;
            }
            else
            {
                //future use?
            }

        }
        public void noTrade()
        {
            if (currentType == tradeType.pennance)
            {
                hud.SetMessage("Begone desireless soul.");
                currentType = tradeType.power;
            }
            else hud.SetMessage("Unfortunate...");
            currentType++;

            trading = false;
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
            if (exit.isExitAt(pos, false))
                available = false;

            return available;

        }
        protected void randomMove()
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
            if (IsSpaceAvailable(targetPos))
            {
                pos = targetPos;
            }
        }
    }
}
