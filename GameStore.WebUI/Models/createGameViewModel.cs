using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Models
{
    public class CreateGameViewModel
    {
        public Game Game { get; set; }
        public SelectList Category { get; private set; }

        public CreateGameViewModel() { }

        public CreateGameViewModel(IEnumerable<Category> category, int ItemId = 0)
        {
            setCategory(category, ItemId);
        }

        public void setCategory(IEnumerable<Category> category, int ItemId = 0)
        {
            
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (Category item in category)
            {
                bool selected = false;
                if (ItemId == item.CategoryId)
                {
                    selected = true;
                }
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryId.ToString(),
                    Text = item.Name,
                    Selected = selected
                });
            }
            Category = new SelectList(list);
        }
    }
}