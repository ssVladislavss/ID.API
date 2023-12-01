namespace ID.Host.Infrastracture
{
    public class AjaxResult
    {
        public AjaxResultTypes Result { get; set; }
        public string? Message { get; set; }

        public AjaxResult(AjaxResultTypes result, string? message)
        {
            this.Result = result;
            this.Message = message;
        }

        public static AjaxResult Success()
            => new(AjaxResultTypes.Success, null);
        public static AjaxResult Error(string message)
            => new(AjaxResultTypes.Error, message);
    }

    public class AjaxResult<T> : AjaxResult
    {
        public T? Data { get; set; }
        public AjaxResult(AjaxResultTypes result, string? message) : base(result, message) { }

        public static AjaxResult<T> Success(T data)
            => new(AjaxResultTypes.Success, null) { Data = data };
        public static new AjaxResult<T> Error(string message)
            => new(AjaxResultTypes.Error, message);
    }
}
