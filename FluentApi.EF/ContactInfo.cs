namespace FluentApi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContactInfos")]
    public partial class ContactInfo
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
