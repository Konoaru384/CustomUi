using Exiled.API.Features;
using Player_Ui;
namespace Example_Plugin
{
    public class EventHandlers : Plugin<Config>
    {

        public override void OnEnabled()
        {
            base.OnEnabled();
            Exiled.Events.Handlers.Server.RoundStarted += CassieMessage;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            Exiled.Events.Handlers.Server.RoundStarted -= CassieMessage;
        }

        private void CassieMessage()
        {
            Cassie.MessageTranslated("", "", isHeld: false, isNoisy: false, isSubtitles: true); //Dans la premiere "" vous mettez le message cassie qui est envoie mais pas affichier dans la deuxième "" vous mettez le message cassie
            Map.TurnOffAllLights(4); //Pour éteindre les lumiére pendant 4Seconde
        }
    }
}