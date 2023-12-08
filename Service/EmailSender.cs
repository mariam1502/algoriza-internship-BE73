using Data;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmailSender:IEmailSender
    {
        private readonly IRepository<Doctor> doctorRepo;
        public EmailSender(IRepository<Doctor> doctorRepo)
        {
            this.doctorRepo = doctorRepo;
        }
        public async Task SendEmailAsync(string doctorId)
        {
            string adminMail = (await doctorRepo.GetById(string_id: "1")).Email;
            string subject = "Doctor Account";
            Doctor doctor = await doctorRepo.GetById(string_id: doctorId);
            if(doctor!=null)
            {
                string doctorEmail = doctor.Email;
                string message = "Hello " + doctor.FirstName + " " + doctor.LastName+"\n" + "your user name is " + doctor.UserName + " your password is " + doctor.PasswordHash;
                string adminPassword = "ogxz cujb yeov onah";

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false; 
                    client.Credentials = new NetworkCredential(adminMail, adminPassword);
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Timeout = 10000;

                    await client.SendMailAsync(
                        new MailMessage(from: adminMail, to: doctorEmail, subject: subject, body: message)
                    );
                }

            }
         
        }

    }
}
