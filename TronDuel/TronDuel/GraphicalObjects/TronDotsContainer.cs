namespace TronDuel.GraphicalObjects
{
    using System;
    using System.Collections.Generic;
    using TronDuel.MovingObjects.GraphicalObjects;

    public class TronDotsContainer
    {
        private byte dotCapacity = 100;
        private byte dotsAlreadyAdded = 0;
        private bool capacityReached = false;
        private List<TronDot> dots = new List<TronDot>();
        private SpaceShip baseSpaceShip;

        public TronDotsContainer(SpaceShip baseSpaceShip)
        {
            this.baseSpaceShip = baseSpaceShip;
        }

        public List<TronDot> Dots
        {
            get
            {
                return this.dots;
            }
        }

        public void AddDot()
        {
            this.dots.Add(new TronDot((byte)baseSpaceShip.Xposition, (byte)baseSpaceShip.Yposition, ConsoleColor.Cyan));
            dotsAlreadyAdded++;
        }

        public void RemoveExpiredDot()
        {
            for (int i = 0; i < dots.Count; i++)
            {
                if (dots[i].IsLifespanOver())
                {
                    dots.Remove(dots[i]);
                    break; // Currently used to avoid unnecessary checks
                }
            }
        }

        public bool IsCapacityReached()
        {
            if (dotsAlreadyAdded >= dotCapacity)
            {
                capacityReached = true;
            }

            return capacityReached;
        }
    }
}
