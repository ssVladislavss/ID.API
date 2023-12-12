using ID.Core.ApiResources.Exceptions;
using ID.Core.ApiScopes.Exceptions;
using ID.Core.Clients.Exceptions;
using ID.Core.Exceptions.Base;
using ID.Core.Roles.Exceptions;
using ID.Core.Users.Exceptions;
using ID.Host.Infrastracture.Services.Users.Exceptions;
using ISDS.ServiceExtender.Http;

namespace ID.Host.Infrastracture.Middlewares.Exceptions
{
    public class ThrowHelper
    {
        public static AjaxResult HandleIDException(BaseIDException iDException)
        {
            return iDException switch
            {
                ClientAddException => AjaxResult.Error("Не удалось создать приложение"),
                ClientEditException => AjaxResult.Error("Не удалось сохранить данные приложения"),
                ClientRemoveException => AjaxResult.Error("Не удалось удалить данные приложения"),
                ClientNoContentException => AjaxResult.Error("Не найдено ниодного приложения"),
                ClientNotFoundException => AjaxResult.Error("Приложение не найдено"),
                ClientRemoveAccessException => AjaxResult.Error("У Вас нет доступа к данной операции"),
                ClientDefaultException => AjaxResult.Error("Работа на приложением запрещена"),

                ApiScopeAddException => AjaxResult.Error("Не удалось создать область доступа"),
                ApiScopeEditException => AjaxResult.Error("Не удалось сохранить данные области доступа"),
                ApiScopeRemoveException => AjaxResult.Error("Не удалось удалить данные области доступа"),
                ApiScopeNoContentException => AjaxResult.Error("Не найдено ниодной области доступа"),
                ApiScopeNotFoundException => AjaxResult.Error("Область доступа не найдена"),
                ApiScopeDefaultException => AjaxResult.Error("Работа над областью запрещена"),

                ApiResourceAddException => AjaxResult.Error("Не удалось создать ресурс"),
                ApiResourceEditException => AjaxResult.Error("Не удалось сохранить данные ресурса"),
                ApiResourceRemoveException => AjaxResult.Error("Не удалось удалить данные ресурса"),
                ApiResourceNoContentException => AjaxResult.Error("Не найдено ниодного ресурса"),
                ApiResourceNotFoundException => AjaxResult.Error("Ресурс не найден"),
                ApiResourceDefaultException => AjaxResult.Error("Работа над ресурсом запрещена"),

                UserAddException => AjaxResult.Error("Не удалось зарегистрировать пользователя"),
                UserEditException => AjaxResult.Error("Не удалось сохранить данные пользователя"),
                UserDeleteException => AjaxResult.Error("Не удалось удалить данные пользователя"),
                UserNoContentException => AjaxResult.Error("Не найдено ниодного пользователя"),
                UserNotFoundException => AjaxResult.Error("Пользователь не найден"),
                UserRoleNotFoundException => AjaxResult.Error("Не найдена роль пользователя"),
                UserAddExistException => AjaxResult.Error("Пользователь уже зарегистрирован"),
                UserEditAccessExeption => AjaxResult.Error("Данные этого пользователя доступны только для чтения"),
                UserRemoveAccessException => AjaxResult.Error("Данные этого пользователя доступны только для чтения"),
                UserDefaultException => AjaxResult.Error("Работа над пользователем запрещена"),
                UserFindAccessException => AjaxResult.Error("Доступ запрещён"),
                UserResetConfirmPasswordException => AjaxResult.Error("Не удалось сбросить пароль"),
                UserEmailConfirmException => AjaxResult.Error("Не удалось подтвердить электронный адрес"),
                UserAddAccessException => AjaxResult.Error("Доступ запрещён"),
                UserChangePasswordException => AjaxResult.Error("Не удалось изменить пароль"),
                UserChangePhoneNumberException => AjaxResult.Error("Не удалось изменить номер телефона"),
                UserConfirmPhoneNumberException => AjaxResult.Error("Не удалось подвердить номер телефона"),
                UserSetLockoutEnabledException => AjaxResult.Error("Не удалось заблокировать аккаунт"),
                UserEmailNotifyDeliveredException => AjaxResult.Error("Не удалось доставить email сообщение"),
                UserCodeAddException => AjaxResult.Error("Не удалось сохранить код подтверждения пользователя"),
                UserVerifyCodeException => AjaxResult.Error("Полученный код не валиден"),
                UserVerifyCodeExpireException => AjaxResult.Error("Время жизни полученного когда истекло"),

                RoleAddException => AjaxResult.Error("Не удалось зарегистрировать роль"),
                RoleEditException => AjaxResult.Error("Не удалось сохранить данные роли"),
                RoleRemoveException => AjaxResult.Error("Не удалось удалить данные роли"),
                RoleNoContentException => AjaxResult.Error("Не найдено ниодной роли"),
                RoleNotFoundException => AjaxResult.Error("Роль не найдена"),
                RoleAddClaimException => AjaxResult.Error("Не удалось добавить утверждения к роли"),
                RoleDefaultException => AjaxResult.Error("Работа над ролью запрещена"),

                _ => AjaxResult.Error("Произошла неизвестная ошибка"),
            };
        }
    }
}
