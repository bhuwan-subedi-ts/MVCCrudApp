using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace MVCCrud.Models
{
    public class BookViewModel
    {
        [Key]
       public int ID { get; set;}
        [Required]
       public string Name { get; set;}
        [Required]
       public string Author { get; set;}
        [Range(1,int.MaxValue,ErrorMessage ="Enter the correct price value")]
       public int price { get; set;}
        [ForeignKey("Categories")]
       public int CategoryID { get; set;}
    }
}
