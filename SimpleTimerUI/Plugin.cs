using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Example_Plugin;
using Player_Ui;

namespace PlayerUI
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "PlayerUI";
        public override string Author => "Python";
        public override Version Version => new Version(1, 0, 0);

        private DateTime roundStartTime;
        private bool roundStarted = false;
        private CoroutineHandle updateCoroutine;

        public static Plugin Instance { get; private set; }

        public override void OnEnabled()
        {
            Instance = this;
            base.OnEnabled();
            Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
            updateCoroutine = Timing.RunCoroutine(UpdateRoutine());
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Timing.KillCoroutines(updateCoroutine);
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
            Instance = null;
        }

        private void OnRoundStarted()
        {
            roundStartTime = DateTime.Now;
            roundStarted = true;
        }

        private IEnumerator<float> UpdateRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);

                if (roundStarted)
                {
                    TimeSpan elapsedTime = DateTime.Now - roundStartTime;
                    string roundTimeFormatted = string.Format("{0:D2}:{1:D2}", elapsedTime.Minutes, elapsedTime.Seconds);

                    foreach (var player in Player.List)
                    {
                        if (player.IsAlive)
                        {
                            player.ShowHint($@"
<size=30><align=center><voffset=-10em><u></u></voffset>\n{roundTimeFormatted}</align></size>", 2.5f);
                        }
                    }
                }
            }
        }
    }
}
