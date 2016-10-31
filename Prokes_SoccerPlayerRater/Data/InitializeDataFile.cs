using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prokes_SoccerPlayerRater
{
    public class InitializeDataFile
    {
        public static void AddPlayerData()
        {
            List<Player> players = new List<Player>();

            players.Add(new Player() { ID = 1, firstName = "Cristiano", lastName = "Ronaldo", Number = 7, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 2, firstName = "Lionel", lastName = "Messi", Number = 10, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 3, firstName = "Luis", lastName = "Suarez", Number = 9, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 4, firstName = "Manuel", lastName = "Neuer", Number = 1, _position = Player.Position.GOALKEEPER });
            players.Add(new Player() { ID = 5, firstName = "Robert", lastName = "Lewandoski", Number = 9, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 6, firstName = "Neymar", lastName = "Jr.", Number = 11, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 7, firstName = "Sergio", lastName = "Aguero", Number = 10, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 8, firstName = "Eden", lastName = "Hazard", Number = 10, _position = Player.Position.MIDFIELDER });
            players.Add(new Player() { ID = 9, firstName = "Kevin", lastName = "De Bruyne", Number = 17, _position = Player.Position.MIDFIELDER });
            players.Add(new Player() { ID = 10, firstName = "Mesut", lastName = "Ozil", Number = 11, _position = Player.Position.MIDFIELDER });
            players.Add(new Player() { ID = 11, firstName = "Gareth", lastName = "Bale", Number = 11, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 12, firstName = "Luka", lastName = "Modric", Number = 19, _position = Player.Position.MIDFIELDER });
            players.Add(new Player() { ID = 13, firstName = "Toni", lastName = "Kroos", Number = 8, _position = Player.Position.MIDFIELDER });
            players.Add(new Player() { ID = 14, firstName = "Pierre", lastName = "Aubameyang", Number = 17, _position = Player.Position.FORWARD });
            players.Add(new Player() { ID = 15, firstName = "Marco", lastName = "Reus", Number = 11, _position = Player.Position.MIDFIELDER });
            players.Add(new Player() { ID = 16, firstName = "Jerome", lastName = "Boateng", Number = 17, _position = Player.Position.DEFENDER });
            players.Add(new Player() { ID = 17, firstName = "Sergio", lastName = "Ramos", Number = 4, _position = Player.Position.DEFENDER });
            players.Add(new Player() { ID = 18, firstName = "David", lastName = "De Gea", Number = 1, _position = Player.Position.GOALKEEPER });
            players.Add(new Player() { ID = 19, firstName = "David", lastName = "Silva", Number = 21, _position = Player.Position.MIDFIELDER });
            players.Add(new Player() { ID = 20, firstName = "Paul", lastName = "Pogba", Number = 6, _position = Player.Position.MIDFIELDER });

            WriteAllPlayers(players, DataSettings.dataFilePath);
        }

        public static void WriteAllPlayers(List<Player> players, string dataFilePath)
        {
            string playerString;

            List<string> playerStringList = new List<string>();

            foreach (Player player in players)
            {
                playerString = player.ID + "|" + player.firstName + "|"
                    + player.lastName + "|"
                    + player.Number + "|"
                    + player._position;
                playerStringList.Add(playerString);
            }

            FileStream _fileStream = File.OpenWrite(DataSettings.dataFilePath);

            using (_fileStream)
            {
                StreamWriter sWriter = new StreamWriter(_fileStream);

                foreach (string player in playerStringList)
                {
                    sWriter.WriteLine(player);
                }

                sWriter.Close();
            }
        }
    }
}
