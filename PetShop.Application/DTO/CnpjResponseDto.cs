using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.DTO
{
    public class CnpjResponseDto
    {
        

        public string cnpj { get; set; }
        public string razao_social { get; set; }
        public string nome_fantasia { get; set; }
        public string descricao_situacao_cadastral { get; set; }
        public int cep { get; set; }
        public string uf { get; set; }
        public string logradouro { get; set; }
        public string municipio { get; set; }
        public string ddd_telefone_1 { get; set; }
        public int capital_social { get; set; }
        public string descricao_porte { get; set; }
    }
}
