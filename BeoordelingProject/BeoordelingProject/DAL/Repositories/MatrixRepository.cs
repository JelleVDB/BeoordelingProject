﻿using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Repositories
{
    public class MatrixRepository : GenericRepository<Matrix>, BeoordelingProject.DAL.Repositories.IMatrixRepository
    {
        public MatrixRepository(BeoordelingsContext context) : base(context)
        {

        }

        public IEnumerable<Matrix> GetMatrixByID(int id)
        {            
            var query =
            (
            from m in context.Matrices
            from h in m.Hoofdaspecten
            from d in h.Deelaspecten
            
            where m.ID.Equals(id)

            select m
            );

            return query;
        }

    }
}