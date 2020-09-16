using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAutoEcole.Service
{
    [Table ("MONITEURS")]
    public class Moniteur
    {
        [Key]
        public int MoniteurID { get; set; }
        [StringLength(30)]
        public string NomMoniteur { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public virtual ICollection<Client> clients { get; set; }
        
    }
}
