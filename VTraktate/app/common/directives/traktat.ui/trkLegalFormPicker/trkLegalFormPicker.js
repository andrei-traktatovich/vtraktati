angular.module('traktat.ui')
  .directive('trkLegalFormPicker', function () {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkLegalFormPicker/trkLegalFormPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '=',
              itemsToExclude : '=except'
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = exclude(GlobalsService.get('legalForms'), $scope.itemsToExclude);
              $scope.model = $scope;

              function exclude(source, itemsToExclude) {
                  if (!itemsToExclude) 
                      return source;
                      
                  var idsToExclude = itemsToExclude.map(function(item) { return item.id; });

                  return source.filter(function (item) {
                      return idsToExclude.indexOf(item.id) < 0;
                  });
              }
          }
      };
  });