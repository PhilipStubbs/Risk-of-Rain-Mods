using BepInEx;
using RoR2;
using UnityEngine;

namespace BetterItemRewards
{
    [BepInDependency("com.bepis.r2api")]

    [BepInPlugin("com.CaptainPudding.BetterItemRewards", "BetterItemRewards", "1.0.0")]
    public class BetterItemRewards : BaseUnityPlugin
    {
        public void Awake()
        {
            Chat.AddMessage("betterItemRewards v1.0.0 Loaded!");

            On.RoR2.GlobalEventManager.OnTeamLevelUp += (orig, self) =>
            {
                orig(self);
                float timeLimit = 5.00f * 60f;
                int connectedPlayers = PlayerCharacterMasterController.instances.Count;
                var currentTime = RoR2.Run.instance.time;

                for (int i = 0; i < connectedPlayers; i++)
                {
                    var character = PlayerCharacterMasterController.instances[i].master;
                    if (character.alive)
                    {
                        int randomIndex = randomIndex = Random.Range(0, 1);

                          if (currentTime >= timeLimit * 4f)
                        {
                            randomIndex = Random.Range(0, 4);
                        }  else if(currentTime >= timeLimit * 2f)
                        {
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