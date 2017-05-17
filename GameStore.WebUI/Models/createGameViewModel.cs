using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class CreateGameViewModel
    {
        public CreateGameViewModel()
        {
            Game = new Game();
        }

        public Game Game;
        public IEnumerable<Category> Category; 
    }
}