using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Http.ModelBinding;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace RAP.API.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Converts the Fluent Validation result to the type the both mvc and ef expect
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns>IEnumerable<ValidationResult></returns>
        public static IEnumerable<ValidationResult> ToValidationResult(
            this FluentValidation.Results.ValidationResult validationResult)
        {
            var results = validationResult.Errors.Select(item => new ValidationResult(item.ErrorMessage, new List<string> { item.PropertyName }));
            //var results = validationResult.Errors.Select(item => new ValidationResult(item.ErrorMessage));
            return results;
        }

        /// <summary>
        /// Converts ModelState validation messages into Dictionary<string, string[]>
        /// </summary>
        /// <param name="modelState">The ModelState</param>
        /// <returns>Dictionary<string, string[]></returns>
        public static Dictionary<string, List<string>> Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => (kvp.Key.Split('.').Count() > 1 ? kvp.Key.Split('.')[1] : kvp.Key),
                                               kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList());
            }
            return null;
        }

        /// <summary>
        /// Update View Model
        /// </summary>
        /// <param name="dataTransformationObject"></param>
        /// <param name="businessObject"></param>
        public static void UpdateViewModel(object dataTransformationObject, object businessObject)
        {
            Type targetType = businessObject.GetType();
            Type sourceType = dataTransformationObject.GetType();

            PropertyInfo[] sourceProps = sourceType.GetProperties();
            foreach (var propInfo in sourceProps)
            {
                //Get the matching property from the target
                PropertyInfo toProp = (targetType == sourceType) ? propInfo : targetType.GetProperty(propInfo.Name);

                //If it exists and it's writeable
                if (toProp != null && toProp.CanWrite)
                {
                    //Copy the value from the source to the target
                    Object value = propInfo.GetValue(dataTransformationObject, null);
                    toProp.SetValue(businessObject, value, null);
                }
            }
        }
    }
}