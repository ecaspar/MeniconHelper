//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeniconHelper
{
    using System;
    using System.Collections.Generic;
    
    public partial class task
    {
        public int id_task { get; set; }
        public string comment { get; set; }
        public System.DateTime date_create { get; set; }
        public System.DateTime date_close { get; set; }
        public int id_anomaly { get; set; }
    
        public virtual incident incident { get; set; }
    }
}
