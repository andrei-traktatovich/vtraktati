﻿angular.module('traktat.ui').directive('trkRequireMultiple', function () {
    return {
        require: 'ngModel',
        link: function postLink(scope, element, attrs, ngModel) {
            ngModel.$validators.required = function (value) {
                return angular.isArray(value) && value.length > 0;
            };
        }
    };
});