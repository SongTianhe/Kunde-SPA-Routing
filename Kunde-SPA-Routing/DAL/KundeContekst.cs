using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace KundeApp2.DAL
{
    public class Kunder
    {
        public int Id { get; set; }  // gir en primærnøkkel med autoincrement fordi attributten heter noe med "id"
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public string Adresse { get; set; }
        virtual public Poststeder Poststed { get; set; }  // viritual gjør at Poststedet blir hentet sammen med Kunden med LazyLoading 
    }
    
    public class Poststeder
    {
        [Key]  
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // dette for å ikke lage en autoincrement på primærnøkkelen
        public string Postnr { get; set; }
        public string Poststed { get; set; }

        // denne listen ikke nødvendig med mindre man skal finne kundene på et gitt postnr (altså gå inn via Poststeder-collection)
        virtual public List<Kunder> Kunder { get; set; }  
    }

    public class KundeContext : DbContext
    {
            public KundeContext (DbContextOptions<KundeContext> options)
                    : base(options)
            {
                // setningen under brukes for å opprette databasen fysisk dersom den ikke er opprettet
                // dette er uavhenig av initiering av databasen, se DBInit(seed)
                // når man endrer på strukturen på KundeContext er det fornuftlig å slette "Kunde.Db" fysisk før nye kjøringer
                Database.EnsureCreated();
        }

        public DbSet<Kunder> Kunder { get; set; }
        public DbSet<Poststeder> Poststeder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // må importere pakken Microsoft.EntityFrameworkCore.Proxies
            // og legge til"viritual" på de attriuttene som ønskes å lastes automatisk (LazyLoading)
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
