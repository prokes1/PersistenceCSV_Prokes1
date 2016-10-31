using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prokes_SoccerPlayerRater
{
    public class Player
    {
        public enum Position
        {
            None,
            GOALKEEPER,
            DEFENDER,
            MIDFIELDER,
            FORWARD
        }

        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int Number { get; set; }
        public Position _position { get; set; }

        public Player()
        {

        }

        public Player(int id, string FirstName, string LastName, int number, Position position)
        {
            this.ID = id;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.Number = number;
            this._position = position;
        }
    }
}
