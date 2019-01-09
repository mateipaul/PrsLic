namespace DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AparitieProdus
    {
        public Guid Id { get; set; }

        public Guid? Id_Produs { get; set; }

        public Guid? Id_Cautare { get; set; }

        public virtual IstoricCautari IstoricCautari { get; set; }

        public virtual Produs Produs { get; set; }
    }
}
