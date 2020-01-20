using System;
using System.Linq;
using System.Text.RegularExpressions;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Models.enums;

namespace HospitalApi.Filters
{
    public class PatientValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Patient patient = null;
            var error = new ErrorResponse();
            foreach (var argument in context.ActionArguments.Values.Where(v => v is Patient))
            {
                patient = argument as Patient;
                break;
            }
            if (patient == null)
                error.AddError(nameof(patient), "Body is not a json representing patient data");
            else
            {
                if (string.IsNullOrEmpty(patient.Name))
                    error.AddError(nameof(patient.Name), "Field is mandatory");

                if (string.IsNullOrEmpty(patient.FileNo))
                    error.AddError(nameof(patient.FileNo), "Field is mandatory");
                else
                {
                    if (!int.TryParse(patient.FileNo, out int fileNumber))
                        error.AddError(nameof(patient.FileNo), "Field is not in correct format");
                }

                if (string.IsNullOrEmpty(patient.Gender))
                    error.AddError(nameof(patient.Name), "Field is mandatory");
                else
                {
                    if (!Enum.TryParse(patient.Gender, out Gender sex))
                        error.AddError(nameof(patient.Gender), "Field is not in correct format");
                }

                if (!string.IsNullOrEmpty(patient.Vip))
                {
                    if (!bool.TryParse(patient.Vip, out bool isVip))
                        error.AddError(nameof(patient.Vip), "Field is not in correct format");
                }

                if (string.IsNullOrEmpty(patient.PhoneNumber))
                    error.AddError(nameof(patient.PhoneNumber), "Field is mandatory");
                else
                {
                    if (!IsValidPhoneNumber(patient.PhoneNumber))
                        error.AddError(nameof(patient.PhoneNumber), "Field is not in correct format");
                }

                if (!string.IsNullOrEmpty(patient.Email) && !IsValidEmail(patient.Email))
                    error.AddError(nameof(patient.Email), "Field is not in correct format");


                if (!string.IsNullOrEmpty(patient.Birthdate))
                    if (!IsValidDateTime(patient.Birthdate))
                        error.AddError(nameof(patient.Birthdate), "Field is not in correct format");

                if (!string.IsNullOrEmpty(patient.FirstVisitDate))
                    if (!IsValidDateTime(patient.FirstVisitDate))
                        error.AddError(nameof(patient.FirstVisitDate), "Field is not in correct format");

            }
            if (error.Errors.Any())
            {
                context.Result = new BadRequestObjectResult(error);
                return;
            }
        }
        bool IsValidPhoneNumber(string phone)
        {
            Regex regex = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            return regex.Match(phone).Success;
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool IsValidDateTime(string dateTime)
        {
            return DateTime.TryParse(dateTime, out DateTime temp);
        }
    }
}
