using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class QuestManager //condense the enum pickers maybe, otherwise done!
    {
        //ints needed for quest generation
        private int questCountGoal; //goal for quest
        private int questCountTracker; //trackers for quest
        private int questCountMin; 
        private int questExpReward; 
        
        private int expMultiplier; //based by floor. 2x floor 1, 3x floor 2, 3x floor 3?
        
        private bool isQuestComplete; //is the quest complete?
        private bool isBossFloor;
        private int cFloor;

        Player player;

        Random rand = new Random(); //random used for quest generation
        Hud hud;

        private enum qType //types of quest
        {
            Kill,
            Gather
        }

        private enum iType //types of items
        {
            HealthPotion,
            ShieldRepair
        }

        private enum eType //types of enemies
        {
            Slime,
            Kobold,
            Goblin
        }

        //for quest tracking
        iType questItem;
        eType questEnemy;
        private int qGoal; //quest goal
        private string questType; //string that has current quest type

        public QuestManager() 
        {

            isQuestComplete = false;
            isBossFloor = false;
        }
        public void update()
        {
            if (cFloor >= Constants.BossFloor) isBossFloor = true;

            if (player.getIsAttack()) return;

            if ((!isQuestComplete) && (isBossFloor == false) && (questType == qType.Kill.ToString()))
            {
                hud.SetMessage("Quest: Kill " + (qGoal - questCountTracker) + " " + questEnemy.ToString());
            }
            else if ((!isQuestComplete) && (isBossFloor == false) && (questType == qType.Gather.ToString()))
            {
                hud.SetMessage("Quest: Gather " + (qGoal - questCountTracker) + " " + questItem.ToString());
            }
            else if (isQuestComplete && (isBossFloor == false))
            {
                hud.SetMessage("You completed the quest! +" + questExpReward + " XP!");
            }
        }

        public void SetHud(Hud hud) //hud access
        {
            this.hud = hud;
        }
        public void setPlayer(Player player)
        {
            this.player = player;
        }
        public void tryIncrementQuest(Enemy enemy, Item item)
        {
            if (questType == qType.Kill.ToString())
            {
                if (item != null) return;
                if (enemy.GetName() == questEnemy.ToString())
                {
                    questCountTracker++;//increment quest kill counter

                    if (questCountTracker >= questCountGoal)
                    {
                        isQuestComplete = true;
                        applyQuestRewards();
                    }
                }
            }
            else if (questType == qType.Gather.ToString())
            {
                if (enemy != null) return;
                if (item.GetName() == questItem.ToString()) //this is for the gather quest
                {
                    questCountTracker++;//increment quest counter

                    if (questCountTracker >= questCountGoal)
                    {
                        isQuestComplete = true;
                        applyQuestRewards();
                    }
                }
            }
        }
        private void applyQuestRewards()
        {
            player.giveXP(questExpReward);
            player.questLevelUp();
            questType = "";
        }
        public void generateRandomQuest(int slimeMax, int koboldMax, int goblinMax, int healMax, int ShieldMax, int floor)//create a random quest (do x y) and receive a reward(XP)
        {
            qType chosenQuest = pickQuestType();
            questType = chosenQuest.ToString();

            switch (chosenQuest)
            {
                case qType.Kill: //kill Quest

                    cFloor = floor;
                    resetAllValues(); //resets value before making a new quest

                    eType chosenEnemy = pickEnemy(); //picks random enemy
                    questEnemy = chosenEnemy; //set chosen quest eType for rest of class to access

                    switch (chosenEnemy) //generates quest based on enemy chosen
                    {
                        case eType.Slime:
                            createQuestSpecs(slimeMax, floor);
                            break;
                            ;
                        case eType.Kobold:
                            createQuestSpecs(koboldMax, floor);
                            break;
                        case eType.Goblin:
                            createQuestSpecs(goblinMax, floor);
                            break;
                    }

                    startQuest(questCountGoal);

                    break;

                case qType.Gather: //gather Quest

                    cFloor = floor;
                    resetAllValues(); //resets value before making a new quest

                    iType chosenItem = pickItem();
                    questItem = chosenItem;

                    switch (chosenItem) //generates quest based on enemy chosen
                    {
                        case iType.HealthPotion:
                            createQuestSpecs(healMax, floor);
                            break;
                            ;
                        case iType.ShieldRepair:
                            createQuestSpecs(ShieldMax, floor);
                            break;
                    }

                    startQuest(questCountGoal);

                    break;
            }
        }
        private void startQuest(int goal) //displays a message to player about the start of a quest
        {
            qGoal = goal;
            if (questType == qType.Kill.ToString())
            {
                hud.SetMessage("Quest: Kill " + qGoal + " " + questEnemy);
            }
            else if (questType == qType.Gather.ToString())
            {
                hud.SetMessage("Quest: Gather " + qGoal + " " + questItem);
            }
        }
        private void createQuestSpecs(int max, int floor) //creates the numerical specs of the quest
        {
            expMultiplier = (1 * floor);
            questCountMin = (3 * floor);
            if (questCountMin > max) questCountMin = max; //prevents incompletable quests
            questCountGoal = rand.Next(questCountMin, max);
            questExpReward = (questCountGoal * expMultiplier);
        }
        private eType pickEnemy() //eTypeRandom picks a random enum(enemy) to make a quest out of.
        {
            Array values = Enum.GetValues(typeof(eType));
            Random random = new Random();
            eType eTypeRandom = (eType)values.GetValue(random.Next(values.Length));
            return eTypeRandom;
        }
        private iType pickItem() //eTypeRandom picks a random enum(item) to make a quest out of.
        {
            Array values = Enum.GetValues(typeof(iType));                            
            Random random = new Random();
            iType iTypeRandom = (iType)values.GetValue(random.Next(values.Length));
            return iTypeRandom;
        }
        private qType pickQuestType() //qTypeRandom picks a random quest type
        {
            Array values = Enum.GetValues(typeof(qType));
            Random random = new Random();
            qType qTypeRandom = (qType)values.GetValue(random.Next(values.Length));
            questType = qTypeRandom.ToString();
            return qTypeRandom;
        }
        private void resetAllValues() //resets all relevent values so a new quest can be created.
        {
            questCountGoal = 0;
            questCountTracker = 0;
            questExpReward = 0;
            questCountMin = 0;
            expMultiplier = 0;
            qGoal = 0;
            isQuestComplete = false;
        }
    }
}
