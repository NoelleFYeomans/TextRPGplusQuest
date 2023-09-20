using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shopkeep : GameCharacter //trade is broken(something to do with key being overwriten every frame) ***TODO LIST*** Add 2 more trades(simple)
    {
        private Player player;
        private ItemManager itemManager;
        private InputManager inputManager;
        private Exit exit;
        private Hud hud;

        private ConsoleKey key;

        private bool trading;
        private int bloodPaid;

        public Shopkeep(Position pos, Map map, EnemyManager enemyManager, Render rend, GameManager manager, InputManager inputManager, ItemManager itemManager, Exit exit, SoundManager soundManager, Player player, Hud hud) : base(pos, Constants.shopkeepBaseHP, Constants.shopkeepBaseAttack, Constants.shopkeepSprite, map, enemyManager, rend, manager, soundManager) 
        {
            this.itemManager = itemManager;
            this.inputManager = inputManager;
            this.exit = exit;
            this.player = player;
            this.hud = hud;

            trading = false;
        }
        public void tradeStart()
        {
            hud.SetMessage("Give Blood, Receive Power. Y/N");
            Console.ReadKey(true);

            trading = true; //will be used to prevent player movement, only Y/N

            key = inputManager.GetKey(); //Need to stop overwriting GetKey every frame

            switch (key)
            {
                case ConsoleKey.Y: //this system will only recognize the last input before the interaction started
                    bloodTrade();
                    break;
                case ConsoleKey.N:
                    //check next option
                    noTrade();
                    break;
            }
        }
        public void update()
        {
            if ((player.GetPos().x == (pos.x - 1)) && (player.GetPos().y == pos.y) || (player.GetPos().x == (pos.x + 1)) && (player.GetPos().y == pos.y) || (player.GetPos().x == pos.x) && (player.GetPos().y == (pos.y - 1)) || (player.GetPos().x == pos.x) && (player.GetPos().y == (pos.y + 1))) 
            {
                trading = true;

            } 
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
                //lmfao
            }
        }
        public void bloodTrade()  //first trade
        {
            bloodPaid = (player.GetHealth() - 1);

            player.TakeDMG(player.GetHealth() - 1);

            player.giveXP(bloodPaid * Constants.powerOfBlood);

            while (player.GetXP() >= Constants.playerXPThreshold)
            {
                player.LevelUp();
            }

            trading = false;

            player.setHP(1); //ensures that player HP is 1 after the trade, counters the benefits of gaining HP on levelup
        }
        public void noTrade()
        {
            hud.SetMessage("Unfortunate.");
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
