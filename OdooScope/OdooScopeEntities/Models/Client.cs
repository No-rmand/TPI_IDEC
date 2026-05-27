using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace OdooScopeEntities.Entities
{
    [ModelMetadataTypeAttribute(typeof(ClientMetaData))]
    public partial class Client { }

    public partial class ClientMetaData
    {
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "L'e-mail est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'e-mail n'est pas au bon format.")]
        public string Email { get; set; }

        [Range(1, 999, ErrorMessage = "Le nombre d'employés doit être un chiffre positif.")]
        public int NombreEmploye { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un secteur d'activité.")]
        public int SecteurActiviteId { get; set; }
    }
}
