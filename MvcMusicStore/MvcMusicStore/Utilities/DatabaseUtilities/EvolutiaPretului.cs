//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcMusicStore.Utilities.DatabaseUtilities
{
    using System;
    using System.Collections.Generic;
    
    public partial class EvolutiaPretului
    {
        public System.Guid Id { get; set; }
        public decimal Pret { get; set; }
        public Nullable<System.Guid> Id_Produs { get; set; }
        public Nullable<System.DateTime> Data_Actualizare { get; set; }
    
        public virtual Produs Produs { get; set; }
    }
}
