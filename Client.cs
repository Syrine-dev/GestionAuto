using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAutoEcole.Service
{
    [Table("CLIENTS")]
    public class Client
    {
        [Key] 
        public int ClientID { get; set; }  
       // [Required,MinLength(6),MaxLength(70)]
       // [StringLength(70)]
        public string CIN { get; set; }
        //[Required,Range(10,500000)]
        public string Nom { get; set; }
        public DateTime Age { get; set; }
        public string Ville { get; set; }
        public int Nbrheure { get; set; }
        public int paie { get; set; }
        public DateTime ProchaineSeance { get; set; }
        public int MontantTotale { get; set; }
        public int MontantAjouter { get; set; }
        public string Categoriee { get; set; }
        public string Sexe { get; set; }
        
        public string HistoriquePaiement { get; set; }
        public string HistoriqueDate { get; set; }
        public string HistoriqueSeance { get; set; }
        public string Tel { get; set; }
        public DateTime DateExamen { get; set; }
        public int MoniteurID { get; set; }
        [ForeignKey("MoniteurID")]

        public List<Paiement> paiements { get; set; }


        public virtual Moniteur Moniteur { get; set;  }

    }
}
