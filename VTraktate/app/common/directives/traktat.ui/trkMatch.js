angular.module('traktat.ui')
  .directive('trkMatch', function () {
      return {
          scope: {
              trkMatch: '='
          },
          require: 'ngModel',
          restrict: 'A',
          link: function (scope, elem, attr, ngModel) {
              ngModel.$validators.trkMatch = function (modelValue, viewValue) {
                  var value = viewValue,
                  valueToCompare = scope.trkMatch && scope.trkMatch.$viewValue;//(scope.trkMatch.$modelValue || scope.trkMatch.$viewValue);

                  return value == valueToCompare;
              };
          }
      }
  });