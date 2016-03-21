angular.module('traktat.ui')
  .directive('trkDomainPicker', function (GlobalsService) {
      return {
          templateUrl: 'app/common/directives/traktat.ui/trkDomainPicker/trkDomainPicker.tpl.html',
          restrict: 'E',
          scope: {
              selection: '=',
              existingItems: '=except'
          },
          controller: function ($scope, GlobalsService, _) {

              var items = GlobalsService.get('domains');

              if ($scope.existingItems) {
                  items.forEach(disableIfExisting);
              }

              $scope.items = items;
              $scope.model = $scope;

              function disableIfExisting(item) {
                  var found = !!(_.findWhere($scope.existingItems, { domainId: item.id }));
                  item.disabled = found;
              }

              
          }
      };
  });