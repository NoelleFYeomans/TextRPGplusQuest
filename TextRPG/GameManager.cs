using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameManager
    {
        private bool play = true;
        private bool win = false;

        //Define Objects
        static Render render;

        public static MapGenerator mapGen;
        public static Map map;
        public static MiniMap miniMap;

        public static EnemyManager enemyManager;

        static Player player;

        public static ItemManager itemManager;

        public static QuestManager questManager; //manages la questa

        public static ShopManager shopManager;

        static InputManager inputManager;

        public static Hud hud;

        public static Exit exit;

        static Camera cam;

        //private string message;

        public LoadManager loadManager;

        public StartScreen startScreen = new StartScreen();

        public EndScreen endScreen;

        public SoundManager soundManager = new SoundManager();


        public GameManager()
        {
            endScreen = new EndScreen(soundManager);
            render = new Render();
            mapGen = new MapGenerator();
            questManager = new QuestManager();
            render.setGameManager(this);
            inputManager = new InputManager(this);
            map = new Map(mapGen.RandomizeMap(), render);
            exit = new Exit(this, render, map);
            itemManager = new ItemManager(map, render, this, exit, soundManager, questManager);
            enemyManager = new EnemyManager(map, render, itemManager, this, exit, soundManager, questManager);
            player = new Player(new Position((Constants.mapWidth/2) * Constants.roomWidth + (Constants.roomWidth/2), (Constants.mapHeight / 2) * Constants.roomHeight + (Constants.roomHeight / 2)), map, enemyManager, render, this, inputManager, itemManager, exit, soundManager);
            miniMap = new MiniMap(mapGen.makeMiniMap(), player);
            cam = new Camera(player, this);
            hud = new Hud(player, enemyManager, itemManager, this, questManager);
            shopManager = new ShopManager(map, render, itemManager, this, exit, enemyManager, inputManager, soundManager, hud);
            loadManager = new LoadManager(this, render, cam, exit, itemManager, enemyManager, miniMap, player, hud, map, mapGen, questManager, shopManager);
            questManager.setPlayer(player);
            player.getShopManager(shopManager);
            enemyManager.getShopManager(shopManager);
            
        }

        public void Update()
        {
            hud.SetMessage(" ");
            if(player.isAlive() == false)   //
            {                               //  End game if player is dead
                play = false;               //
            }                               //
                                            //
            inputManager.Update();          //
            player.Update(); //this is where the player inputs their turn               //  Update everything
            cam.Update();                   //
            if (Globals.currentFloor < Constants.BossFloor) questManager.update();          //
            enemyManager.UpdateEnemies();   //
            shopManager.UpdateShopkeepers();//this is where the shopkeeper reacts, prompting another input(which waits until player turn) but then because of this the hud message is set back to quest text in the questmanager
            miniMap.Update();               //
        }
        
        public void Play() //this is the game loop
        {
            startScreen.Display();
            loadManager.FloorSetUp();
            while(play == true)
            {
                Update();
                Draw();
            }
            Conclusion(win);
        }

        public void Draw()  //Draw Everything
        {
            //render.ResetBackgrounds();//
            map.DrawMap();              //
            itemManager.Draw();         //  Set chars to arrays in rend
            player.Draw();              //
            enemyManager.DrawEnemies(); //
            shopManager.DrawShopkeep(); //
            exit.Draw();                //
            hud.draw();                 //
            render.DrawToScreen();    //Adds to screen
        }

        public void EndGame(bool win)
        {
            this.win = win;
            play = false;
        }

        public void Conclusion(bool win)
        {
            if (win)
            {
                endScreen.Display(EndScreen.EndCon.Win);
            }
            else
            {
                endScreen.Display(EndScreen.EndCon.Lose);
            }
        }

    }
}
