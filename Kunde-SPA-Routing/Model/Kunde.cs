using System;
using System.ComponentModel.DataAnnotations;

namespace KundeApp2.Model
{
     public class Kunde
     {
        public int Id { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string Fornavn { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string Etternavn { get; set; }
        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string Adresse { get; set; }
        [RegularExpression(@"[0-9]{4}")]
        public string Postnr { get; set; }
        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,30}")]
        public string Poststed { get; set; }
    }
}
