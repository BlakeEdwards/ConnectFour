using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class BoardSaveLoad
    {
        public static GameState Load(string filePath,string fileName)
        {
            string file  = OpenFile();
            GameState loadGame = decodeFile(file);
            return new GameState();
        }
        // ToDo
        private static GameState decodeFile(string file)
        {
            throw new NotImplementedException();
        }

        private static string OpenFile()
        {
            throw new NotImplementedException();
        }
        public static void Save(GameState game)
        {
            string file = encodeFile(game);
            DoSave(file);
        }

        private static void DoSave(string file)
        {
            throw new NotImplementedException();
        }

        private static string encodeFile(GameState game)
        {
            throw new NotImplementedException();
        }
    }
}
