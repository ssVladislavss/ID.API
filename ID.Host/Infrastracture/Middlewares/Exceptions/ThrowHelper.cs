using ID.Core.ApiResources.Exceptions;
using ID.Core.ApiScopes.Exceptions;
using ID.Core.Clients.Exceptions;
using ID.Core.Exceptions.Base;
using ID.Core.Users.Exceptions;

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
                ApiScopeAddException => AjaxResult.Error("Не удалось создать область доступа"),
                ApiScopeEditException => AjaxResult.Error("Не удалось сохранить данные области доступа"),
                ApiScopeRemoveException => AjaxResult.Error("Не удалось удалить данные области доступа"),
                ApiScopeNoContentException => AjaxResult.Error("Не найдено ниодной области доступа"),
                ApiScopeNotFoundException => AjaxResult.Error("Область доступа не найдена"),
                ApiResourceAddException => AjaxResult.Error("Не удалось создать ресурс"),
                ApiResourceEditException => AjaxResult.Error("Не удалось сохранить данные ресурса"),
                ApiResourceRemoveException => AjaxResult.Error("Не удалось удалить данные ресурса"),
                ApiResourceNoContentException => AjaxResult.Error("Не найдено ниодного ресурса"),
                ApiResourceNotFoundException => AjaxResult.Error("Ресурс не найден"),

                UserAddException => AjaxResult.Error("Не удалось зарегистрировать пользователя"),
                UserEditException => AjaxResult.Error("Не удалось сохранить данные пользователя"),
                UserDeleteException => AjaxResult.Error("Не удалось удалить данные пользователя"),
                UserNoContentException => AjaxResult.Error("Не найдено ниодного пользователя"),
                UserNotFoundException => AjaxResult.Error("Пользователь не найден"),
                UserRoleNotFoundException => AjaxResult.Error("Не найдена роль пользователя"),
                _ => AjaxResult.Error("Произошла неизвестная ошибка"),
            };
        }
    }
}
