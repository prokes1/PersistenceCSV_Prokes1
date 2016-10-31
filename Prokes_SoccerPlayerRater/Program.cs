using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prokes_SoccerPlayerRater;

namespace Prokes_SoccerPlayerRater
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeDataFile.AddPlayerData();

            Controller applicationController = new Controller();
        }
    }
}
