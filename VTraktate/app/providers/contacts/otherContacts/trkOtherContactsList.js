angular.module('traktat.ui')
.directive('trkOtherContactsList', function() {
    return {
        scope : { 
            items : '=' 
        },
        templateUrl: 'app/providers/contacts/otherContacts/trkOtherContactsList.tpl.html',
        controller: function ($scope, GlobalsService) {

            $scope.contactTypes = GlobalsService.get('otherContactTypes');

            $scope.items = $scope.items || [];

            $scope.remove = function (item) {
                var index = $scope.items.indexOf(item);
                $scope.items.splice(index, 1);
            };

            $scope.add = function () {
                $scope.items.push(createItem());

                function createItem() {
                    return {};
                }
            };

        }
    };
})