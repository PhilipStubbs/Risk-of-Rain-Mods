using BepInEx;
using BepInEx.Configuration;
using RoR2;
using UnityEngine;

namespace BetterItemRewards
{
    [BepInDependency("com.bepis.r2api")]

    [BepInPlugin("com.CaptainPudding.BetterItemRewards", "BetterItemRewards", "1.0.0")]
    
    public class BetterItemRewards : BaseUnityPlugin
    {
 
        public static ConfigWrapper<int> equipmentTimeLimit { get; set; }
        public static ConfigWrapper<int> tierTwoTimeLimit { get; set; }
        public static ConfigWrapper<int> tierThreeTimeLimit { get; set; }
        public void Awake()
        {
            Chat.AddMessage("betterItemRewards v1.0.0 Loaded!");
            initConfig();

            On.RoR2.GlobalEventManager.OnTeamLevelUp += (orig, self) =>
            {
                orig(self);
              //  float timeLimit = equipmentTimeLimit.Value * 60;
                int connectedPlayers = PlayerCharacterMasterController.instances.Count;
                var currentTime = RoR2.Run.instance.time;

                for (int i = 0; i < connectedPlayers; i++)
                {
                    var character = PlayerCharacterMasterController.instances[i].master;
                    if (character.alive)
                    {
                        
                        int randomIndex = 0;
                            if (currentTime >= tierThreeTimeLimit.Value * 60f)
                        {
                            // Possible to spawn Tier 3 items
                            // Range(0,4) will return 0 - 3
                            randomIndex = Random.Range(0, 4);
                        }  else if(currentTime >= tierTwoTimeLimit.Value * 60f)
                        {
                            // Possible to spawn Tier 2 items
                            // Range(0,3) will return 0 - 2
                            randomIndex = Random.Range(0, 3);
                        }
                           else if (currentTime >= equipmentTimeLimit.Value * 60f)
                        {
                            // Possible to spawn Equipment
                            // Range(0,2) will return 0 - 1
                            randomIndex = Random.Range(0, 2);
                        }

                        switch (randomIndex)
                        {
                            case 0:
                                spawnTier1Item(1, character.GetBodyObject().transform);
                                break;
                            case 1:
                                spawnEquipment(0, character.GetBodyObject().transform);
                                break;
                            case 2:
                                spawnTier2Item(0, character.GetBodyObject().transform);
                                break;
                            case 3:
                                spawnTier3Item(0, character.GetBodyObject().transform);
                                break;
                            default:
                                spawnTier1Item(0, character.GetBodyObject().transform);
                                break;
                        }
                    }
                }
            };
        }
   
        public void initConfig()
        {
            tierThreeTimeLimit = Config.Wrap<int>(
                "BetterItemRewards time limits",
                "Tier Three Time Limit",
                "Upto and including Tier three will start appearing after this time limit. IN MINUTES. (tier one, tier two, tier three, equipment)",
                30
            );

            tierTwoTimeLimit = Config.Wrap<int>(
               "BetterItemRewards time limits",
               "Tier Two Time Limit",
               "Upto and including Tier two will start appearing after this time limit. IN MINUTES. (tier one, tier two, equipment)",
               10
           );

            equipmentTimeLimit = Config.Wrap<int>(
              "BetterItemRewards time limits",
              "Equipment Time Limit",
              "Upto and including equipment will start appearing after this time limit. IN MINUTES. (tier one, equipment)",
              5
          );
        }

        public void spawnTier1Item(float offSet, Transform transform)
        {
            var dropList = Run.instance.availableTier1DropList;

             var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

        public void spawnTier2Item(float offSet, Transform transform)
        {
            var dropList = Run.instance.availableTier2DropList;

            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

        public void spawnTier3Item(float offSet, Transform transform)
        {
            var dropList = Run.instance.availableTier3DropList;

            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

        public void spawnLunarItem(float offSet, Transform transform)
        {
            var dropList = Run.instance.availableLunarDropList;

            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

        public void spawnEquipment(float offSet, Transform transform)
        {
            var dropList = Run.instance.availableEquipmentDropList;

            var nextItem = Run.instance.treasureRng.RangeInt(0, dropList.Count);

            PickupDropletController.CreatePickupDroplet(dropList[nextItem], transform.position, transform.forward * (20f + offSet));
        }

    public void Update()
        {

        }
    }
}