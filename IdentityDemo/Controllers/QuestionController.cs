using IdentityDemo.Data;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDemo.Controllers
{
    public class QuestionController:Controller
    {
        private AppIdentityDbContext context;

        public QuestionController(AppIdentityDbContext ctx)
        {
            context = ctx;
        }

        public ViewResult Create() => View();
        public ViewResult Thanks() => View();

        [HttpPost]
        public IActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                Question quest = new Question
                {
                    Name= question.Name,
                    Profession= question.Profession,
                    Age= question.Age
                };

                context.Questions.Attach(quest);
                context.Add(quest);
                context.SaveChanges();


                return RedirectToAction("Thanks");      
                
            }
            return View(question);
        }
     }
}
