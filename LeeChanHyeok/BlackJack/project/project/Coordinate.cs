using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    class DealerFrontCardPatternCoordinate
    {
        public int X;
        public int Y;
    }
    class DealerEndCardPatternCoordinate
    {
        public int X;
        public int Y;
    }
    class DealerNumberCoordinate
    {
        public int X;
        public int Y;
    }
    class UserFrontCardPatternCoordinate
    {
        public int X; 
        public int Y;
    }
    class UserEndCardPatternCoordinate
    {
        public int X;
        public int Y;
    }
    class UserNumberCoordinate
    {
        public int X;
        public int Y;
    }
    class Coordinate
    {
        public DealerFrontCardPatternCoordinate[] dealerFrontCardPatternCoordinate = new DealerFrontCardPatternCoordinate[]
        {
                new DealerFrontCardPatternCoordinate {X = 51, Y = 9},
                new DealerFrontCardPatternCoordinate {X = 66, Y = 9},
                new DealerFrontCardPatternCoordinate {X = 81, Y = 9}
        };

        public DealerEndCardPatternCoordinate[] dealerEndCardPatternCoordinate = new DealerEndCardPatternCoordinate[]
        {
                new DealerEndCardPatternCoordinate {X = 60, Y = 15},
                new DealerEndCardPatternCoordinate {X = 75, Y = 15},
                new DealerEndCardPatternCoordinate {X = 90, Y = 15}
        };

        public DealerNumberCoordinate[] dealerNumberCoordinate = new DealerNumberCoordinate[]
        {
                new DealerNumberCoordinate {X = 56, Y = 12},
                new DealerNumberCoordinate {X = 71, Y = 12},
                new DealerNumberCoordinate {X = 86, Y = 12}
        };

        public UserFrontCardPatternCoordinate[] userFrontCardPatternCoordinate = new UserFrontCardPatternCoordinate[]
        {
                new UserFrontCardPatternCoordinate {X = 51, Y = 24},
                new UserFrontCardPatternCoordinate {X = 66, Y = 24},
                new UserFrontCardPatternCoordinate {X = 81, Y = 24}
        };

        public UserEndCardPatternCoordinate[] userEndCardPatternCoordinate = new UserEndCardPatternCoordinate[]
        {
                new UserEndCardPatternCoordinate {X = 60, Y = 30},
                new UserEndCardPatternCoordinate {X = 75, Y = 30},
                new UserEndCardPatternCoordinate {X = 90, Y = 30}
        };

        public UserNumberCoordinate[] userNumberCoordinate = new UserNumberCoordinate[]
        {
                new UserNumberCoordinate {X = 56, Y = 27},
                new UserNumberCoordinate {X = 71, Y = 27},
                new UserNumberCoordinate {X = 86, Y = 27}
        };
    }
}
