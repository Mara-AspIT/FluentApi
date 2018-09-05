using System;
using System.Collections.Generic;
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
        public void CreateAffiliatedTeam()
        {
            Team t = new Team();
            t.Name = "A Team";
            Project p = new Project();
            p.Name = "The A Team Project";
            List<Team> demTeams = new List<Team>();
            demTeams.Add(t);
            p.Teams = demTeams;
            Model model = new Model();
            model.Projects.Add(p);
            model.SaveChanges();

            Team team = model.Teams.Where(someteam => someteam.Name == "A Team").FirstOrDefault();
            Project affiliatedProject = team.Project;
            bool projectHasTeam = team.Name == t.Name && affiliatedProject.Name == p.Name;

            Assert.IsNotNull(team);
            Assert.IsNotNull(affiliatedProject);
            Assert.IsTrue(projectHasTeam);
        }

        [ExpectedException(typeof(Exception), AllowDerivedTypes = true)]
        [TestMethod]
        public void InvalidOperationException_On_Create_Unaffiliated_ContactInfo()
        {
            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "mara@aspit.dk";
            contactInfo.Phone = "12345678";
            Model model = new Model();
            model.ContactInfos.Add(contactInfo);
            model.SaveChanges();
        }

        [TestMethod]
        public void CreateEmployeeWithoutContactInfo()
        {

        }

        [TestMethod]
        public void CreateEmployeeWithContactInfo()
        {

        }
    }
}
