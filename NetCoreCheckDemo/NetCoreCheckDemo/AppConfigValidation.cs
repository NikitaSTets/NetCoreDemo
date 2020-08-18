using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;

namespace NetCoreCheckDemo
{
    public class AppConfigValidation : IValidateOptions<AppSettings>
    {
        public ValidateOptionsResult Validate(string name, AppSettings options)
        {
            //string errors = null;
            //var rx = new Regex(@"^[a-zA-Z''-'\s]{1,2}$");
            //var match = rx.Match(options.FirstName);

            //if (string.IsNullOrEmpty(match.Value))
            //{
            //    errors = $"{options.FirstName} doesn't match RegEx \n";
            //}

            //if (options.LastName.Length > 10 && options.LastName.Length < 1000)
            //{
            //    errors = $"{options.LastName} doesn't match Range 10 - 1000 \n";
            //}

            //if (errors != null)
            //{
            //    return ValidateOptionsResult.Fail(errors);
            //}

            return ValidateOptionsResult.Success;
        }
    }
}
