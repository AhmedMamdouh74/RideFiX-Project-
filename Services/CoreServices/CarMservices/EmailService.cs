using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceAbstraction.CoreServicesAbstractions.CarMservices;

namespace Service.CoreServices.CarMservices
{
    public class EmailService : IEmailService
    {
       
        public async Task SendEmail(string toEmail , string maintananceType, string ownername, DateOnly maintananceDate)
        { 
            string subject = string.Empty;
            string body = string.Empty;
            switch (maintananceType)
            {
                case "تغيير زيت المحرك":
                    subject = "تذكير بموعد صيانة تغيير الزيت لسيارتك";
                    body = $@"
                    مرحبًا {ownername},

                    نود أن نُذكّرك بموعد صيانة تغيير الزيت لسيارتك. نحن نوصي بتغيير الزيت بانتظام للحفاظ على أداء محرك سيارتك وزيادة عمره الافتراضي.

                    تفاصيل الموعد:
                    - نوع الخدمة: تغيير الزيت
                    - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}
              

                    مع أطيب التحيات,
                    فريق ride fix"; ;
                    break;
 
                case "تغيير فلتر الزيت":
                    subject = "تذكير بموعد صيانة سيارتك";
                    body = $@"
                        مرحبًا {ownername},

                        نود أن نُذكّرك بموعد صيانة تغيير فلتر الزيت لسيارتك. نحن نوصي بتغيير الفلتر بانتظام للحفاظ على أداء محرك سيارتك وزيادة عمره الافتراضي.

                        تفاصيل الموعد:
                        - نوع الخدمة: تغيير فلتر الزيت
                        - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}

                        مع أطيب التحيات,
                        فريق RideFix";
                    break;

                case "تغيير فلتر الهواء":
                    subject = "تذكير بموعد صيانة تغيير فلتر الهواء لسيارتك";
                    body = $@"
                    مرحبًا {ownername},
                    نود أن نُذكّرك بموعد صيانة تغيير فلتر الهواء لسيارتك. نحن نوصي بتغيير الفلتر بانتظام للحفاظ على جودة الهواء داخل السيارة وأداء المحرك.
                    تفاصيل الموعد:
                    - نوع الخدمة: تغيير فلتر الهواء
                    - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}
                    مع أطيب التحيات,
                    فريق RideFix";
                    break;

                case "تغيير فلتر التكييف":
                    subject = "تذكير بموعد صيانة تغيير فلتر التكييف لسيارتك";
                    body = $@"
                    مرحبًا {ownername},
                    نود أن نُذكّرك بموعد صيانة تغيير فلتر التكييف لسيارتك. نحن نوصي بتغيير الفلتر بانتظام للحفاظ على جودة الهواء داخل السيارة وأداء نظام التكييف.
                    تفاصيل الموعد:
                    - نوع الخدمة: تغيير فلتر التكييف
                    - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}
                    مع أطيب التحيات,
                    فريق RideFix";
                    break;

                case "تغيير تيل الفرامل":
                    subject = "تذكير بموعد صيانة تغيير تيل الفرامل لسيارتك";
                    body = $@"
                    مرحبًا {ownername},
                    نود أن نُذكّرك بموعد صيانة تغيير تيل الفرامل لسيارتك. نحن نوصي بتغيير التيل بانتظام للحفاظ على سلامتك وأداء نظام الفرامل.
                    تفاصيل الموعد:
                    - نوع الخدمة: تغيير تيل الفرامل
                    - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}
                    مع أطيب التحيات,
                    فريق RideFix";
                    break;

                case "فحص سائل التبريد":
                    subject = "تذكير بموعد فحص سائل التبريد لسيارتك";
                    body = $@"
                    مرحبًا {ownername},
                    نود أن نُذكّرك بموعد فحص سائل التبريد لسيارتك. نحن نوصي بفحص السائل بانتظام للحفاظ على درجة حرارة المحرك وأدائه.
                    تفاصيل الموعد:
                    - نوع الخدمة: فحص سائل التبريد
                    - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}
                    مع أطيب التحيات,
                    فريق RideFix";
                    break;

                case "فحص البطارية":
                    subject = "تذكير بموعد فحص البطارية لسيارتك";
                    body = $@"
                    مرحبًا {ownername},
                    نود أن نُذكّرك بموعد فحص البطارية لسيارتك. نحن نوصي بفحص البطارية بانتظام للتأكد من أنها تعمل بشكل صحيح.
                    تفاصيل الموعد:
                    - نوع الخدمة: فحص البطارية
                    - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}
                    مع أطيب التحيات,
                    فريق RideFix";
                    break;

                case "ضبط الزوايا والترصيص":
                    subject = "تذكير بموعد ضبط الزوايا والترصيص لسيارتك";
                    body = $@"
                    مرحبًا {ownername},
                    نود أن نُذكّرك بموعد ضبط الزوايا والترصيص لسيارتك. نحن نوصي بهذا الإجراء للحفاظ على استقرار السيارة وأداء العجلات.
                    تفاصيل الموعد:
                    - نوع الخدمة: ضبط الزوايا والترصيص
                    - تاريخ الموعد: {maintananceDate:dd/MM/yyyy}
                    مع أطيب التحيات,
                    فريق RideFix";
                    break;
                default:
                    throw new ArgumentException("نوع الصيانة غير معروف");
            }

            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("a.elkholy2711@gmail.com", "scepygusljwhzmel");


                var mailMessage = new MailMessage("a.elkholy2711@gmail.com", toEmail, subject, body);
                client.Send(mailMessage);
            }
        }

    }
   
}
