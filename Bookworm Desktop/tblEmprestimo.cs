//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bookworm_Desktop
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEmprestimo
    {
        public int IDEmprestimo { get; set; }
        public int IDFuncionario { get; set; }
        public int IDProduto { get; set; }
        public int IDLeitor { get; set; }
        public System.DateTime DataRetirada { get; set; }
        public System.DateTime DataEntrega { get; set; }
        public int Renovacao { get; set; }
    
        public virtual tblFuncionario tblFuncionario { get; set; }
        public virtual tblLeitor tblLeitor { get; set; }
        public virtual tblProduto tblProduto { get; set; }
    }
}