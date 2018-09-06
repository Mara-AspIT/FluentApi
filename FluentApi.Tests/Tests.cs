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
            p.Description = ((new Random()).Next(0, Int32.MaxValue)).ToString();
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
            // Arrange:
            Model model = new Model();
            string newName = $"Lewis Hamilton {(new Random()).Next(0, Int32.MaxValue)}";
            Employee newEmployee = new Employee { Name = newName };
            model.Employees.Add(newEmployee);

            // Act:
            model.SaveChanges();
            Employee existingEmployee = model.Employees.Single(e => e.Name == newName);

            // Assert:
            Assert.AreEqual(newEmployee.Name, existingEmployee.Name);
        }

        [TestMethod]
        public void CreateEmployeeWithContactInfo()
        {
            // Arrange:
            Model model = new Model();
            string newName = $"Sebastian Vettel {(new Random()).Next(0, Int32.MaxValue)}";
            Employee newEmployee = new Employee { Name = newName };
            ContactInfo newContactInfo = new ContactInfo { Email = "seb@ferrari.it", Phone = $"{(new Random()).Next(0, Int32.MaxValue)}" };
            newEmployee.ContactInfo = newContactInfo;
            model.Employees.Add(newEmployee);

            // Act:
            model.SaveChanges();
            Employee existingEmployee = model.Employees.Single(e => e.Name == newName);
            ContactInfo existingContactInfo = existingEmployee.ContactInfo;

            // Assert:
            Assert.AreEqual(newEmployee.Name, existingEmployee.Name);
            Assert.AreEqual(newContactInfo.Phone, existingContactInfo.Phone);
        }

        //[TestMethod]
        //public void MyTestMethod()
        //{
        //    Employee kevin = new Employee { Name = "Kevin Magnussen" };
        //    Employee romain = new Employee { Name = "Romain Grojsean" };
        //    ContactInfo kevinsInfo = new ContactInfo { Email = "kmag@haas.com", Phone = $"{(new Random()).Next(0, Int32.MaxValue)}" };
        //    ContactInfo romainsInfo = new ContactInfo { Email = "romain@haas.com", Phone = $"{(new Random()).Next(0, Int32.MaxValue)}" };
        //    kevin.ContactInfo = kevinsInfo;
        //    romain.ContactInfo = romainsInfo;
        //    List<Employee> haasDrivers = new List<Employee> { kevin, romain };
        //    Team haas = new Team { Name = "Haas F1" };
        //    haas.Employees = haasDrivers;

        //    Employee sebastian = new Employee { Name = "Sebastian Vettel" };
        //    Employee kimi = new Employee { Name = "Kimi Räikkönnen" };
        //    ContactInfo sebastiansInfo = new ContactInfo { Email = "seb@ferrari.it", Phone = $"{(new Random()).Next(0, Int32.MaxValue)}" };
        //    ContactInfo kimisInfo = new ContactInfo { Email = "kimi@ferrari.it", Phone = $"{(new Random()).Next(0, Int32.MaxValue)}" };
        //    sebastian.ContactInfo = sebastiansInfo;
        //    kimi.ContactInfo = kimisInfo;
        //    List<Employee> ferrariDrivers = new List<Employee> { sebastian, kimi };
        //    Team ferrari = new Team { Name = "Scuderia Ferrari" };
        //    ferrari.Employees = ferrariDrivers;

        //    List<Team> f1Teams = new List<Team> { haas, ferrari };
        //    Project f1 = new Project { Name = "F1", Teams = f1Teams };

        //    Model model = new Model();
        //    model.Projects.Add(f1);
        //    model.SaveChanges();

        //    bool projectExists;
        //    bool has2Teams;
        //    bool eachTeamHasTwoDrivers;
        //    bool eachDriverHasAContactInfo;
        //    Project projects = model.Projects.Single(p => p.Name == "F1");
        //    List<Team> existingTeams = projects.Teams.ToList();
        //    has2Teams = existingTeams.Exists(t => t.Name == "Haas") && existingTeams.Exists(t => t.Name == "Scuderia Ferrari") ;


        //    Assert.IsTrue(projectExists && has2Teams && eachTeamHasTwoDrivers && eachTeamHasTwoDrivers);
        //}
    }
}