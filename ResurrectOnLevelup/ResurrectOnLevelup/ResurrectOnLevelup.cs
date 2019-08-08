using BepInEx;
using RoR2;
using UnityEngine;

namespace ResurrectOnLevelup
{

    [BepInDependency("com.bepis.r2api")]


    [BepInPlugin("com.CaptainPudding.ResurrectOnLevelup", "ResurrectOnLevelup", "1.0")]


    public class ResurrectOnLevelup : BaseUnityPlugin
    {
        public void Awake()
        {
            Chat.AddMessage("ResurrectOnLevelup Loaded!");

            On.RoR2.GlobalEventManager.OnTeamLevelUp += (orig, self) =>
            {
                orig(self);
                bool groupRevive = true;

                int connectedPlayers = PlayerCharacterMasterController.instances.Count;
                int index = 0;
                CharacterMaster[] deadPlayers = new CharacterMaster[connectedPlayers];
                for (int i = 0; i < connectedPlayers; i++)
                {
                    CharacterMaster character = PlayerCharacterMasterController.instances[i].master;
                    if (!character.alive && !groupRevive)
                    {
                        deadPlayers[index] = character;
                        index++;
                    } else if (!character.alive && groupRevive)
                    {
                        character.Respawn(character.GetBody().footPosition, character.GetBody().transform.rotation);
                    }
                }
                int luckyPlayerIndex = Run.instance.stageRng.RangeInt(0, index);
                if (!groupRevive)
                {
                    CharacterMaster luckyPlayer = deadPlayers[luckyPlayerIndex];
                    luckyPlayer.Respawn(luckyPlayer.GetBody().footPosition, luckyPlayer.GetBody().transform.rotation);
                } 
                
            };
        }

        public void Update()
        {

        }
    }
}