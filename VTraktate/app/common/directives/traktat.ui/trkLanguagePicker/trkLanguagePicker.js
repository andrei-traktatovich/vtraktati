angular.module('traktat.ui')
  .directive('trkLanguagePicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkLanguagePicker/trkLanguagePicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '=',
              itemsToExclude : '=except'
          },
          controller: function ($scope, GlobalsService) {
              $scope.items = exclude(GlobalsService.get('languages'), $scope.itemsToExclude);
              $scope.model = $scope;

              function exclude(source, itemsToExclude) {
                  if (!itemsToExclude) 
                      return source;

                  var idsToExclude = itemsToExclude.map(function(item) { return item.languagePairId; });

                  return source.filter(function (item) {
                      return idsToExclude.indexOf(item.id) < 0;
                  });
              }
          }
      };
  });