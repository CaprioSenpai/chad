using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PCTO_Progetto01
{
    class Person
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthPlace { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string CF { get; set; }

        public Person(int id,string fName, string lName, string bPlace, DateTime bDate, string gender, string cf) 
        {
            this.Id = id;
            this.FirstName = fName;
            this.LastName = lName;
            this.BirthPlace = bPlace;
            this.BirthDate = bDate;
            this.Gender = gender;
            this.CF = cf;
        }
        public Person() 
        { 
        
        } 
    }
}
