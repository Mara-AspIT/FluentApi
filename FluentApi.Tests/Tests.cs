using System;
using System.Linq;
using FluentApi.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentApi.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void GetAllProjects()
        {
            Model model = new Model();
            var result = model.Projects.ToList();
            Assert.AreNotEqual(result.Count, 0);
        }

        [TestMethod]
        public void AddNewproject()
        {
            Model model = new Model();
            Project p = new Project();
            p.Name = "Something";
            p.Description = "A lot of something";
            model.Projects.Add(p);
            int count = model.Projects.ToList().Count;
            model.SaveChanges();
            int newCount = model.Projects.ToList().Count;
            Assert.AreEqual(newCount, count + 1);
        }

        
        [TestMethod]
        public void UpdateProject()
        {
            Model model = new Model();
            Project p = model.Projects.Find(1);
            string oldDescription = p.Description;
            p.Description = ((new Random()).Next(0,Int32.MaxValue)).ToString();
            model.SaveChanges();
            var newP = model.Projects.Find(1);
            Assert.AreNotEqual(oldDescription, newP.Description);
        }

        [TestMethod]
        public void CreateUnaffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "Awesome Team";
            Model model = new Model();
            model.Teams.Add(t);
            model.SaveChanges();
        }

        [TestMethod]
        public void MyTestMethod()
        {

        }
    }
}
