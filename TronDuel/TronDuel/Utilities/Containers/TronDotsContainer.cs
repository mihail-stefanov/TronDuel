namespace TronDuel.GraphicalObjects.Containers
{
    using System;
    using System.Collections.Generic;
    using TronDuel.MovingObjects.GraphicalObjects;

    public class TronDotsContainer
    {
        private byte dotCapacity = 150;
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
            byte xpotentialPosition = (byte)this.baseSpaceShip.XpreviousPosition;
            byte ypotentialPosition = (byte)this.baseSpaceShip.YpreviousPosition;

            // Making sure that the new dot does not overlap the spaceship
            if (!(xpotentialPosition == (byte)this.baseSpaceShip.Xposition &&
                ypotentialPosition == (byte)this.baseSpaceShip.Yposition))
            {
                if (this.Dots.Count > 0)
                {
                    // Making sure the new dot does not overlap the old dot
                    if (!(this.Dots[this.Dots.Count - 1].Xposition == xpotentialPosition &&
                        this.Dots[this.Dots.Count - 1].Yposition == ypotentialPosition))
                    {
                        this.Dots.Add(new TronDot(xpotentialPosition, ypotentialPosition, ConsoleColor.Cyan));
                        this.dotsAlreadyAdded++;
                    }
                }
                else
                {
                    this.dots.Add(new TronDot(xpotentialPosition, ypotentialPosition, ConsoleColor.Cyan));
                    this.dotsAlreadyAdded++;
                }
            }
        }

        public void RemoveExpiredDot()
        {
            for (int i = 0; i < this.Dots.Count; i++)
            {
                if (this.Dots[i].IsLifespanOver())
                {
                    this.Dots.Remove(this.dots[i]);
                    break; // Currently used to avoid unnecessary checks
                }
            }
        }

        public bool IsCapacityReached()
        {
            if (this.dotsAlreadyAdded >= this.dotCapacity)
            {
                this.capacityReached = true;
            }

            return this.capacityReached;
        }

        public void ReachCapacity()
        {
            this.capacityReached = true;
        }
    }
}
