using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Practical_AddressBook1.Models
{
    public class AddressBookLogs
    {
        [Key]
        public int PersonID { get; set; }

        [DisplayName("Person Name")]
        [Required]
        public string PersonName { get; set; }

        [DisplayName("Person Address")]
        [Required]
        public string PersonAddress { get; set; }

        [DisplayName("City")]
        [Required]
        public string City { get; set; }
    }
}
