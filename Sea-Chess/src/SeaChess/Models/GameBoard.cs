using System;
using System.ComponentModel.DataAnnotations;

namespace SeaChess
{
    public enum GameState
    {
        New = 0,
        InProgress = 1,
        Finished = 2
    }
}

namespace SeaChess.Models
{
    public class GameBoard
    {
        [Key]
        public Guid Id { get; set; }
        public string Data { get; set; }
        public GameState State { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
