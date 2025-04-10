using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public static class TransfromationMapping
    {
        public static Transformation ToEntity(this TransformationDto transformation) => new Transformation
        {
            Name = transformation.Name,
            Ki = transformation.Ki
        };
    }
}
