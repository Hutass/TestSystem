﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;

    public partial class Answer
    {
        public int ID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public string Text { get; set; }
        public Nullable<double> Cost { get; set; }

        public virtual Question Question { get; set; }
    }
}