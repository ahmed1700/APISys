using System;
using System.Text;
using Microsoft.Net.Http.Headers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Entities.DataTransferObjects;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace CompanyEmployees.Custom
{
    public class CsVOutputFormatter : TextOutputFormatter
    {
        public CsVOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(CompanyDto).IsAssignableFrom(type) || typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<CompanyDto>)
            {
                foreach (var company in (IEnumerable<CompanyDto>)context.Object)
                {
                    FormatCSV(buffer, company);
                }
            }
            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCSV( StringBuilder buffer , CompanyDto company)
        {
            buffer.AppendLine($"{company.Id},\"{company.Name},\"{company.FullAdress}");
        }
    }
}

