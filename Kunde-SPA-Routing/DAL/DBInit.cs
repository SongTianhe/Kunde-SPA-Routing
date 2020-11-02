using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KundeApp2.DAL
{
    public static class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();
            
            var db = serviceScope.ServiceProvider.GetService<KundeContext>();

            // må slette og opprette databasen hver gang når den skal initieres (seed`es)
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var poststed1 = new Poststeder
            {
                Postnr = "0270",
                Poststed = "Oslo"}
            ;
            var poststed2 = new Poststeder
            {
                Postnr = "1370",
                Poststed = "Asker"
            };

            var kunde1 = new Kunder
            {
                Fornavn = "Ole",
                Etternavn = "Hansen",
                Adresse = "Osloveien 82",
                Poststed = poststed1
            };

            var kunde2 = new Kunder
            {
                Fornavn = "Line",
                Etternavn = "Jensen",
                Adresse = "Askerveien 72",
                Poststed = poststed2
            };

            var kunde3 = new Kunder
            {
                Fornavn = "Finn",
                Etternavn = "Finnsen",
                Adresse = "Drammensveien 1",
                Poststed = poststed1
            };

            db.Kunder.Add(kunde1);
            db.Kunder.Add(kunde2);
            db.Kunder.Add(kunde3);

            db.SaveChanges();
        }
    }  
}
