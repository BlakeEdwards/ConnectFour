using ConnectFour;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinaryFileHandling
{
    class BinaryFileHelper
    {
        public enum BinaryType {Number,Numbers, String, Strings}
        public static void SaveFile(string filePath, string file)
        {
            //File.Open(fileName, FileMode.Create)
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write((int)BinaryType.String);       // save Type
                writer.Write(file);
            }
        }
        public static void SaveFile(string filePath, int[] file)
        {
            //File.Open(fileName, FileMode.Create)
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                
                writer.Write((int)BinaryType.Numbers);      //Save Type Numbers
                writer.Write(file.Length);                  // Array Length
                for (int i = 0; i < file.Length; i++)       // Save array values
                {
                    writer.Write(file[i]);
                }
            }
        }
        public static string LoadFile(string filePath)
        {
            string file = "";
            if (File.Exists(filePath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    BinaryType type = (BinaryType)reader.ReadInt32();
                    if (type == BinaryType.String) { file = reader.ReadString(); }
                    else { file = "Error IncorrectFormat: File Formate"; }
                }
            }
            return file;
        }
        /// <summary>
        /// Return an int32[] from a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>

        public static void SaveGameState(string filePath,GameState gameState)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(gameState.turn);
                writer.Write(gameState.board.GetLength(0));
                writer.Write(gameState.board.GetLength(1));
                for (int x = 0; x < gameState.board.GetLength(0); x++)
                {
                    for (int y = 0; y < gameState.board.GetLength(1); y++)
                    {
                        writer.Write(gameState.board[x, y]);
                    }
                }
            }
        }
        public static GameState LoadGameState(string filePath)
        {
            if (File.Exists(filePath))
            {
                int turn;
                int[,] board;
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    turn = reader.ReadInt32();
                    int columns = reader.ReadInt32();
                    int rows = reader.ReadInt32();
                    board = new int[columns, rows];
                    for (int x = 0; x < columns; x++)
                    {
                        for (int y = 0; y < rows; y++)
                        {
                            board[x, y] = reader.ReadInt32();
                        }
                    }
                }
                return new GameState(turn, board);
            }
            else
                return GameState.GetGameState(); // return Default
            
        }
    }
}
