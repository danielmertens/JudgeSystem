using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JudgeSystem.Web.Formatters
{
    public class ByteArrayInputFormatter : InputFormatter
    {
        public ByteArrayInputFormatter()
        {
            SupportedMediaTypes.Add(Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream"));
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(byte[]);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var stream = new MemoryStream();
            context.HttpContext.Request.Body.CopyToAsync(stream);
            return InputFormatterResult.SuccessAsync(stream.ToArray());
        }
    }
}