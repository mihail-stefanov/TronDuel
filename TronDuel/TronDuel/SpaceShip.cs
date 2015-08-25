namespace TronDuel
{
    public class SpaceShip
    {
        public const char ShipCharRight = '►';
        public const char ShipCharLeft = '◄';
        public const char ShipCharUp = '▲';
        public const char ShipCharDown = '▼';

        public SpaceShip(byte startingPositionX, byte startingPositionY)
        {
            this.Xposition = startingPositionX;
            this.Yposition = startingPositionY;
        }

        public byte Xposition { get; set; }
        public byte Yposition { get; set; }
    }
}
