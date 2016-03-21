// https://github.com/ericpanorel/indicated-angular-button/blob/master/indiClick.js

var customDirectives = angular.module('customDirectives', []);

(function (Spinner, module) {
    'use strict';
    var directiveId = 'indiClick';
    module.directive(directiveId, ['$parse', function ($parse) {
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;
        function link(scope, element, attr) {
            var fn = $parse(attr[directiveId]),
            target = element[0];

            element.on('click', function (event) {
                scope.$apply(function () {
                    
                    var height = element.height(),
                    oldWidth = element.width(),
                    elText = element.text(),
                    elColor = element.css("color"),
                    opts = {
                        length: 5, //Math.round(height / 3),
                        radius: 5, //Math.round(height / 2),
                        width: 2,// Math.round(height / 10),
                        color: elColor,
                        left: '30%'
                    }; // customize this "resizing and coloring" algorithm

                    attr.$set('disabled', true);
                    //element.width(oldWidth + oldWidth / 2); // make room for spinner
                    element.text('тук-тук');

                    var spinner = new Spinner(opts).spin(target);
                    // expects a promise
                    // http://docs.angularjs.org/api/ng.$q
                    fn(scope, { $event: event })
                    .then(function (res) {
                        element.width(oldWidth); // restore size
                        attr.$set('disabled', false);
                        spinner.stop();
                        element.text(elText);
                        return res;
                    }, function (res) {
                        element.width(oldWidth); // restore size
                        attr.$set('disabled', false);
                        element.text(elText);
                        spinner.stop();
                    });
                });
            });
        }
    }]);
})(Spinner, customDirectives);