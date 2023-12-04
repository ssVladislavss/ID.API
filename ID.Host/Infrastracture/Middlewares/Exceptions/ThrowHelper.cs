using ID.Core.ApiResources.Exceptions;
using ID.Core.ApiScopes.Exceptions;
using ID.Core.Clients.Exceptions;
using ID.Core.Exceptions.Base;

namespace ID.Host.Infrastracture.Middlewares.Exceptions
{
    public class ThrowHelper
    {
        public static AjaxResult HandleIDException(BaseIDException iDException)
        {
            switch (iDException)
            {
                case ClientAddException:
                    return AjaxResult.Error("Не удалось создать приложение");
                case ClientEditException:
                    return AjaxResult.Error("Не удалось сохранить данные приложения");
                case ClientRemoveException:
                    return AjaxResult.Error("Не удалось удалить данные приложения");
                case ClientNoContentException:
                    return AjaxResult.Error("Не найдено ниодного приложения");
                case ClientNotFoundException:
                    return AjaxResult.Error("Приложение не найдено");

                case ApiScopeAddException:
                    return AjaxResult.Error("Не удалось создать область доступа");
                case ApiScopeEditException:
                    return AjaxResult.Error("Не удалось сохранить данные области доступа");
                case ApiScopeRemoveException:
                    return AjaxResult.Error("Не удалось удалить данные области доступа");
                case ApiScopeNoContentException:
                    return AjaxResult.Error("Не найдено ниодной области доступа");
                case ApiScopeNotFoundException:
                    return AjaxResult.Error("Область доступа не найдена");

                case ApiResourceAddException:
                    return AjaxResult.Error("Не удалось создать ресурс");
                case ApiResourceEditException:
                    return AjaxResult.Error("Не удалось сохранить данные ресурса");
                case ApiResourceRemoveException:
                    return AjaxResult.Error("Не удалось удалить данные ресурса");
                case ApiResourceNoContentException:
                    return AjaxResult.Error("Не найдено ниодного ресурса");
                case ApiResourceNotFoundException:
                    return AjaxResult.Error("Ресурс не найден");
                default:
                    return AjaxResult.Error("Произошла неизвестная ошибка");
            }
        }
    }
}
