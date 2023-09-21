using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player : GameCharacter
    {
        private ConsoleKey key;
        private int shield = Constants.playerBaseShield;
        private int maxShield = Constants.playerBaseShield;
        private InputManager inputManager;
        private ItemManager itemManager;
        private ShopManager shopManager;
        private Exit exit;
        private Hud hud;
        private int XP = 0;
        private int LVL = 1;

        private bool isAttack;
        public bool isTrading; //not used yet

        public Player(Position pos, Map map, EnemyManager enemyManager, Render rend, GameManager manager, InputManager inputManager, ItemManager itemManager, Exit exit, SoundManager soundManager) : base(pos, Constants.playerBaseHP, Constants.playerBaseAttack, Constants.playerSprite, map, enemyManager, rend, manager, soundManager)
        {
            this.inputManager = inputManager;
            this.itemManager = itemManager;
            this.exit = exit;
            isAttack = false;
        }

        public bool getIsAttack()
        {
            return isAttack;
        }

        public void Update()
        {
            isAttack = false;
            key = inputManager.GetKey();                                                                //
            targetPos = pos;
            switch (key)                                                                                //
            {                                                                                           //
                case ConsoleKey.W:                                                                      //
                case ConsoleKey.UpArrow:                                                                //
                    targetPos.y--;                                                                          //
                    break;                                                                              //
                case ConsoleKey.S:                                                                      //
                case ConsoleKey.DownArrow:                                                              //  Pick Direction
                    targetPos.y++;                                                                          //
                    break;                                                                              //
                case ConsoleKey.A:                                                                      //
                case ConsoleKey.LeftArrow:                                                              //
                    targetPos.x--;                                                                          //
                    break;                                                                              //
                case ConsoleKey.D:                                                                      //
                case ConsoleKey.RightArrow:                                                             //
                    targetPos.x++;                                                                          //
                    break;                                                                              //
            }                                                                                           //
            if(map.isFloorAt(targetPos) && enemyManager.EnemyAt(targetPos, false) == null && itemManager.ItemAt(targetPos) == null && shopManager.keeperAt(targetPos) == null)    //
            {                                                                                                                                               //  Move if empty floor
                pos = targetPos;                                                                                                                            //
            }else if (enemyManager.EnemyAt(targetPos, false) != null)    //
            {                                                                   //  Attack enemy in target space
                AttackEnemy(enemyManager.EnemyAt(targetPos, true));      //
            }else if (itemManager.ItemAt(targetPos) != null)                 //
            {                                                                       //  Pick Up item in target space
                itemManager.PickUp(itemManager.ItemAt(targetPos), this);     //
            }else if (shopManager.keeperAt(targetPos) != null){
                //no mova
            }

            exit.isExitAt(targetPos, true);
        }

        public void getShopManager(ShopManager shopManager)
        {
            this.shopManager = shopManager;
        }

        public bool isPlayerAt(Position pos)   //returns true if the provided coordinates are the player's coordinates
        {
            bool check = false;
            if (this.pos == pos) check = true;
            return check;
        }

        public void placePlayer(Position pos)
        {
            this.pos = pos;
        }

        public void AttackEnemy(Enemy enemy) //Attacks the provided enemy and gives the interaction message
        {
            enemy.TakeDMG(ATK);
            isAttack = true;
            if (enemy.GetHealth() > 0) hud.SetMessage("You attacked " + enemy.GetName());
            else hud.SetMessage("You killed " + enemy.GetName());
            if (XP >= Constants.playerXPThreshold)
            {
                LevelUp();
            }
        }

        public override void TakeDMG(int DMG)
        {
            if(shield > DMG)        //
            {                       //
                shield -= DMG;      //  Damages shield if dmg wouldn't destroy it
                base.TakeDMG(0);    //
            }                       //
            else                        //
            {                           //
                DMG -= shield;          //  Shield reduces dmg (if it isn't 0 already), before being destroyed and applying dmg normally
                shield = 0;             //
                base.TakeDMG(DMG);      //
            }                           //
        }

        public void Heal(int heal)  //  Restores HP up to max
        {
            HP += heal;
            if(HP > maxHP)
            {
                HP = maxHP;
            }
        }

        public void RestoreShield(int restore)  //  Restores Shield up to max
        {
            shield += restore;
            if(shield > maxShield)
            {
                shield = maxShield;
            }
        }

        public void RaiseATK(int raise) //  Increase ATK power
        {
            ATK += raise;
        }

        public int GetMaxHP()
        {
            return maxHP;
        }

        public void setHP(int value)
        {
            HP = value;
        }

        public int GetShield()
        {
            return shield;
        }

        public int GetMaxShield()
        {
            return maxShield;
        }

        public int GetLevel()
        {
            return LVL;
        }

        public int GetXP()
        {
            return XP;
        }

        public void LevelUp()
        {
            hud.SetMessage("Player Leveled Up!");
            LVL++;
            maxHP++;
            maxShield++;
            HP++;
            shield++;
            ATK++;
            XP -= Constants.playerXPThreshold;
        }

        public void LevelDown()
        {
            hud.SetMessage("Player's soul was cleansed...");
            LVL = 1;
            maxHP = Constants.playerBaseHP;
            maxShield = Constants.playerBaseShield;
            HP = Constants.playerBaseHP;
            shield = Constants.playerBaseShield;
            ATK = Constants.playerBaseAttack;
            XP = 0;
        }

        public void questLevelUp()
        {
            while (XP >= Constants.playerXPThreshold)
            {
                LevelUp();
            }
        }

        public void giveXP(int reward)
        {
            XP += reward;
        }

        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }

    }
}
