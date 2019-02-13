﻿using SmartSolucionesCuba.SAPRESSC.Core.Web.Management.Models.View;
using SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Models.View
{
    public class NautaBalanceTuneUpProfileInputViewModel : IEntityInputViewModel<NautaBalanceTuneUpProfile, int>
    {
        public int Id { get ; set ; }
        
        [Display(Name = "Habilitado")]
        public bool Enabled { get; set; }

        [Required]
        [Display(Name = "Monto")]
        public float Amount { get; set; }

        public string Label { get; set; }

        [Display(Name = "Primario")]
        public bool Primary { get; set; }

        public NautaBalanceTuneUpProfile Export()
        {
            var entity = new NautaBalanceTuneUpProfile();
            Mergue(entity);
            return entity;
        }

        public void Import(NautaBalanceTuneUpProfile entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Amount = entity.Amount;
            Label = entity.Label;
            Primary = entity.Primary;
        }       

        public void Mergue(NautaBalanceTuneUpProfile entity)
        {
            entity.Enabled = Enabled;
            entity.Amount = Amount;
            entity.Label = Label;
            entity.Primary = Primary;
        }
    }
}