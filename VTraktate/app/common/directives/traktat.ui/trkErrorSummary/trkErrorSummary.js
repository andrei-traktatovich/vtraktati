// produces a summary of errors in a control by iterating through its $error object
// attribute: points to an object that has an $error dictionary on it... 
// TODO: decouple directive from template ... 

angular.module('traktat.ui')
.directive('trkErrorSummary', function () {

    var errorMessages = {
        "required": "Это поле обязательно",
        "minlength": "Слишком мало букав",
        "maxlength": "Слишком много букав",
        "trkUnique": "Значение не является уникальным",
        'trkValidateEmail': 'Некорректный адрес электронной почты',
        "pattern": "Вводишь ненужные символы или не вводишь нужные, дружок",
        "trkMatch": "Значения должны совпадать",
        "email": "Некорректный адрес электропочты",
        "trkPositiveAmount": "Должна быть положительная сумма",
        "number": "Должно быть число",
        "internationalPhoneNumber": "Неверный тел.",
        "min": "Значение выходит за пределы допустимого диапазона",
        "max": "Значение выходит за пределы допустимого диапазона"
    };

    var getErrorMessage = function (err) {
        var result = errorMessages[err] || "Неизвестная ошибка '" + err + "'";
        return result;
    };

    return {
        restrict: 'E',
        scope: {
            for: '='
        },
        templateUrl: 'app/common/directives/traktat.ui/trkErrorSummary/trkErrorSummary.tpl.html',
        link: function (scope, el, attrs, ctrl) {
            console.log(scope);
            scope.errs = scope.for.$error;
             
            scope.getErrorMessage = getErrorMessage;
        }
    }
});