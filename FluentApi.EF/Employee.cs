namespace FluentApi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public class Person
    {
        protected string name;

        public string Name
        {
            get => name;
            set
            {
                if(!Validator.IsNameValid(value))
                    throw new ArgumentException("Invalid value provided");
                name = value;
            }
        }
    }

    public partial class Employee: Person
    {


        public int Id
        {
            get; set;
        }

        public int? TeamId
        {
            get; set;
        }

        public virtual ContactInfo ContactInfo
        {
            get; set;
        }

        public virtual Team Team
        {
            get; set;
        }

        public decimal? Salary
        {
            get; set;
        }
    }
}