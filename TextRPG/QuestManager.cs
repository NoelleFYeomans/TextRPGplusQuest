using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class QuestManager
    {
        //ints needed for quest generation
        private int questKillGoal; //goal for quest
        private int questKillTracker; //tracks kills for quest
        private int questKillMin; //should be 5?
        private int questExpReward; //genned randomly
        private int expMultiplier; //based by floor. 2x floor 1, 3x floor 2, 3x floor 3?

        Random rand = new Random(); //random used for quest generation

        Hud hud;

        private enum eType
        {
            Slimes,
            Kobolds,
            Goblins
        }

        public QuestManager(Hud hud) //useful if I added multiple quests?
        {
            this.hud = hud; //gives access to the HUD
        }

        public void generateRandomQuest(int slimeMax, int koboldMax, int goblinMax, int floor)//create a random quest (kill x enemies) and receive a reward(XP) (and maybe check to levelup multiple times, just incase)
        {
            resetAllValues(); //rese value before making a new quest

            eType chosenEnemy = pickEnemy(); //picks random enemy

            switch  (chosenEnemy) //generates quest based on enemy chosen
            {
                case eType.Slimes:
                    createQuestSpecs(slimeMax, floor);
                    break;
;               case eType.Kobolds:
                    createQuestSpecs(koboldMax, floor);
                    break;
                case eType.Goblins:
                    createQuestSpecs(goblinMax, floor);
                    break;
            }

            startQuest(questKillGoal, chosenEnemy);
        }

        private void startQuest(float goal, eType chosen) //displays a message to player about the start of a quest
        {
            hud.SetMessage("Quest: Kill " + goal + " " + chosen.ToString()); //maybe keep a live kill tracker in the updoot?
            //start tracking kills
        }
        private void questComplete() //used when a quest is completed
        {
            hud.SetMessage("Quest Complete! Reward: " + questExpReward + " XP"); //only display once
            //levelup check
        }
        private void update()
        {
            //updates when killing an enemy
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
        }



    }
}
