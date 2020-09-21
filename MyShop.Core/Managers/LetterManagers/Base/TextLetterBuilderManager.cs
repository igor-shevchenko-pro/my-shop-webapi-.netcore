using MyShop.ApiModels;
using MyShop.Core.Interfaces.Managers.LetterManagers.Base;
using MyShop.Core.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Managers.LetterManagers.Base
{
    public class TextLetterBuilderManager : ITextLetterBuilderManager
    {
        List<LetterTextModel> Texts { get; set; }

        public TextLetterBuilderManager()
        {
            InitializeLetterText();
        }

        public string GetText(LetterTextParamsModel letterParams)
        {
            var text = Texts.FirstOrDefault(x => x.LetterType == letterParams.LetterType && 
                                                 x.LetterLanguage == letterParams.LetterLanguage && 
                                                 x.TypeContact == letterParams.TypeContact)?.Text ?? "";

            foreach (var item in letterParams.Params)
            {
                var name = "{" + item.Key + "}";

                if (text.Contains(name))
                {
                    text = text.Replace(name, item.Value);
                }
            }

            return text;
        }

        private void InitializeLetterText()
        {
            Texts = new List<LetterTextModel>();
            // Email verification
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.Russian,
                LetterType = LetterTypeEnum.ContactVerification,
                TypeContact = ContactTypeEnum.Email,
                Text = @"<td>
                          <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                            <tr>
                              <td align='center' style='padding: 14px 0 0 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi1'>
                                  Поздравляем <strong><a href='mailto:{contact}' target='_blank' rel='noopener noreferrer' style='color: #000000 !important; text-decoration: none !important; cursor: unset !important;'>{contact}</a></strong>,
                              </td>
                            </tr>
                            <tr>
                              <td align='center' style='padding: 0 0 14px 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi2'>
                                  спасибо, что присоединились к нам!
                              </td>
                            </tr>
                            <tr>
                              <td style='background: radial-gradient(50% 50% at 50% 50%, #004ADB 0%, rgba(0, 44, 139, 0.9) 100%); border-radius: 14px;'>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                  <tr>
                                    <td align='center' style='padding: 44px 0 7px 0; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 40px; line-height: 60px;' class='mobile-title'>
                                      Подтвердите ваш адрес электронной почты!
                                    </td>
                                  </tr>
                                  <tr>
                                    <td align='center' style='padding: 10px 80px 10px 80px; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 18px; line-height: 27px;' class='mobile-subtitle'>
                                      Нам просто нужно подтвердить, что <a href='mailto:{contact}' target='_blank' rel='noopener noreferrer' style='color: #ffffff !important; text-decoration: none !important; cursor: unset !important;'>{contact}</a> это ваш адрес электронной почты.
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                        <tr>
                                          <td align='center'>
                                            <table border='0' cellspacing='0' cellpadding='0'>
                                              <tr>
                                                <td align='center' class='mobile-button' style='padding: 48px 0 46px 0;'>
                                                  <a href='{confirmationLink}' target='_blank' style='font-size: 17px; line-height: 25px; font-family: Poppins, Arial, sans-serif; font-weight: 700; color: #ffffff; text-decoration: none; border-radius: 30px; -webkit-border-radius: 30px; -moz-border-radius: 30px; background-color: #FFA800; border-top: 17px solid #FFA800; border-bottom: 18px solid #FFA800; border-right: 37px solid #FFA800; border-left: 37px solid #FFA800; display: inline-block;'>
                                                    Подтвердить email
                                                  </a>
                                                </td>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </td>"
                //Text = @"Уважаемый Пользователь,
                //<br /><br />
                //Мы получили запрос о добавлении электронного адреса {contact} в ваш аккаунт LAMPOCHKA.UA.
                //<br /><br />
                //Чтобы подтвердить электронный адрес, нажмите эту ссылку: <br/><br />" + "<a href='{confirmationLink}'>{confirmationLink}</a>" + @"<br /><br />
                //Если эта ссылка не работает, откройте новое окно браузера, а затем скопируйте и вставьте URL-адрес в адресную строку.<br />
                //Если вы получили это сообщение по ошибке, не предпринимайте никаких действий. Если вы не нажмете эту ссылку, то адрес не будет добавлен в аккаунт.<br /><br />
                //С уважением,<br />
                //команда LAMPOCHKA.UA<br /><br />
                //Это сообщение электронной почты было сгенерировано автоматически. Отвечать на этот адрес электронной почты не следует."
            });
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.English,
                LetterType = LetterTypeEnum.ContactVerification,
                TypeContact = ContactTypeEnum.Email,
                Text = @"<td>
                          <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                            <tr>
                              <td align='center' style='padding: 14px 0 0 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi1'>
                                  Congratulations <strong><a href='mailto:{contact}' target='_blank' rel='noopener noreferrer' style='color: #000000 !important; text-decoration: none !important; cursor: unset !important;'>{contact}</a></strong>,
                              </td>
                            </tr>
                            <tr>
                              <td align='center' style='padding: 0 0 14px 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi2'>
                                  thanks for joining us!
                              </td>
                            </tr>
                            <tr>
                              <td style='background: radial-gradient(50% 50% at 50% 50%, #004ADB 0%, rgba(0, 44, 139, 0.9) 100%); border-radius: 14px;'>
                                <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                  <tr>
                                    <td align='center' style='padding: 44px 0 7px 0; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 40px; line-height: 60px;' class='mobile-title'>
                                      Confirm your email!
                                    </td>
                                  </tr>
                                  <tr>
                                    <td align='center' style='padding: 10px 80px 10px 80px; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 18px; line-height: 27px;' class='mobile-subtitle'>
                                      We just need to verify that <a href='mailto:{contact}' target='_blank' rel='noopener noreferrer' style='color: #ffffff !important; text-decoration: none !important; cursor: unset !important;'>{contact}</a> is your email address.
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                      <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                        <tr>
                                          <td align='center'>
                                            <table border='0' cellspacing='0' cellpadding='0'>
                                              <tr>
                                                <td align='center' class='mobile-button' style='padding: 48px 0 46px 0;'>
                                                  <a href='{confirmationLink}' target='_blank' style='font-size: 17px; line-height: 25px; font-family: Poppins, Arial, sans-serif; font-weight: 700; color: #ffffff; text-decoration: none; border-radius: 30px; -webkit-border-radius: 30px; -moz-border-radius: 30px; background-color: #FFA800; border-top: 17px solid #FFA800; border-bottom: 18px solid #FFA800; border-right: 37px solid #FFA800; border-left: 37px solid #FFA800; display: inline-block;'>
                                                    Confirm email
                                                  </a>
                                                </td>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                        </td>"
                //Text = @"Dear user,
                //<br /><br />
                //We received a request to add the {contact} Email address to your account LAMPOCHKA.UA.
                //<br /><br />
                //To confirm your Email address, click this link: <br /><br />" + "<a href='{confirmationLink}'>{confirmationLink}</a>" + @"<br /><br />
                //If this link does not work, open a new browser window, and then copy and paste the URL into the address bar.<br />
                //If you received this message as an error, don’t take any action. If you don’t click on the link, the address won’t be added to your account.<br /><br />
                //With best regards,<br />
                //LAMPOCHKA.UA Team<br /><br />
                //This Email has been generated automatically. You should not reply to this Email address."
            });
            
            // Recovery password by email
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.Russian,
                LetterType = LetterTypeEnum.RecoveryPassword,
                TypeContact = ContactTypeEnum.Email,
                Text = @"<td>
                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                              <tr>
                                <td align='center'
                                  style='padding: 14px 0 0 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi1'>
                                  Здравствуйте, <strong><a href='mailto:{contact}' target='_blank' rel='noopener noreferrer' style='color: #000000 !important; text-decoration: none !important; cursor: unset !important;'>{contact}</a></strong>,
                                </td>
                              </tr>
                              <tr>
                                <td align='center'
                                  style='padding: 0 0 14px 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi2'>
                                  спасибо, что доверяете нам!
                                </td>
                              </tr>
                              <tr>
                                <td style='background: radial-gradient(50% 50% at 50% 50%, #004ADB 0%, rgba(0, 44, 139, 0.9) 100%); border-radius: 14px;'>
                                  <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                    <tr>
                                      <td align='center' style='padding: 44px 0 7px 0; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 40px; line-height: 60px;' class='mobile-title'>
                                        Сбросить пароль
                                      </td>
                                    </tr>
                                    <tr>
                                      <td align='center' style='padding: 10px 80px 10px 80px; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 18px; line-height: 27px;' class='mobile-subtitle'>
                                        Мы получили запрос на изменение вашего пароля.
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                          <tr>
                                            <td align='center'>
                                              <table border='0' cellspacing='0' cellpadding='0'>
                                                <tr>
                                                  <td align='center' class='mobile-button' style='padding: 48px 0 46px 0;'>
                                                    <a href='{confirmationLink}' target='_blank' style='font-size: 17px; line-height: 25px; font-family: Poppins, Arial, sans-serif; font-weight: 700; color: #ffffff; text-decoration: none; border-radius: 30px; -webkit-border-radius: 30px; -moz-border-radius: 30px; background-color: #FFA800; border-top: 17px solid #FFA800; border-bottom: 18px solid #FFA800; border-right: 37px solid #FFA800; border-left: 37px solid #FFA800; display: inline-block;'>
                                                      Сброс пароля
                                                    </a>
                                                  </td>
                                                </tr>
                                              </table>
                                            </td>
                                          </tr>
                                        </table>
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                            </table>
                          </td>"
                //Text = @"Уважаемый Пользователь,
                //<br /><br />
                //Мы получили запрос на сброс пароля для вашего аккаунта {contact} в LAMPOCHKA.UA.
                //<br /><br />
                //Чтобы подтвердить сброс пароля, нажмите эту ссылку: <br /><br />" + "<a href='{confirmationLink}'>{confirmationLink}</a>" + @"<br /><br />
                //Если эта ссылка не работает, откройте новое окно браузера, а затем скопируйте и вставьте URL-адрес в адресную строку.<br />
                //Если вы получили это сообщение по ошибке, не предпринимайте никаких действий. Если вы не нажмете эту ссылку, то вы не сможете сбросить пароль.<br /><br />
                //С уважением,<br />
                //команда LAMPOCHKA.UA<br /><br />
                //Это сообщение электронной почты было сгенерировано автоматически. Отвечать на этот адрес электронной почты не следует."
            });
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.English,
                LetterType = LetterTypeEnum.RecoveryPassword,
                TypeContact = ContactTypeEnum.Email,
                Text = @"<td>
                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                              <tr>
                                <td align='center'
                                  style='padding: 14px 0 0 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi1'>
                                  Hello, <strong><a href='mailto:{contact}' target='_blank' rel='noopener noreferrer' style='color: #000000 !important; text-decoration: none !important; cursor: unset !important;'>{contact}</a></strong>,
                                </td>
                              </tr>
                              <tr>
                                <td align='center'
                                  style='padding: 0 0 14px 0; color: #000000; font-family: Poppins, Arial, sans-serif; font-weight: 300; font-size: 20px; line-height: 30px;' class='mobile-text-hi2'>
                                  thanks for trusting us!
                                </td>
                              </tr>
                              <tr>
                                <td style='background: radial-gradient(50% 50% at 50% 50%, #004ADB 0%, rgba(0, 44, 139, 0.9) 100%); border-radius: 14px;'>
                                  <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                    <tr>
                                      <td align='center' style='padding: 44px 0 7px 0; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 40px; line-height: 60px;' class='mobile-title'>
                                        Reset your password
                                      </td>
                                    </tr>
                                    <tr>
                                      <td align='center' style='padding: 10px 80px 10px 80px; color: #ffffff; font-family: Poppins, Arial, sans-serif; font-weight: 400; font-size: 18px; line-height: 27px;' class='mobile-subtitle'>
                                        We’ve received a request to reset your password.
                                      </td>
                                    </tr>
                                    <tr>
                                      <td>
                                        <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                          <tr>
                                            <td align='center'>
                                              <table border='0' cellspacing='0' cellpadding='0'>
                                                <tr>
                                                  <td align='center' class='mobile-button' style='padding: 48px 0 46px 0;'>
                                                    <a href='{confirmationLink}' target='_blank' style='font-size: 17px; line-height: 25px; font-family: Poppins, Arial, sans-serif; font-weight: 700; color: #ffffff; text-decoration: none; border-radius: 30px; -webkit-border-radius: 30px; -moz-border-radius: 30px; background-color: #FFA800; border-top: 17px solid #FFA800; border-bottom: 18px solid #FFA800; border-right: 37px solid #FFA800; border-left: 37px solid #FFA800; display: inline-block;'>
                                                      Reset password
                                                    </a>
                                                  </td>
                                                </tr>
                                              </table>
                                            </td>
                                          </tr>
                                        </table>
                                      </td>
                                    </tr>
                                  </table>
                                </td>
                              </tr>
                            </table>
                          </td>"
                //Text = @"Dear user,
                //<br /><br />
                //We received a request to reset the password for your account {contact} in LAMPOCHKA.UA.
                //<br /><br />
                //To reset your password, click this link: <br /><br />" + "<a href='{confirmationLink}'>{confirmationLink}</a>" + @"<br /><br />
                //If this link does not work, open a new browser window, and then copy and paste the URL into the address bar.<br />
                //If you received this message as an error, don’t take any action. If you don’t click on the link, your password won't be reseted.<br /><br />
                //With all regards,<br />
                //LAMPOCHKA.UA Team<br /><br />
                //This Email has been generated automatically. You should not reply to this Email address."
            });
                      
            // Phone verification
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.Russian,
                LetterType = LetterTypeEnum.ContactVerification,
                TypeContact = ContactTypeEnum.Phone,
                Text = "Для подтверждения номера используйте этот код: {confirmationLink}"
            });
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.English,
                LetterType = LetterTypeEnum.ContactVerification,
                TypeContact = ContactTypeEnum.Phone,
                Text = "Please confirm your Phone by using this code: {confirmationLink}"
            });
            
            // Recovery password by phone
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.Russian,
                LetterType = LetterTypeEnum.RecoveryPassword,
                TypeContact = ContactTypeEnum.Phone,
                Text = "Для сброса пароля используйте этот код: {confirmationLink}"
            });
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.English,
                LetterType = LetterTypeEnum.RecoveryPassword,
                TypeContact = ContactTypeEnum.Phone,
                Text = "Please reset your password by using this code: {confirmationLink}"
            });

            // Support service answer
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.Russian,
                LetterType = LetterTypeEnum.SupportService,
                TypeContact = ContactTypeEnum.Email,
                Text = "Ответ службы поддержки <br><br> {question} <br><br> {answer}"
            });
            Texts.Add(new LetterTextModel
            {
                LetterLanguage = UserLanguageEnum.English,
                LetterType = LetterTypeEnum.SupportService,
                TypeContact = ContactTypeEnum.Email,
                Text = "Support service answer <br><br> {question} <br><br> {answer}"
            });
        }
    }
}
