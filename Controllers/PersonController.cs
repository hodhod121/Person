using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGame.Data;
using MyGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGame.Controllers
{
    public class PersonController : Controller
    {
        private readonly MyGameContext _context;

        public PersonController(MyGameContext context)
        {
            _context = context;
        }
     
        public IActionResult Index(string SearchText="")
        {
            List<PersonModel> people;
            if (SearchText!="" && SearchText!=null)
            {
               
                people = _context.Person.Where(per=>per.name .Contains(SearchText)).ToList();
                people.AddRange(_context.Person.Where(per=>per.city .Contains(SearchText)));
                
                return View(people);
            }
            else
            {
                people= _context.Person.ToList();
            }
                return View(people);    
        }
        [HttpGet]
        public IActionResult Create()
        {
            PersonModel person = new PersonModel();

            return View(person);
        }
        [HttpPost]
        public IActionResult Create(PersonModel people)
        {
            _context.Add(people);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {

            PersonModel person = _context.Person.Where(p => p.id == Id).FirstOrDefault();
          
            return View(person);
        }
        [HttpGet]
        public  IActionResult Edit(int Id)
        {
            PersonModel person = _context.Person.Where(p => p.id == Id).FirstOrDefault();
          
            return View(person);
        }
        [HttpPost]
        public IActionResult Edit(PersonModel people)
        {           
            _context.Attach(people);
            _context.Entry(people).State = EntityState.Modified;
            _context.SaveChanges(); 
            return RedirectToAction("Index");
          
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            PersonModel person = _context.Person.Where(p => p.id == Id).FirstOrDefault();

            return View(person);
        }
        [HttpPost]
        public IActionResult Delete(PersonModel people)
        {
            _context.Attach(people);
            _context.Entry(people).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
