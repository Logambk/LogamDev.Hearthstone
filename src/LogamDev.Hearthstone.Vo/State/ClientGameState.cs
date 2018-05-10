namespace LogamDev.Hearthstone.Vo.State
{
    public class ClientGameState
    {
        public ClientPlayerState Me { get; set; }
        public ClientOpponentState Opp { get; set; }
    }
}
