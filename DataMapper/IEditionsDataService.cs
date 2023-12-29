using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface IEditionDataService
    {
        IList<Edition> GetAllEditions();

        Edition GetEditionById(int id);

        void AddEdition(Edition Edition);

        void DeleteEdition(Edition Edition);

        void UpdateEdition(Edition Edition);
    }
}
