//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestClothesForHands.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaterialSupp
    {
        public int ID { get; set; }
        public Nullable<int> IdMaterial { get; set; }
        public Nullable<int> IdSupplier { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}