using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prokes_SoccerPlayerRater
{
    public class Controller
    {
        #region Fields

        bool active = true;

        #endregion

        #region Properties
        
        #endregion

        #region Constructors

        public Controller()
        {
            ApplicationController();
        }
        
        #endregion

        #region Methods

        private void ApplicationController()
        {
            PlayerRepository playerRepository = new PlayerRepository();

            ConsoleView.DisplayWelcomeScreen();

            using (playerRepository)
            {
                List<Player> players = playerRepository.GetAllPlayers();

                while (active)
                {
                    MenuOption.MenuAction userMenuActionChoice;

                    userMenuActionChoice = ConsoleView.GetUserMenuActionChoice();

                    switch (userMenuActionChoice)
                    {
                        case MenuOption.MenuAction.None:
                            break;
                        case MenuOption.MenuAction.DisplayAllPlayers:
                            ConsoleView.DisplayAllPlayers(players);
                            break;
                        case MenuOption.MenuAction.AddPlayer:
                            ConsoleView.DisplayAddPlayer(players);
                            break;
                        case MenuOption.MenuAction.UpdatePlayer:
                            ConsoleView.DisplayUpdatePlayer(players);
                            break;
                        case MenuOption.MenuAction.DeletePlayer:
                            ConsoleView.DisplayDeleteRecord(players);
                            break;
                        case MenuOption.MenuAction.QueryPlayersByPosition:
                            ConsoleView.DisplayQueryPlayer(players);
                            break;
                        case MenuOption.MenuAction.SortPlayersByPosition:
                            ConsoleView.DisplaySortPlayers(players);
                            break;
                        case MenuOption.MenuAction.Exit:
                            active = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }
        
        #endregion
    }
}
