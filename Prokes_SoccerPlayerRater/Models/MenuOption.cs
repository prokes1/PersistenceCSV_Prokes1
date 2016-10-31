using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prokes_SoccerPlayerRater
{
    public class MenuOption
    {
        public enum MenuAction
        {
            None,
            DisplayAllPlayers,
            AddPlayer,
            UpdatePlayer,
            DeletePlayer,
            QueryPlayersByPosition,
            SortPlayersByPosition,
            Exit
        }
    }
}
