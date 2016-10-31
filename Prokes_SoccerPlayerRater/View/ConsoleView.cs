using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prokes_SoccerPlayerRater
{
    public static class ConsoleView
    {
        #region Fields

        //
        // window size
        //
        private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
        private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

        //
        // horizontal and vertical margins in console window for display
        //
        private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
        private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

        #endregion

        #region Properties
        
        #endregion

        #region Constructors
        
        #endregion

        #region Methods

        public static MenuOption.MenuAction GetUserMenuActionChoice()
        {
            Console.CursorVisible = false;

            MenuOption.MenuAction userMenuActionChoice = MenuOption.MenuAction.None;

            string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

            DisplayReset();

            DisplayMessage("Futbol Player Manager Menu");
            DisplayMessage("");
            Console.WriteLine(
                leftTab + "1. Display All Player Info" + Environment.NewLine +
                leftTab + "2. Add Player" + Environment.NewLine +
                leftTab + "3. Update Player" + Environment.NewLine +
                leftTab + "4. Delete Player" + Environment.NewLine +
                leftTab + "5. Query Player by Position" + Environment.NewLine +
                leftTab + "6. Sort Player by Position" + Environment.NewLine +
                leftTab + "7. Exit");
            DisplayMessage("");
            DisplayMessage("Enter the number corresponding to your Menu choice.");
            ConsoleKeyInfo userChoice = Console.ReadKey(true);

            switch (userChoice.KeyChar)
            {
                case '1':
                    userMenuActionChoice = MenuOption.MenuAction.DisplayAllPlayers;
                    break;
                case '2':
                    userMenuActionChoice = MenuOption.MenuAction.AddPlayer;
                    break;
                case '3':
                    userMenuActionChoice = MenuOption.MenuAction.UpdatePlayer;
                    break;
                case '4':
                    userMenuActionChoice = MenuOption.MenuAction.DeletePlayer;
                    break;
                case '5':
                    userMenuActionChoice = MenuOption.MenuAction.QueryPlayersByPosition;
                    break;
                case '6':
                    userMenuActionChoice = MenuOption.MenuAction.SortPlayersByPosition;
                    break;
                case '7':
                    userMenuActionChoice = MenuOption.MenuAction.Exit;
                    break;
                default:
                    Console.WriteLine(
                        "It appears you have selected an incorrect choice." + Environment.NewLine +
                        "Press any key to try again or the ESC key to exit.");

                    userChoice = Console.ReadKey(true);
                    if (userChoice.Key == ConsoleKey.Escape)
                    {
                        userMenuActionChoice = MenuOption.MenuAction.Exit;
                    }
                    break;
            }

            return userMenuActionChoice;
        }

        public static void DisplayAllPlayers(List<Player> players)
        {
            DisplayInitialPlayerDetails(players);

            DisplayContinuePrompt();
        }

        public static void DisplayAddPlayer(List<Player> players)
        {
            bool validResponse = false;

            string userResponse;

            DisplayReset();

            Player player = new Player();
            int playerID = players.Count();
            StringBuilder playerInfo = new StringBuilder();

            player.ID = playerID + 1;

            while (!validResponse)
            {
                Console.Write("Enter the First Name of the Player you wish to add:  ");
                userResponse = Console.ReadLine();
                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The First Name you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                    DisplayReset();
                }

                else
                {
                    player.firstName = userResponse;
                    validResponse = true;
                }
            }

            validResponse = false;

            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the Last Name of the Player you wish to add:  ");
                userResponse = Console.ReadLine();
                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The Last Name you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else
                {
                    player.lastName = userResponse;
                    validResponse = true;
                }
            }

            validResponse = false;

            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the Jersey Number of the Player you wish to add:  ");
                userResponse = Console.ReadLine();
                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The Jersey Number you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else if (userResponse != "")
                {
                    if (Convert.ToInt32(userResponse) <= 0 || Convert.ToInt32(userResponse) >= 100)
                    {
                        Console.WriteLine();
                        DisplayMessage("The Jersey Number must be 1-99; press any key to try again...");
                        Console.WriteLine();
                        Console.ReadKey();
                    }
                    else if (Convert.ToInt32(userResponse) > 0 && Convert.ToInt32(userResponse) < 100)
                    {
                        player.Number = Convert.ToInt32(userResponse);
                        validResponse = true;
                    }
                }
            }

            validResponse = false;

            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the Position of the Player you wish to add" + Environment.NewLine +
                                "(Goalkeeper/Defender/Midfielder/Forward):  ");
                userResponse = Console.ReadLine().ToUpper();

                if (userResponse == "GOALKEEPER" || userResponse == "DEFENDER" ||
                    userResponse == "MIDFIELDER" || userResponse == "FORWARD")
                {
                    player._position = (Player.Position)Enum.Parse(typeof(Player.Position), userResponse);
                    validResponse = true;
                    Console.WriteLine();
                    Console.WriteLine();
                }

                else
                {
                    Console.WriteLine();
                    DisplayMessage("The Player Position you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }

            playerInfo.Append(player.ID.ToString().PadRight(10));
            playerInfo.Append(player.firstName.PadRight(12));
            playerInfo.Append(player.lastName.PadRight(12));
            playerInfo.Append(player.Number.ToString().PadRight(10));
            playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(10));
            columnHeader.Append("FirstName".PadRight(12));
            columnHeader.Append("LastName".PadRight(12));
            columnHeader.Append("Number".PadRight(10));
            columnHeader.Append("Position".PadRight(12));

            DisplayMessage(columnHeader.ToString());

            DisplayMessage(playerInfo.ToString());

            Console.WriteLine();

            DisplayMessage("Your Player has been added!");

            players.Add(player);

            DisplayContinuePrompt();
        }

        public static void DisplayUpdatePlayer(List<Player> players)
        {
            DisplayReset();

            bool validResponse = false;
            List<Player> playerDetails = new List<Player>();
            Player playerU = new Player();
            int playerID;
            string userResponse = "";

            DisplayMessage("All of the existing Players are displayed below:");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(10));
            columnHeader.Append("FirstName".PadRight(12));
            columnHeader.Append("LastName".PadRight(12));
            columnHeader.Append("Number".PadRight(10));
            columnHeader.Append("Position".PadRight(12));

            DisplayMessage(columnHeader.ToString());

            foreach (Player player in players)
            {
                StringBuilder playerInfo = new StringBuilder();

                playerInfo.Append(player.ID.ToString().PadRight(10));
                playerInfo.Append(player.firstName.PadRight(12));
                playerInfo.Append(player.lastName.PadRight(12));
                playerInfo.Append(player.Number.ToString().PadRight(10));
                playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                playerDetails.Add(player);

                DisplayMessage(playerInfo.ToString());
            }
            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the ID number of the Player you wish to Update:  ");

                userResponse = Console.ReadLine();

                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The ID you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else 
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        playerU.ID = players[i].ID;
                    }

                    if (Convert.ToInt32(userResponse) != playerU.ID)
                    {
                        Console.WriteLine();
                        DisplayMessage("The ID you have entered is invalid; press any key to try again...");
                        Console.WriteLine();
                        Console.ReadKey();
                    }

                    else
                    {
                        validResponse = true;
                    }
                }
            }

            validResponse = false;

            playerID = Convert.ToInt32(userResponse);

            StringBuilder playerDeets = new StringBuilder();
            StringBuilder updatedPlayerDeets = new StringBuilder();

            Console.WriteLine();

            DisplayMessage(columnHeader.ToString());

            playerDeets.Append(playerDetails[playerID - 1].ID.ToString().PadRight(10));
            playerDeets.Append(playerDetails[playerID - 1].firstName.PadRight(12));
            playerDeets.Append(playerDetails[playerID - 1].lastName.PadRight(12));
            playerDeets.Append(playerDetails[playerID - 1].Number.ToString().PadRight(10));
            playerDeets.Append((Player.Position)Enum.Parse(typeof(Player.Position), playerDetails[playerID - 1]._position.ToString().PadRight(12)));
            DisplayMessage(playerDeets.ToString());
            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the new First Name of the Player:  ");
                userResponse = Console.ReadLine();
                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The First Name you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else
                {
                    playerDetails[playerID - 1].firstName = userResponse;
                    validResponse = true;
                }
            }

            validResponse = false;

            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the new Last Name of the Player:  ");
                userResponse = Console.ReadLine();
                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The Last Name you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else
                {
                    playerDetails[playerID - 1].lastName = userResponse;
                    validResponse = true;
                }
            }

            validResponse = false;

            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the Jersey Number of the Player you wish to add:  ");
                userResponse = Console.ReadLine();
                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The Jersey Number you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else if (userResponse != "")
                {
                    if (Convert.ToInt32(userResponse) <= 0 || Convert.ToInt32(userResponse) >= 100)
                    {
                        Console.WriteLine();
                        DisplayMessage("The Jersey Number must be 1-99; press any key to try again...");
                        Console.WriteLine();
                        Console.ReadKey();
                    }
                    else if (Convert.ToInt32(userResponse) > 0 && Convert.ToInt32(userResponse) < 100)
                    {
                        playerDetails[playerID - 1].Number = Convert.ToInt32(userResponse);
                        validResponse = true;
                    }
                }
            }

            validResponse = false;

            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the Position of the Player you wish to add" + Environment.NewLine +
                                "(Goalkeeper/Defender/Midfielder/Forward):  ");
                userResponse = Console.ReadLine().ToUpper();

                if (userResponse == "GOALKEEPER" || userResponse == "DEFENDER" ||
                    userResponse == "MIDFIELDER" || userResponse == "FORWARD")
                {
                    playerDetails[playerID - 1]._position = (Player.Position)Enum.Parse(typeof(Player.Position), userResponse);
                    validResponse = true;
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    DisplayMessage("The Player Position you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }

            DisplayMessage(columnHeader.ToString());

            updatedPlayerDeets.Append(playerDetails[playerID - 1].ID.ToString().PadRight(10));
            updatedPlayerDeets.Append(playerDetails[playerID - 1].firstName.PadRight(12));
            updatedPlayerDeets.Append(playerDetails[playerID - 1].lastName.PadRight(12));
            updatedPlayerDeets.Append(playerDetails[playerID - 1].Number.ToString().PadRight(10));
            updatedPlayerDeets.Append((Player.Position)Enum.Parse(typeof(Player.Position), playerDetails[playerID - 1]._position.ToString().PadRight(12)));
            DisplayMessage(updatedPlayerDeets.ToString());

            Console.WriteLine();

            DisplayMessage("Your Player has been Updated!");

            DisplayContinuePrompt();
        }

        public static void DisplayDeleteRecord(List<Player> players)
        {
            DisplayReset();

            List<Player> playerDetails = new List<Player>();
            int playerID;
            bool validResponse = false;
            string userResponse = "";
            Player playerD = new Player();

            DisplayMessage("All of the existing Players are displayed below:");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(10));
            columnHeader.Append("FirstName".PadRight(12));
            columnHeader.Append("LastName".PadRight(12));
            columnHeader.Append("Number".PadRight(10));
            columnHeader.Append("Position".PadRight(12));

            DisplayMessage(columnHeader.ToString());

            foreach (Player player in players)
            {
                StringBuilder playerInfo = new StringBuilder();

                playerInfo.Append(player.ID.ToString().PadRight(10));
                playerInfo.Append(player.firstName.PadRight(12));
                playerInfo.Append(player.lastName.PadRight(12));
                playerInfo.Append(player.Number.ToString().PadRight(10));
                playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                playerDetails.Add(player);

                DisplayMessage(playerInfo.ToString());
            }
            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the ID number of the Player you wish to Delete:  ");

                userResponse = Console.ReadLine();

                if (userResponse == "")
                {
                    Console.WriteLine();
                    DisplayMessage("The ID you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
                else
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        playerD.ID = players[i].ID;
                    }

                    if (Convert.ToInt32(userResponse) != playerD.ID)
                    {
                        Console.WriteLine();
                        DisplayMessage("The ID you have entered is invalid; press any key to try again...");
                        Console.WriteLine();
                        Console.ReadKey();
                    }

                    else
                    {
                        validResponse = true;
                    }
                }
            }

            playerID = Convert.ToInt32(userResponse);

            Console.WriteLine();

            players.Remove(playerDetails[playerID - 1]);

            DisplayMessage("Your Player has been Deleted!");

            DisplayContinuePrompt();
        }

        public static void DisplayQueryPlayer(List<Player> players)
        {
            DisplayReset();

            List<Player> playerDetails = new List<Player>();
            Player.Position playerPosition = Player.Position.None;
            bool validResponse = false;
            string userResponse;

            DisplayMessage("All of the existing Players are displayed below:");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(10));
            columnHeader.Append("FirstName".PadRight(12));
            columnHeader.Append("LastName".PadRight(12));
            columnHeader.Append("Number".PadRight(10));
            columnHeader.Append("Position".PadRight(12));

            DisplayMessage(columnHeader.ToString());

            foreach (Player player in players)
            {
                StringBuilder playerInfo = new StringBuilder();

                playerInfo.Append(player.ID.ToString().PadRight(10));
                playerInfo.Append(player.firstName.PadRight(12));
                playerInfo.Append(player.lastName.PadRight(12));
                playerInfo.Append(player.Number.ToString().PadRight(10));
                playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                playerDetails.Add(player);

                DisplayMessage(playerInfo.ToString());
            }

            Console.WriteLine();

            while (!validResponse)
            {
                Console.Write("Enter the Position of the Player you wish to add" + Environment.NewLine +
                                "(Goalkeeper/Defender/Midfielder/Forward):  ");
                userResponse = Console.ReadLine().ToUpper();

                if (userResponse == "GOALKEEPER" || userResponse == "DEFENDER" ||
                    userResponse == "MIDFIELDER" || userResponse == "FORWARD")
                {
                    playerPosition = (Player.Position)Enum.Parse(typeof(Player.Position), userResponse);
                    validResponse = true;
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    DisplayMessage("The Player Position you have entered is invalid; press any key to try again...");
                    Console.WriteLine();
                    Console.ReadKey();
                }
            }

            Console.WriteLine();

            foreach (Player player in players)
            {
                StringBuilder playerInfo = new StringBuilder();

                if (player._position == Player.Position.GOALKEEPER && playerPosition == Player.Position.GOALKEEPER)
                {
                    playerInfo.Append(player.ID.ToString().PadRight(10));
                    playerInfo.Append(player.firstName.PadRight(12));
                    playerInfo.Append(player.lastName.PadRight(12));
                    playerInfo.Append(player.Number.ToString().PadRight(10));
                    playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                    playerDetails.Add(player);
                }

                else if (player._position == Player.Position.DEFENDER && playerPosition == Player.Position.DEFENDER)
                {
                    playerInfo.Append(player.ID.ToString().PadRight(10));
                    playerInfo.Append(player.firstName.PadRight(12));
                    playerInfo.Append(player.lastName.PadRight(12));
                    playerInfo.Append(player.Number.ToString().PadRight(10));
                    playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                    playerDetails.Add(player);
                }

                else if (player._position == Player.Position.MIDFIELDER && playerPosition == Player.Position.MIDFIELDER)
                {
                    playerInfo.Append(player.ID.ToString().PadRight(10));
                    playerInfo.Append(player.firstName.PadRight(12));
                    playerInfo.Append(player.lastName.PadRight(12));
                    playerInfo.Append(player.Number.ToString().PadRight(10));
                    playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                    playerDetails.Add(player);
                }

                else if (player._position == Player.Position.FORWARD && playerPosition == Player.Position.FORWARD)
                {
                    playerInfo.Append(player.ID.ToString().PadRight(10));
                    playerInfo.Append(player.firstName.PadRight(12));
                    playerInfo.Append(player.lastName.PadRight(12));
                    playerInfo.Append(player.Number.ToString().PadRight(10));
                    playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                    playerDetails.Add(player);
                }

                DisplayMessage(playerInfo.ToString());
            }

            DisplayContinuePrompt();
        }

        public static void DisplaySortPlayers(List<Player> players)
        {
            DisplayReset();

            List<Player> playerDetails = new List<Player>();

            DisplayMessage("All of the existing Players, sorted by Position, are displayed below:");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(10));
            columnHeader.Append("FirstName".PadRight(12));
            columnHeader.Append("LastName".PadRight(12));
            columnHeader.Append("Number".PadRight(10));
            columnHeader.Append("Position".PadRight(12));

            DisplayMessage(columnHeader.ToString());

            foreach (Player player in players)
            {
                StringBuilder playerInfo = new StringBuilder();

                playerInfo.Append(player.ID.ToString().PadRight(10));
                playerInfo.Append(player.firstName.PadRight(12));
                playerInfo.Append(player.lastName.PadRight(12));
                playerInfo.Append(player.Number.ToString().PadRight(10));
                playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                playerDetails.Add(player);
            }
            Console.WriteLine();

            List<Player> sortedPlayerList = playerDetails.OrderBy(o => o._position).ToList();

            foreach (Player player in sortedPlayerList)
            {
                StringBuilder playerInfo = new StringBuilder();

                playerInfo.Append(player.ID.ToString().PadRight(10));
                playerInfo.Append(player.firstName.PadRight(12));
                playerInfo.Append(player.lastName.PadRight(12));
                playerInfo.Append(player.Number.ToString().PadRight(10));
                playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                playerDetails.Add(player);

                DisplayMessage(playerInfo.ToString());
            }

            DisplayContinuePrompt();
        }

        public static void DisplayInitialPlayerDetails(List<Player> players)
        {
            DisplayReset();

            List<Player> playerDetails = new List<Player>();

            DisplayMessage("All of the existing Players are displayed below:");
            DisplayMessage("");

            StringBuilder columnHeader = new StringBuilder();

            columnHeader.Append("ID".PadRight(10));
            columnHeader.Append("FirstName".PadRight(12));
            columnHeader.Append("LastName".PadRight(12));
            columnHeader.Append("Number".PadRight(10));
            columnHeader.Append("Position".PadRight(12));

            DisplayMessage(columnHeader.ToString());

            Console.WriteLine();

            foreach (Player player in players)
            {
                StringBuilder playerInfo = new StringBuilder();

                playerInfo.Append(player.ID.ToString().PadRight(10));
                playerInfo.Append(player.firstName.PadRight(12));
                playerInfo.Append(player.lastName.PadRight(12));
                playerInfo.Append(player.Number.ToString().PadRight(10));
                playerInfo.Append((Player.Position)Enum.Parse(typeof(Player.Position), player._position.ToString().PadRight(12)));

                playerDetails.Add(player);

                DisplayMessage(playerInfo.ToString());
            }
            Console.WriteLine();
        }

        /// <summary>
        /// reset display to default size and colors including the header
        /// </summary>
        public static void DisplayReset()
        {
            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Futbol Player Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();
        }


        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }


        /// <summary>
        /// display the Exit prompt
        /// </summary>
        public static void DisplayExitPrompt()
        {
            DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            DisplayMessage("Thank you for using our application. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.Center("The Futbol Player Rater", WINDOW_WIDTH));
            Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

            Console.ResetColor();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a message in the message area
        /// </summary>
        /// <param name="message">string to display</param>
        public static void DisplayMessage(string message)
        {
            //
            // calculate the message area location on the console window
            //
            const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
            const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

            // message is not an empty line, display text
            if (message != "")
            {
                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                foreach (var messageLine in messageLines)
                {
                    Console.WriteLine(messageLine);
                }
            }
            // display an empty line
            else
            {
                Console.WriteLine();
            }
        }
        #endregion
    }
}
