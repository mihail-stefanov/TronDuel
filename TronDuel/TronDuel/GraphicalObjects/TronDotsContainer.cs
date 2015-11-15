namespace TronDuel.GraphicalObjects
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
            byte xPotentialPosition = (byte) baseSpaceShip.XpreviousPosition;
            byte yPotentialPosition = (byte) baseSpaceShip.YpreviousPosition;

            // Making sure that the new dot does not overlap the spaceship
            if (!(xPotentialPosition == (byte)baseSpaceShip.Xposition &&
                yPotentialPosition == (byte)baseSpaceShip.Yposition))
            {
                if (Dots.Count > 0)
                {
                    // Making sure the new dot does not overlap the old dot
                    if (!(Dots[Dots.Count - 1].Xposition == xPotentialPosition &&
                        Dots[Dots.Count - 1].Yposition == yPotentialPosition))
                    {
                        this.dots.Add(new TronDot(xPotentialPosition, yPotentialPosition, ConsoleColor.Cyan));
                        dotsAlreadyAdded++;
                    }
                }
                else
                {
                    this.dots.Add(new TronDot(xPotentialPosition, yPotentialPosition, ConsoleColor.Cyan));
                    dotsAlreadyAdded++;
                }
            }
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
