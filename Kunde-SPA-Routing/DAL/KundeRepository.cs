using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KundeApp2.Model;
using Microsoft.EntityFrameworkCore;

namespace KundeApp2.DAL
{
        public class KundeRepository : IKundeRepository
    {
        private readonly KundeContext _db;

        public KundeRepository(KundeContext db)
        {
            _db = db;
        }

        public async Task<bool> Lagre(Kunde innKunde)
        {
            try
            {
                var nyKundeRad = new Kunder();
                nyKundeRad.Fornavn = innKunde.Fornavn;
                nyKundeRad.Etternavn = innKunde.Etternavn;
                nyKundeRad.Adresse = innKunde.Adresse;

                var sjekkPostnr = await _db.Poststeder.FindAsync(innKunde.Postnr);
                if (sjekkPostnr == null)
                {
                    var poststedsRad = new Poststeder();
                    poststedsRad.Postnr = innKunde.Postnr;
                    poststedsRad.Poststed = innKunde.Poststed;
                    nyKundeRad.Poststed = poststedsRad;
                }
                else
                {
                    nyKundeRad.Poststed = sjekkPostnr;
                }
                _db.Kunder.Add(nyKundeRad);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<List<Kunde>> HentAlle()
        {
            try
            {
                List<Kunde> alleKunder = await _db.Kunder.Select(k => new Kunde
                {
                    Id = k.Id,
                    Fornavn = k.Fornavn,
                    Etternavn = k.Etternavn,
                    Adresse = k.Adresse,
                    Postnr = k.Poststed.Postnr,
                    Poststed = k.Poststed.Poststed
                }).ToListAsync();
                return alleKunder;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Slett(int id)
        {
            try
            {
                Kunder enDBKunde = await _db.Kunder.FindAsync(id);
                _db.Kunder.Remove(enDBKunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Kunde> HentEn(int id)
        {
            Kunder enKunde = await _db.Kunder.FindAsync(id);
            var hentetKunde = new Kunde()
            {
                Id = enKunde.Id,
                Fornavn = enKunde.Fornavn,
                Etternavn = enKunde.Etternavn,
                Adresse = enKunde.Adresse,
                Postnr = enKunde.Poststed.Postnr,
                Poststed = enKunde.Poststed.Poststed
            };
            return hentetKunde;
        }

        public async Task<bool> Endre(Kunde endreKunde)
        {
            try
            {
                var endreObjekt = await _db.Kunder.FindAsync(endreKunde.Id);
                if (endreObjekt.Poststed.Postnr != endreKunde.Postnr)
                {
                    var sjekkPostnr = _db.Poststeder.Find(endreKunde.Postnr);
                    if (sjekkPostnr == null)
                    {
                        var poststedsRad = new Poststeder();
                        poststedsRad.Postnr = endreKunde.Postnr;
                        poststedsRad.Poststed = endreKunde.Poststed;
                        endreObjekt.Poststed = poststedsRad;
                    }
                    else
                    {
                        endreObjekt.Poststed.Postnr = endreKunde.Postnr;
                    }
                }
                endreObjekt.Fornavn = endreKunde.Fornavn;
                endreObjekt.Etternavn = endreKunde.Etternavn;
                endreObjekt.Adresse = endreKunde.Adresse;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}