using Media.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Abstractions.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
