using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airbox.Api.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Get any validation error messages.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns>A list of vaidation errors or an empty list.</returns>
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors)
                             .Select(m => m.ErrorMessage)
                             .ToList();
        }
    }
}
