using System.Collections.Generic;
using EXILED;
using EXILED.Extensions;
using MEC;

namespace MuteFixer
{
    class MuteFixer : Plugin
    {
        public override string getName { get; } = "MuteFixer";
        public static readonly string Version = "1.0a";
        private EventHandler eventHandler;

        public override void OnEnable()
        {
            Log.Info($"[OnEnable] Enabled. Version:{Version}");

            eventHandler = new EventHandler();
            Events.PlayerJoinEvent += eventHandler.OnPlayerJoin;
        }
        public override void OnDisable()
        {
            Events.PlayerJoinEvent -= eventHandler.OnPlayerJoin;
            eventHandler = null;

            Log.Info($"[OnDisable] Disabled. Version:{Version}");
        }
        public override void OnReload()
        {
            //Not used
        }
    }

    public class EventHandler
    {
        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            Timing.RunCoroutine(ForceMuteSync());
        }

        private IEnumerator<float> ForceMuteSync()
        {
            yield return Timing.WaitForSeconds(0.25f);
            foreach(var player in Player.GetHubs())
            {
                player.characterClassManager.SetDirtyBit(1ul);
            }
            yield break;
        }
    }
}