namespace FluentApi.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Employee:INotifyPropertyChanged
    {

        private string name;
        public int Id { get; set; }

        [StringLength(100)]
        public string Name
        {
            get => name;
            set
            {
                if(value != name)
                {
                    name = value;
                    Notify(nameof(Name));
                }
            }
        }

        public int? TeamId { get; set; }

        public virtual ContactInfo ContactInfo { get; set; }

        public virtual Team Team { get; set; }

        protected virtual void Notify(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
