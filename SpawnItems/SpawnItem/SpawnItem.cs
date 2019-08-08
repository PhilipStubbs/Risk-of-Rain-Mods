using BepInEx;
using RoR2;
using UnityEngine;

namespace SpawnItems
{
    //This is an example plugin that can be put in BepInEx/plugins/ExamplePlugin/ExamplePlugin.dll to test out.
    //It's a very simple plugin that adds Bandit to the game, and gives you a tier 3 item whenever you press F2.
    //Lets examine what each line of code is for:

    //This attribute specifies that we have a dependency on R2API, as we're using it to add Bandit to the game.
    //You don't need this if you're not using R2API in your plugin, it's just to tell BepInEx to initialize R2API before this plugin so it's safe to use R2API.
    [BepInDependency("com.bepis.r2api")]

    //This attribute is required, and lists metadata for your plugin.
    //The GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config). I like to use the java package notation, which is "com.[your name here].[your plugin name here]"
    //The name is the name of the plugin that's displayed on load, and the version number just specifies what version the plugin is.
    [BepInPlugin("com.SpawnItems.SpawnItems", "SpawnItems", "1.0")]

    //This is the main declaration of our plugin class. BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    //BaseUnityPlugin itself inherits from MonoBehaviour, so you can use this as a reference for what you can declare and use in your plugin class: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    public class ExamplePlugin : BaseUnityPlugin
    {
        //The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
        
        }

        public void spawnTier1Item(float offSet) {
            var dropList = Run.instance.availableTier1DropList;

             var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

             var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;
            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

        public void spawnTier2Item(float offSet)
        {
            var dropList = Run.instance.availableTier2DropList;

            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

        public void spawnTier3Item(float offSet)
        {
            var dropList = Run.instance.availableTier3DropList;

            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }


        public void spawnLunarItem(float offSet)
        {
            //We grab a list of all available Tier 3 drops:
            var dropList = Run.instance.availableLunarDropList;

            //Randomly get the next item:
            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            //Get the player body to use a position:
            var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

            //And then finally drop it infront of the player.
            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

        public void spawnEquipment(float offSet)
        {
            //We grab a list of all available Tier 3 drops:
            var dropList = Run.instance.availableEquipmentDropList;

            //Randomly get the next item:
            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            //Get the player body to use a position:
            var transform = PlayerCharacterMasterController.instances[0].master.GetBodyObject().transform;

            //And then finally drop it infront of the player.
            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }



        public void Update()
        {
            //This if statement checks if the player has currently pressed F2, and then proceeds into the statement:
            if (Input.GetKeyDown(KeyCode.F3))
            {
               spawnTier3Item(1);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                spawnTier2Item(1);
            }
            if (Input.GetKeyDown(KeyCode.F1))
            {
                spawnTier1Item(1);
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                spawnLunarItem(1);
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                spawnEquipment(1);
            }



            if (Input.GetKeyDown(KeyCode.F9))
            {
                int amount = 1;

                while(amount <= 11)
                {
                    spawnTier1Item(amount++);
                }
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                int amount = 1;

                while (amount <= 11)
                {
                    spawnTier2Item(amount++);

                }
            }
            if (Input.GetKeyDown(KeyCode.F11))
            {
                int amount = 1;

                while (amount <= 11)
                {
                    spawnTier3Item(amount++);

                }
            }
            if (Input.GetKeyDown(KeyCode.F12))
            {
                int amount = 1;

                while (amount <= 11)
                {
                    spawnLunarItem(amount++);
                }
            }
        }
    }
}