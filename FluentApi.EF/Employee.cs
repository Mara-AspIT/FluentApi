namespace FluentApi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Employee
    {
        protected string name;

        public int Id
        {
            get; set;
        }

        [StringLength(100)]
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
