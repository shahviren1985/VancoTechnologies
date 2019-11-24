var Bhaari = function () {
    var CallHandler = function (URL, SuccessFunction, FailureFunction) {
        $.ajax({
            url: URL,
            success: SuccessFunction,
            error: FailureFunction
        });
    }
}
