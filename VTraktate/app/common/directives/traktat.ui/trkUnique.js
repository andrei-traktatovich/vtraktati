angular.module('traktat.ui')
    .directive('trkUnique', function ($q) {
        return {
            restrict: 'A',
            scope: {
                service: '=trkUnique',
                data: '=trkUniqueData'
            },
            require: 'ngModel',
            link: function (scope, elem, attr, ngModel) {

                ngModel.$asyncValidators.trkUnique = function (modelValue, viewValue) {

                    return scope.service.test(scope.data, viewValue).then(function (response) {
                        if (!response.data.ok) {
                            return $q.reject(response.data.errorMessage || 'Значение не является уникальным.');
                        }
                        else {
                            return true;
                        }
                    }, function error(response) {
                        return $q.reject(response.errorMessage); // ???
                    });
                };
            }
        };
    });
            