angular.module('traktat.ui')
  .directive('trkPositiveAmount', function () {
      return {
          require: 'ngModel',
          restrict: 'A',
          link: function (scope, elem, attr, ngModel) {
              
              function isValidAmount(value) {
                // value should be a valid positive number with two decimal places
                  return value >= 0;
              }


              ngModel.$validators.trkPositiveAmount = function (modelValue, viewValue) {
                  console.log('viewValue = ' + viewValue + ' got validated as ' + isValidAmount(viewValue));
                  return isValidAmount(viewValue);
              };
          }
      }
  });