﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Interface.person
{
   public interface IPersonLocationRepository :IRepository<PersonLocation>
   {
       List<PersonLocation> GetPersonCurrentLocation(int personId);
   }
}
