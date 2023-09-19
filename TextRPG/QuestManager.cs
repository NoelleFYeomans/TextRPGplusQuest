using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class QuestManager //Add Item Quest! (last step)
    {
        //ints needed for quest generation
        private int questKillGoal; //goal for quest
        private int questKillTracker; //tracks kills for quest
        private int questKillMin; //should be 5?
        private int questExpReward; //genned randomly
        private int expMultiplier; //based by floor. 2x floor 1, 3x floor 2, 3x floor 3?
        private bool isQuestComplete; //is the quest complete?
        private bool isBossFloor;
        private int cFloor;

        Player player;

        Random rand = new Random(); //random used for quest generation
        Hud hud;

        private enum eType
        {
            Slime,
            Kobold,
            Goblin
        }

        ////for kill tracking
        eType questEnemy;
        private int kGoal;

        public QuestManager() //useful if I added multiple quests?
        {

            isQuestComplete = false;
            isBossFloor = false;
        }
        public void update()
        {
            if (cFloor > 2) isBossFloor = true;

            if ((hud.message == " ") && (!isQuestComplete) && (isBossFloor ==  false))
            {
                hud.SetMessage("Quest: Kill " + (kGoal - questKillTracker) + " " + questEnemy.ToString());
            }
            else if ((hud.message == " ") && isQuestComplete && (isBossFloor == false))
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
        public void tryIncrementQuest(Enemy enemy)
        {
            if (enemy.GetName() == questEnemy.ToString())
            {
                questKillTracker++;//increment quest kill counter
                //write to status
                if (questKillTracker >= questKillGoal) 
                {
                    isQuestComplete = true;
                    applyQuestRewards();
                }
            }
        }
        private void applyQuestRewards()
        {
            player.giveXP(questExpReward);
            player.questLevelUp();
        }
        public void generateRandomQuest(int slimeMax, int koboldMax, int goblinMax, int floor)//create a random quest (kill x enemies) and receive a reward(XP) (and maybe check to levelup multiple times, just incase)
        {
            cFloor = floor;
            resetAllValues(); //resets value before making a new quest

            eType chosenEnemy = pickEnemy(); //picks random enemy
            questEnemy = chosenEnemy; //set chosen quest eType for rest of class to access

            switch  (chosenEnemy) //generates quest based on enemy chosen
            {
                case eType.Slime:
                    createQuestSpecs(slimeMax, floor);
                    break;
;               case eType.Kobold:
                    createQuestSpecs(koboldMax, floor);
                    break;
                case eType.Goblin:
                    createQuestSpecs(goblinMax, floor);
                    break;
            }

            startQuest(questKillGoal, chosenEnemy);
        }
        private void startQuest(int goal, eType chosen) //displays a message to player about the start of a quest
        {
            kGoal = goal;
            hud.SetMessage("Quest: Kill " + goal + " " + chosen.ToString());
        }
        private void createQuestSpecs(int max, int floor) //creates the numerical specs of the quest
        {
            expMultiplier = (1 * floor);
            questKillMin = (3 * floor);
            if (questKillMin > max) questKillMin = max; //prevents incompletable quests
            questKillGoal = rand.Next(questKillMin, max);
            questExpReward = (questKillGoal * expMultiplier);
        }
        private eType pickEnemy() //eTypeRandom picks a random enum(enemy) to make a quest out of.
        {
            Array values = Enum.GetValues(typeof(eType));
            Random random = new Random();
            eType eTypeRandom = (eType)values.GetValue(random.Next(values.Length));
            return eTypeRandom;
        }
        private void resetAllValues() //resets all relevent values so a new quest can be created.
        {
            questKillGoal = 0;
            questKillTracker = 0;
            questExpReward = 0;
            questKillMin = 0;
            expMultiplier = 0;
            isQuestComplete = false;
        }



    }
}
