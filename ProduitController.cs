using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionAutoEcole.Service;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionAutoEcole.Controllers
{
    public class ProduitController : Controller
    {

        public CatalogueDbContext dbContext { get; set; }

        public ProduitController(CatalogueDbContext db) 
        {
            this.dbContext = db;
        }


        // GET: /<controller>/ 

        //Espace Client
         public IActionResult Index(int page=0,int size=5,string motCle="")
        {
            int position = page * size;
            IEnumerable<Client> clients = dbContext.clients.Where(p=>p.Moniteur.NomMoniteur.Contains(motCle)).Skip(position).Take(size).Include(p=>p.Moniteur).ToList();//c'est  la methode syntax link (on peut utiliser le query syntax)
            ViewBag.currentPage = page;
            int totalPages;
            int nbreProduit = dbContext.clients.Where(p => p.Moniteur.NomMoniteur.Contains(motCle)).ToList().Count;
            if(nbreProduit % size ==0 ) { 
                 totalPages = nbreProduit/size;
            }else{
                totalPages = 1+ nbreProduit / size ;
            }
            ViewBag.totalPages = totalPages;
            ViewBag.motCle = motCle;
            return View("Clients", clients);
        }
        public IActionResult FormClient()
        {
            Client p = new Client();
            IEnumerable<Moniteur> cats = dbContext.Moniteurs.ToList();
            ViewBag.Moniteurs = cats;


            return View("FormClient", p);
        }

        [HttpPost]
        public IActionResult Save(Client p)
        {

            if (p.ClientID == 0)
            {
                dbContext.clients.Add(p);

                dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                Client prodToUpdate = dbContext.clients
                  .Where(prod => prod.ClientID == p.ClientID).FirstOrDefault();

                if (prodToUpdate != null)
                {

                    dbContext.Entry(prodToUpdate).CurrentValues.SetValues(p);
                    dbContext.SaveChanges();

                }

                return RedirectToAction("Index");

            }
        }

        public IActionResult Delete(int id)
        {
            var clients = dbContext.clients.Find(id);
            dbContext.clients.Remove(clients);
            dbContext.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int id)
        {
            var produit = dbContext.clients.Find(id);
            ViewBag.Moniteurs = dbContext.Moniteurs.ToList();
            return View(produit);

        }

        public IActionResult Examen(int id, Client c)
        {
            var clients = dbContext.clients.Find(id);
            ViewBag.Moniteurs = dbContext.Moniteurs.ToList();
            dbContext.SaveChanges();
            return View(clients);

        }

        //Espace Moniteur

        public IActionResult Moniteurs(int page = 0, int size = 5, string motCle = "")
        {
            int position = page * size;
            IEnumerable<Moniteur> moniteurs = dbContext.Moniteurs.Where(p => p.NomMoniteur.Contains(motCle)).Skip(position).Take(size).ToList();//c'est  la methode syntax link (on peut utiliser le query syntax)
            ViewBag.currentPage = page;
            int totalPages;
            int nbreProduit = dbContext.Moniteurs.Where(p => p.NomMoniteur.Contains(motCle)).ToList().Count;
            if (nbreProduit % size == 0)
            {
                totalPages = nbreProduit / size;
            }
            else
            {
                totalPages = 1 + nbreProduit / size;
            }
            ViewBag.totalPages = totalPages;
            ViewBag.motCle = motCle;
            return View("Moniteurs", moniteurs);
        }

        public IActionResult FormMoniteur()
        {
            Moniteur p = new Moniteur();
            return View("FormMoniteur", p);
        }

        [HttpPost]
        public IActionResult SaveMoniteur(Moniteur p)

        {
            if (p.MoniteurID == 0)
            {
                dbContext.Moniteurs.Add(p);

                dbContext.SaveChanges();
                return RedirectToAction("Moniteurs");

            }
            else
            {
                Moniteur prodToUpdate = dbContext.Moniteurs
                  .Where(prod => prod.MoniteurID == p.MoniteurID).FirstOrDefault();

                if (prodToUpdate != null)
                {

                    dbContext.Entry(prodToUpdate).CurrentValues.SetValues(p);
                    dbContext.SaveChanges();

                }

                return RedirectToAction("Moniteurs");

            }
        }
        public IActionResult EditMoniteur(int id)
        {
            var moniteurs = dbContext.Moniteurs.Find(id);
            return View(moniteurs);

        }

        public IActionResult DeleteMoniteur(int id)
        {
            var moniteurs = dbContext.Moniteurs.Find(id);
            dbContext.Moniteurs.Remove(moniteurs);
            dbContext.SaveChanges();
            return RedirectToAction("Moniteurs");

        }
        public IActionResult ClientsDetails(int id)
        {

            IEnumerable<Client> ClientsDetails = dbContext.clients
                .Where(p => p.MoniteurID == id)
                .ToList();

            return View("ClientsDetails", ClientsDetails);
        }


        //Espace Cours
        public IActionResult Cours(int page = 0, int size = 5, string motCle = "")
        {
            int position = page * size;
            IEnumerable<Client> clients = dbContext.clients.Where(p => p.Moniteur.NomMoniteur.Contains(motCle)).Skip(position).Take(size).Include(p => p.Moniteur).ToList();//c'est  la methode syntax link (on peut utiliser le query syntax)
            ViewBag.currentPage = page;
            int totalPages;
            int nbreProduit = dbContext.clients.Where(p => p.Moniteur.NomMoniteur.Contains(motCle)).ToList().Count;
            if (nbreProduit % size == 0)
            {
                totalPages = nbreProduit / size;
            }
            else
            {
                totalPages = 1 + nbreProduit / size;
            }
            ViewBag.totalPages = totalPages;
            ViewBag.motCle = motCle;

            return View("Cours", clients);
        }

        public IActionResult Confirmer(int id, Client c)
        {
            var clients = dbContext.clients.Find(id);
            ViewBag.Moniteurs = dbContext.Moniteurs.ToList();
            // string z = clients.HistoriqueSeance;
            // clients.HistoriqueSeance = z + clients.ProchaineSeance + ".";
            if (clients.HistoriqueSeance == "01/01/0001 00:00:00.") { clients.HistoriqueSeance = "."; }
            dbContext.SaveChanges();
            return View(clients);

        }
        public IActionResult Presence(int id, Client c)
        {
            var clients = dbContext.clients.Find(id);

            clients.Nbrheure++;
            if (clients.Categoriee == "A") { clients.MontantTotale = 15* clients.Nbrheure; }
            if (clients.Categoriee == "B") { clients.MontantTotale = 20 * clients.Nbrheure; }
            if (clients.Categoriee == "C") { clients.MontantTotale = 25 * clients.Nbrheure; }
            if (clients.Categoriee == "D") { clients.MontantTotale = 30 * clients.Nbrheure; }

            string z = clients.HistoriqueSeance;
            clients.HistoriqueSeance = z + clients.ProchaineSeance + ".";
            if (clients.HistoriqueSeance == "01/01/0001 00:00:00.") { clients.HistoriqueSeance = "."; }
            dbContext.SaveChanges();
            return View(clients);

        }

        [HttpPost]
        public IActionResult SaveSeance(Client p)
        {
            if (p.ClientID == 0)
            {
                dbContext.clients.Add(p);

                dbContext.SaveChanges();
                return RedirectToAction("Cours");
            }
            else
            {
                Client prodToUpdate = dbContext.clients
                  .Where(prod => prod.ClientID == p.ClientID).FirstOrDefault();

                if (prodToUpdate != null)
                {

                    dbContext.Entry(prodToUpdate).CurrentValues.SetValues(p);
                    dbContext.SaveChanges();

                }

                return RedirectToAction("Cours");
            }
        }



        //Espace Paiement
        public IActionResult Paiement(int page = 0, int size = 5, string motCle = "")
        {
            int position = page * size;
            IEnumerable<Client> clients = dbContext.clients.Where(p => p.Nom.Contains(motCle)).Skip(position).Take(size).Include(p => p.Moniteur).ToList();//c'est  la methode syntax link (on peut utiliser le query syntax)
            ViewBag.currentPage = page;
            int totalPages;
            int nbreProduit = dbContext.clients.Where(p => p.Nom.Contains(motCle)).ToList().Count;
            if (nbreProduit % size == 0)
            {
                totalPages = nbreProduit / size;
            }
            else
            {
                totalPages = 1 + nbreProduit / size;
            }
            ViewBag.totalPages = totalPages;
            ViewBag.motCle = motCle;
            
            return View("Paiement", clients);
        }

        



        /*public IActionResult Cours()
        {
            Client p = new Client();
            IEnumerable<Moniteur> cats = dbContext.Moniteurs.ToList();
            ViewBag.Moniteurs = cats;
            return View("Cours", p);
        }*/

        /* public IActionResult Cours()
         {
             Client p = new Client();
             IEnumerable<Moniteur> cats = dbContext.Moniteurs.ToList();
             ViewBag.Moniteurs = cats;
             return View("Cours", p);
         }

         */

        

        /*  if (ModelState.IsValid)
          {
              dbContext.clients.Add(p); 
              dbContext.SaveChanges();


              return RedirectToAction("Index");  
            //  return View("FormClient", p);
          }*/

        //    return RedirectToAction("Index");

        //return View("FormClient", p);



        
        public IActionResult MontantAjouter(int id)
         {
            var clients = dbContext.clients.Find(id);
             dbContext.SaveChanges();
            return View(clients);

         }
        /*  public IActionResult MontantAjouter(int id)
          {
              var clients = dbContext.clients.Find(id) ;
               dbContext.SaveChanges();
               clients = dbContext.clients.Find(id);
                return View(clients);
          }
        */
        /*public IActionResult Ajouter(int id)
        {
            var clients = dbContext.clients.Find(id);

            dbContext.SaveChanges();
            // return RedirectToAction("/Produit/Paiement");
            // return View(clients);
            //return View("/Produit/Paiement");


        }*/
      /*
        public IActionResult Payer(int id)
        {
           
            var clients = dbContext.clients.Find(id);
            
            int x = clients.paie;
            clients.paie = clients.MontantAjouter + x;
            
            string y = clients.HistoriqueDate;
            clients.HistoriqueDate = y + DateTime.Now + ".";

            string z = clients.HistoriquePaiement;
            clients.HistoriquePaiement = z+ clients.MontantAjouter +".";

            dbContext.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Paiement");

        }*/

        [HttpPost]
        public IActionResult SaveMontantAjouter(Client p)
        {
            if (p.ClientID == 0)
            {
                dbContext.clients.Add(p);

                dbContext.SaveChanges();
                return RedirectToAction("Paiement");
               // return View("MontantAjouter", p);
            }
            else
            {
                Client prodToUpdate = dbContext.clients
                  .Where(prod => prod.ClientID == p.ClientID).FirstOrDefault();

                if (prodToUpdate != null)
                {

                    dbContext.Entry(prodToUpdate).CurrentValues.SetValues(p);
                    
                    int x = prodToUpdate.paie;
                    prodToUpdate.paie = prodToUpdate.MontantAjouter + x;

                    string y = prodToUpdate.HistoriqueDate;
                    prodToUpdate.HistoriqueDate = y + DateTime.Now + ".";

                    string z = prodToUpdate.HistoriquePaiement;
                    prodToUpdate.HistoriquePaiement = z + prodToUpdate.MontantAjouter + ".";
                    dbContext.SaveChanges();

                }

                return RedirectToAction("Paiement");
                // return View("MontantAjouter", p);
                //return View("MontantAjouter", p);
            }

        }
        public IActionResult Accueil()
        {
            
            return View();
        }

        
        //Espace test
        public IActionResult PaiementDate(int id)
        {
            Paiement p = new Paiement();
            p.ClientID = id;
   
            //IEnumerable<Client> cats = dbContext.clients.ToList();
            //ViewBag.clients = cats;
            return View("PaiementDate", p);
        }

        [HttpPost]
        public IActionResult SaveDate(Paiement p)

        {
            if (ModelState.IsValid)
            {
             
                dbContext.Add(p);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("PaiementDate", p);
        }











    }

}
