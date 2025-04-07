using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public static class TransfromationMapping
    {
        public static Transformation ToTransformation(this Transformation transformation) => new Transformation
        {
            id = transformation.id,
            Name = transformation.Name,
            Ki = transformation.Ki
        };
    }
}
