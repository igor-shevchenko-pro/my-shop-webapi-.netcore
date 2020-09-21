using Microsoft.Extensions.Configuration;
using MyShop.ApiModels;
using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Managers.LetterManagers.Base;
using MyShop.Core.Managers.LetterManagers.Base;
using MyShop.Core.Models.Base;

namespace MyShop.Core.Managers.Base.LetterManagers
{
    public class EmailContentBuilderManager : LetterBuilderManager, IEmailContentBuilderManager
    {
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;
        private readonly string _routeGetFile;

        string mainlogo = "main-logo";

        public EmailContentBuilderManager(ITextLetterBuilderManager textLetterBuilderManager, IConfiguration configuration)
            : base(textLetterBuilderManager)
        {
            _configuration = configuration;
            _baseUrl = _configuration["FileStore:BaseUrl"];
            _routeGetFile = _configuration["FileStore:GetFile"];
        }

        protected override string GetWrapper(LetterTextParamsModel letterParams)
        {
            if(letterParams.LetterLanguage == UserLanguageEnum.English)
            {
                return @"<html>
                            <head>
                              <style>
                                @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');
                                @import url('https://fonts.googleapis.com/css2?family=Carter+One&display=swap');
                                body {
                                  font-family: Poppins, sans-serif;
                                }
                                #outlook a {
                                  padding: 0;
                                }
                                .ReadMsgBody {
                                  width: 100%;
                                }
                                .ExternalClass {
                                  width: 100%;
                                }
                                .ExternalClass,
                                .ExternalClass p,
                                .ExternalClass span,
                                .ExternalClass font,
                                .ExternalClass td,
                                .ExternalClass div {
                                  line-height: 100%;
                                }
                                body,
                                table,
                                td,
                                a {
                                  -webkit-text-size-adjust: 100%;
                                  -ms-text-size-adjust: 100%;
                                }
                                table,
                                td {
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                }
                                img {
                                  -ms-interpolation-mode: bicubic;
                                }
                                body {
                                  height: 100% !important;
                                  margin: 0;
                                  padding: 0;
                                  width: 100% !important;
                                }
                                img {
                                  border: 0;
                                  height: auto;
                                  line-height: 100%;
                                  outline: none;
                                  text-decoration: none;
                                } 
                                table {
                                  border-collapse: collapse !important;
                                }
                                @media screen and(max-width: 600px)
                                {
                                  td[class='logo'] {
                                    padding: 6px 0 2px 0! important;
                                  }
                                  td[class='logo'] img {
                                    width: 62px !important;
                                  }
                                  table[class='wrapper'] {
                                    width: 100% !important;
                                  }
                                  td[class='mobile-text-hi1'] {
                                    padding: 19px 0 0 0 !important;
                                    font-size: 18px !important;
                                    line-height: 27px !important;
                                  }
                                  td[class='mobile-text-hi2'] {
                                    padding: 0 0 33px 0 !important;
                                    font-size: 18px !important;
                                    line-height: 27px!important;
                                  }
                                  td[class='card'] {
                                    padding: 23px 16px 26px 16px !important;
                                  }
                                  td[class='mobile-title'] {
                                    padding: 33px 10px 6px 10px !important;
                                    font-size: 24px !important;
                                    line-height: 36px !important;
                                  }
                                  td[class='mobile-subtitle'] {
                                    padding: 6px 25px 10px 25px !important;
                                    font-size: 14px !important;
                                    line-height: 21px !important;
                                  }
                                  td[class='mobile-button'] {
                                    padding: 19px 0 46px 0 !important;
                                  }
                                  td[class='mobile-button'] a {
                                    font-size: 14px !important;
                                    line-height: 21px !important;
                                    border-top: 15px solid #FFA800 !important;
                                    border-bottom: 15px solid #FFA800 !important;
                                    border-right: 48px solid #FFA800 !important;
                                    border-left: 48px solid #FFA800 !important;
                                  }
                                  td[class='mobile-question'] {
                                    padding: 73px 0 12px 0 !important;
                                    font-size: 16px !important;
                                    line-height: 24px !important;
                                  }
                                  td[class='mobile-response'] {
                                    padding: 2px 0 10px 0 !important;
                                    font-size: 16px !important;
                                    line-height: 24px !important;
                                  }
                                  td[class='mobile-link'] {
                                    padding: 25px 0 4px 0 !important;
                                  }
                                }
                              </style>
                            </head>
                            <body style='margin: 0; padding: 0; background-color: #eeeeee;'>
                              <table border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;'>
                                <tr>
                                  <td align='center' bgcolor='#eeeeee'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='600' class='wrapper'>
                                      <tr>
                                        <td bgcolor='#ffffff' style='padding: 23px 56px 26px 56px;' class='card'>
                                          <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                            <tr>
                                              <td align='center' class='logo'>
                                                <img src='" + _baseUrl + _routeGetFile + mainlogo + @"' alt='logo' width='74' border='0' style='display: block;'/>
                                              </td>
                                            </tr>
                                            <tr>" + GetText(letterParams) + @"</tr>   
                                            <tr>
                                              <td align='center' style='padding: 14px 0 3px 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 700; font-size: 18px; line-height: 27px;' class='mobile-question'>
                                                Didn’t request this email?
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 2px 25px 5px 25px; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 500; font-size: 14px; line-height: 21px;' class='mobile-response'>
                                                  No worries! Your address may have been entered by mistake.
                                                  If you ignore or delete this email, nothing further will happen.
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 14px 0 4px 0; color: #000000; font-family: Carter One, cursive; font-weight: normal; font-size: 18px; line-height: 27px;' class='mobile-link'>
                                                <a href='#' style='color: #FFA800; text-decoration: none;'>(Shop title)</a>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 2px 0 0 0; color: #222222; font-family: Poppins, Arial, sans-serif; font-weight: 500; font-size: 12px; line-height: 18px;'>
                                                © (Shop title) by<strong> Igor Shevchenko.</strong>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 0 0 2px 0; color: #222222; font-family: Poppins, Arial, sans-serif; font-weight: 500; font-size: 12px; line-height: 18px;'>
                                                All Rights Reserved.
                                              </td>
                                            </tr>
                                          </table>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </body>
                        </html>";
            }
            else
            {
                return @"<html>
                            <head>
                              <style>
                                @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap');
                                @import url('https://fonts.googleapis.com/css2?family=Carter+One&display=swap');
                                body {
                                  font-family: Poppins, sans-serif;
                                }
                                #outlook a {
                                  padding: 0;
                                }
                                .ReadMsgBody {
                                  width: 100%;
                                }
                                .ExternalClass {
                                  width: 100%;
                                }
                                .ExternalClass,
                                .ExternalClass p,
                                .ExternalClass span,
                                .ExternalClass font,
                                .ExternalClass td,
                                .ExternalClass div {
                                  line-height: 100%;
                                }
                                body,
                                table,
                                td,
                                a {
                                  -webkit-text-size-adjust: 100%;
                                  -ms-text-size-adjust: 100%;
                                }
                                table,
                                td {
                                  mso-table-lspace: 0pt;
                                  mso-table-rspace: 0pt;
                                }
                                img {
                                  -ms-interpolation-mode: bicubic;
                                }
                                body {
                                  height: 100% !important;
                                  margin: 0;
                                  padding: 0;
                                  width: 100% !important;
                                }
                                img {
                                  border: 0;
                                  height: auto;
                                  line-height: 100%;
                                  outline: none;
                                  text-decoration: none;
                                } 
                                table {
                                  border-collapse: collapse !important;
                                }
                                @media screen and(max-width: 600px)
                                {
                                  td[class='logo'] {
                                    padding: 6px 0 2px 0! important;
                                  }
                                  td[class='logo'] img {
                                    width: 62px !important;
                                  }
                                  table[class='wrapper'] {
                                    width: 100% !important;
                                  }
                                  td[class='mobile-text-hi1'] {
                                    padding: 19px 0 0 0 !important;
                                    font-size: 18px !important;
                                    line-height: 27px !important;
                                  }
                                  td[class='mobile-text-hi2'] {
                                    padding: 0 0 33px 0 !important;
                                    font-size: 18px !important;
                                    line-height: 27px!important;
                                  }
                                  td[class='card'] {
                                    padding: 23px 16px 26px 16px !important;
                                  }
                                  td[class='mobile-title'] {
                                    padding: 33px 10px 6px 10px !important;
                                    font-size: 24px !important;
                                    line-height: 36px !important;
                                  }
                                  td[class='mobile-subtitle'] {
                                    padding: 6px 25px 10px 25px !important;
                                    font-size: 14px !important;
                                    line-height: 21px !important;
                                  }
                                  td[class='mobile-button'] {
                                    padding: 19px 0 46px 0 !important;
                                  }
                                  td[class='mobile-button'] a {
                                    font-size: 14px !important;
                                    line-height: 21px !important;
                                    border-top: 15px solid #FFA800 !important;
                                    border-bottom: 15px solid #FFA800 !important;
                                    border-right: 48px solid #FFA800 !important;
                                    border-left: 48px solid #FFA800 !important;
                                  }
                                  td[class='mobile-question'] {
                                    padding: 73px 0 12px 0 !important;
                                    font-size: 16px !important;
                                    line-height: 24px !important;
                                  }
                                  td[class='mobile-response'] {
                                    padding: 2px 0 10px 0 !important;
                                    font-size: 16px !important;
                                    line-height: 24px !important;
                                  }
                                  td[class='mobile-link'] {
                                    padding: 25px 0 4px 0 !important;
                                  }
                                }
                              </style>
                            </head>
                            <body style='margin: 0; padding: 0; background-color: #eeeeee;'>
                              <table border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;'>
                                <tr>
                                  <td align='center' bgcolor='#eeeeee'>
                                    <table border='0' cellpadding='0' cellspacing='0' width='600' class='wrapper'>
                                      <tr>
                                        <td bgcolor='#ffffff' style='padding: 23px 56px 26px 56px;' class='card'>
                                          <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                            <tr>
                                              <td align='center' class='logo'>
                                                <img src='" + _baseUrl + _routeGetFile + mainlogo + @"' alt='logo' width='74' border='0' style='display: block;'/>
                                              </td>
                                            </tr>
                                            <tr>" + GetText(letterParams) + @"</tr>   
                                            <tr>
                                              <td align='center' style='padding: 14px 0 3px 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 700; font-size: 18px; line-height: 27px;' class='mobile-question'>
                                                Не знаете, что это за письмо?
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 2px 25px 5px 25px; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 500; font-size: 14px; line-height: 21px;' class='mobile-response'>
                                                  Не беспокойся! Возможно, вы получили этот email по ошибке.
                                                  Если вы проигнорируете или удалите это письмо, больше ничего не произойдет.
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 14px 0 4px 0; color: #000000; font-family: Carter One, cursive; font-weight: normal; font-size: 18px; line-height: 27px;' class='mobile-link'>
                                                <a href='#' style='color: #FFA800; text-decoration: none;'>(Название магазина)</a>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 2px 0 0 0; color: #222222; font-family: Poppins, Arial, sans-serif; font-weight: 500; font-size: 12px; line-height: 18px;'>
                                                ©(Название магазина) от<strong> Igor Shevchenko.</strong>
                                              </td>
                                            </tr>
                                            <tr>
                                              <td align='center' style='padding: 0 0 2px 0; color: #222222; font-family: Poppins, Arial, sans-serif; font-weight: 500; font-size: 12px; line-height: 18px;'>
                                                Все права защищены.
                                              </td>
                                            </tr>
                                          </table>
                                        </td>
                                      </tr>
                                    </table>
                                  </td>
                                </tr>
                              </table>
                            </body>
                        </html>";
            }
        }
    }
}
