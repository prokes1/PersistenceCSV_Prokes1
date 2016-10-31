using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prokes_SoccerPlayerRater
{
    public class PlayerRepository : IDisposable
    {
        private List<Player> _players;

        public PlayerRepository()
        {
            _players = ReadPlayerData(DataSettings.dataFilePath);
        }

        public static List<Player> ReadPlayerData(string dataFilePath)
        {
            const char delineator = '|';

            List<string> playerStringList = new List<string>();
            List<Player> playerClassList = new List<Player>();

            StreamReader sReader = new StreamReader(DataSettings.dataFilePath);

            using (sReader)
            {
                while (!sReader.EndOfStream)
                {
                    playerStringList.Add(sReader.ReadLine());
                }
            }

            foreach (string player in playerStringList)
            {
                string[] properties = player.Split(delineator);

                playerClassList.Add(new Player() { ID = Convert.ToInt32(properties[0]), firstName = properties[1], lastName = properties[2], Number = Convert.ToInt32(properties[3]), _position = (Player.Position)Enum.Parse(typeof(Player.Position), properties[4])});
            }

            return playerClassList;
        }

        public void WritePlayerData()
        {
            string playerString;

            StreamWriter sWriter = new StreamWriter(DataSettings.dataFilePath);

            using (sWriter)
            {
                foreach (Player player in _players)
                {
                    playerString = player.ID + "|" + player.firstName + "|"
                                    + player.lastName + "|"
                                    + player.Number + "|"
                                    + player._position;

                    sWriter.WriteLine(playerString);
                }
            }
        }

        public List<Player> GetAllPlayers()
        {
            return _players;
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _players = null;
        }
    }
}
